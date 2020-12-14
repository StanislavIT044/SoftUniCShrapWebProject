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
            string userId = this.userManager.GetUserId(this.User);
            SelectPagesViewModel pages = this.pagesService.GetSelectPagesViewModel(userId);

            return this.View(pages);
        }

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

            return this.Redirect("/");
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
    }
}
