namespace WindowToTheSociety.Web.Controllers
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Services.Data;
    using WindowToTheSociety.Web.ViewModels.Pictures;

    public class PicturesController : Controller
    {
        private readonly IUsersSurvice usersSurvice;
        private readonly IPicturesService picturesService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;

        public PicturesController(IUsersSurvice usersSurvice, IPicturesService picturesService, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.usersSurvice = usersSurvice;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.picturesService = picturesService;
        }

        [Authorize]
        public IActionResult AddPicture()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPicture(AddPictureInputModel input)
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
            string fileFolderAndName = $"/{input.Type}s" + $"/{userId}.jpg";
            string filePath = this.webHostEnvironment.WebRootPath + fileFolderAndName;

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await input.Picture.CopyToAsync(stream);
            }

            if (input.Type == "ProfilePicture")
            {
                await this.picturesService.CreateProfilePicture(input, fileFolderAndName, userId);
            }
            else if (input.Type == "CoverPhoto")
            {
                await this.picturesService.CreateCoverPhoto(input, fileFolderAndName, userId);
            }

            return this.Redirect("/Users/Profile");
        }
    }
}
