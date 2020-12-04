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
    using WindowToTheSociety.Web.ViewModels.Users;

    public class UsersController : Controller
    {
        private readonly IUsersSurvice usersSurvice;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(IUsersSurvice usersSurvice, IPicturesService picturesService, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.usersSurvice = usersSurvice;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Profile()
        {
            string userId = this.userManager.GetUserId(this.User);
            UsersProfileViewModel viewModel = this.usersSurvice.GetProfileViewModelById(userId);

            return this.View(viewModel);
        }
    }
}
