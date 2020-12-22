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
    using WindowToTheSociety.Web.ViewModels.Pages;
    using WindowToTheSociety.Web.ViewModels.Posts;

    public class PagesController : Controller
    {
        private readonly IPagesService pagesService;
        private readonly IPhotosService photosService;
        private readonly IPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PagesController(IPagesService pagesService, IPhotosService photosService, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, IPostsService postsService)
        {
            this.pagesService = pagesService;
            this.photosService = photosService;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.postsService = postsService;
        }

        [Authorize]
        public IActionResult AddPost()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPostAsync(CreatePostInputModel input, string id)
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

            await this.postsService.CreatePost(fileFolderAndName, input.Text, null, id);
            await this.AppendPhoto(input, id, fileFolderAndName, doesThePostContainPhoto);

            return this.Redirect($"/Pages/Page/{id}");
        }

        [Authorize]
        public IActionResult PagesMenu()
        {
            string userId = this.userManager.GetUserId(this.User);
            SelectPagesViewModel pages = this.pagesService.GetSelectPagesViewModel(userId);

            return this.View(pages);
        }

        [Authorize]
        public IActionResult Page(string id)
        {
            PageViewModel page = this.pagesService.GetPageViewModel(id);

            return this.View(page);
        }

        [Authorize]
        public IActionResult CreatePage()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePage(CreatePageInputModel input)
        {
            this.DataValidation(input);

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            string userId = this.userManager.GetUserId(this.User);

            await this.pagesService.CreatePage(input.Title, userId);
            await this.AddCoverPhoto(input);

            return this.Redirect("/Pages/PagesMenu");
        }

        private async Task AddCoverPhoto(CreatePageInputModel input)
        {
            if (input.Picture != null)
            {
                string fileFolderAndName = $"/PagesCoverPhotos" + $"/{input.Title}.jpg";
                string filePath = this.webHostEnvironment.WebRootPath + fileFolderAndName;

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await input.Picture.CopyToAsync(stream);
                }

                string pageId = this.pagesService.GetIdByTitle(input.Title);
                await this.photosService.CreatePhoto(fileFolderAndName, null, pageId, (PhotoType)2);
                await this.pagesService.AppendPicture(input.Title, fileFolderAndName);
            }
        }

        private void DataValidation(CreatePageInputModel input)
        {
            if (input.Picture != null)
            {
                if (!input.Picture.FileName.EndsWith(".jpg"))
                {
                    this.ModelState.AddModelError("Picture", "Invalid file type.");
                }

                if (input.Picture.Length > 10 * 1024 * 1024)
                {
                    this.ModelState.AddModelError("Picture", "File too big.");
                }
            }

            if (input.Title.Length > 40)
            {
                this.ModelState.AddModelError("Title", "Title should between 3 and 40.");
            }

            if (input.Title.Length < 3)
            {
                this.ModelState.AddModelError("Title", "Title should between 3 and 40.");
            }
        }

        private async Task AppendPhoto(CreatePostInputModel input, string id, string fileFolderAndName, bool doesThePostContainPhoto)
        {
            if (doesThePostContainPhoto)
            {
                string filePath = this.webHostEnvironment.WebRootPath + fileFolderAndName;

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await input.Photo.CopyToAsync(stream);
                }

                await this.photosService.AppendPhoto(fileFolderAndName, null, id, (PhotoType)3);
            }
        }
    }
}
