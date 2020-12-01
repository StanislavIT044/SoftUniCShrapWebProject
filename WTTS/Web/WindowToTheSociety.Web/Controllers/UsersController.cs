namespace WindowToTheSociety.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Services.Data;
    using WindowToTheSociety.Web.ViewModels.Users;

    public class UsersController : Controller
    {
        private readonly IUsersSurvice usersSurvice;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(IUsersSurvice usersSurvice, UserManager<ApplicationUser> userManager)
        {
            this.usersSurvice = usersSurvice;
            this.userManager = userManager;
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
    }
}
