namespace WindowToTheSociety.Web.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Services.Data;
    using WindowToTheSociety.Web.ViewModels.Posts;

    public class PostsController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPhotosService photosService;
        private readonly IPostsService postsService;

        public PostsController(UserManager<ApplicationUser> userManager, IPhotosService photosService, IPostsService postsService, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.photosService = photosService;
            this.postsService = postsService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public IActionResult CreatePost()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostInputModel input)
        {
            bool doesThePostContainPhoto = input.Photo != null;
            bool doesThePostContainText = input.Text != null;

            if (!doesThePostContainPhoto && !doesThePostContainText)
            {
                this.ModelState.AddModelError("Text", "You can't create empty post.");
                return this.View();
            }

            if (doesThePostContainText && input.Text.Length > 3000)
            {
                this.ModelState.AddModelError("Text", "Text cannot be more than 3000 symbols.");
            }

            if (doesThePostContainPhoto)
            {
                if (!input.Photo.FileName.EndsWith(".jpg"))
                {
                    this.ModelState.AddModelError("Photo", "Invalid file type. Only file type .jpg is allowed.");
                }

                if (input.Photo.Length > 10 * 1024 * 1024)
                {
                    this.ModelState.AddModelError("Photo", "File too big.");
                }
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            string userId = this.userManager.GetUserId(this.User);
            string photoGuid = Guid.NewGuid().ToString();
            string fileFolderAndName = $"/Photos/{photoGuid}.jpg";

            await this.AppendPhoto(input, userId, fileFolderAndName, doesThePostContainPhoto);
            await this.postsService.CreatePost(fileFolderAndName, input.Text, userId, null);

            return this.Redirect("/Users/Profile");
        }

        private async Task AppendPhoto(CreatePostInputModel input, string userId, string fileFolderAndName, bool doesThePostContainPhoto)
        {
            if (doesThePostContainPhoto)
            {
                string filePath = this.webHostEnvironment.WebRootPath + fileFolderAndName;

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await input.Photo.CopyToAsync(stream);
                }

                await this.photosService.AppendPhoto(fileFolderAndName, userId, null, (PhotoType)3);
            }
        }
    }
}
