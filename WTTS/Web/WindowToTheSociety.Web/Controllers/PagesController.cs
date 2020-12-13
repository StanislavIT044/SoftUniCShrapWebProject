namespace WindowToTheSociety.Web.Controllers
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Services.Data;
    using WindowToTheSociety.Web.ViewModels.Pages;

    public class PagesController : Controller
    {
        private readonly IPagesService pagesService;
        private readonly IPhotosService photosService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PagesController(IPagesService pagesService, IPhotosService photosService, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.pagesService = pagesService;
            this.photosService = photosService;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult PagesMenu()
        {
            return this.View();
        }

        public IActionResult CreatePage()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePage(CreatePageInputModel input)
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
            string fileFolderAndName = $"/PagesCoverPhotos" + $"/{input.Title}.jpg";
            string filePath = this.webHostEnvironment.WebRootPath + fileFolderAndName;

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await input.Picture.CopyToAsync(stream);
            }

            await this.pagesService.CreatePage(input.Title, userId);

            if (input.Picture != null)
            {
                string pageId = this.pagesService.GetIdByTitle(input.Title);
                await this.photosService.CreatePhoto(fileFolderAndName, null, pageId, (PhotoType)2);
                await this.pagesService.AppendPicture(input.Title, fileFolderAndName);
            }

            return this.Redirect("/");
        }
    }
}
