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
    using WindowToTheSociety.Web.ViewModels.Photos;

    public class PhotosController : Controller
    {
        private readonly IUsersSurvice usersSurvice;
        private readonly IPhotosService photosService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;

        public PhotosController(IUsersSurvice usersSurvice, IPhotosService photosService, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.usersSurvice = usersSurvice;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.photosService = photosService;
        }

        [Authorize]
        public IActionResult AddPhoto()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPhoto(AddPhotoInputModel input)
        {
            if (!input.Picture.FileName.EndsWith(".jpg"))
            {
                this.ModelState.AddModelError("Picture", "Invalid file type.");
            }

            if (input.Picture.Length > 10 * 1024 * 1024)
            {
                this.ModelState.AddModelError("Picture", "File too big.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            string userId = this.userManager.GetUserId(this.User);
            string photoGuid = Guid.NewGuid().ToString();
            string fileFolderAndName = $"/{input.Type}s" + $"/{userId}.jpg";
            string filePath = this.webHostEnvironment.WebRootPath + fileFolderAndName;

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await input.Picture.CopyToAsync(stream);
            }

            await this.photosService.CreatePhoto(fileFolderAndName, userId, input.Type);

            return this.Redirect("/Users/Profile");
        }
    }
}
