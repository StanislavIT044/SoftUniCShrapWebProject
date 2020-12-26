namespace WindowToTheSociety.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Services.Data;

    public class FollowController : Controller
    {
        private readonly IFollowService followService;
        private readonly UserManager<ApplicationUser> userManager;

        public FollowController(IFollowService followService, UserManager<ApplicationUser> userManager)
        {
            this.followService = followService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Follow(string id)
        {
            string currentUser = this.userManager.GetUserId(this.User);
            await this.followService.Follow(currentUser, id);

            return this.Redirect($"/Users/Profile/{id}");
        }

        public async Task<IActionResult> Unfollow(string id)
        {
            string currentUser = this.userManager.GetUserId(this.User);
            await this.followService.Unfollow(currentUser, id);

            return this.Redirect("/");
        }
    }
}
