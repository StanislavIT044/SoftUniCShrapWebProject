namespace WindowToTheSociety.Web.Controllers
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Services.Data;
    using WindowToTheSociety.Web.ViewModels.Users;

    public class UsersController : Controller
    {
        private readonly IUsersSurvice usersSurvice;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UsersController(IUsersSurvice usersSurvice, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.usersSurvice = usersSurvice;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Profile()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.View("Error");
            }

            string userId = this.userManager.GetUserId(this.User);
            UsersProfileViewModel viewModel = this.usersSurvice.GetById(userId);

            return this.View(viewModel);
        }

        public IActionResult AddPicture()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.View("Error");
            }

            return this.View();
        }

        [HttpPost]
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

            string filePath = this.webHostEnvironment.WebRootPath + $"/{input.Type}s" + $"/{this.userManager.GetUserId(this.User)}.jpg";

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await input.Picture.CopyToAsync(stream);
            }

            return this.Redirect("Profile");
        }
    }
}
