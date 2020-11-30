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
            string userId = this.userManager.GetUserId(this.User);
            UsersProfileViewModel viewModel = this.usersSurvice.GetById(userId);

            return this.View(viewModel);
        }
    }
}
