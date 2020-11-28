namespace WindowToTheSociety.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WindowToTheSociety.Services.Data;

    public class UsersController : Controller
    {
        private readonly IUsersSurvice usersSurvice;

        public UsersController(IUsersSurvice usersSurvice)
        {
            this.usersSurvice = usersSurvice;
        }

        public IActionResult Profile(string userId)
        {
            return this.View();
        }
    }
}
