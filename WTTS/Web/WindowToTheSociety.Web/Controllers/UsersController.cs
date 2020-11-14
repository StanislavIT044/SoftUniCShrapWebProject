namespace WindowToTheSociety.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        public IActionResult Profile(string userId)
        {
            return this.View();
        }
    }
}
