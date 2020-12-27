namespace WindowToTheSociety.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Services.Data;
    using WindowToTheSociety.Web.ViewModels;
    using WindowToTheSociety.Web.ViewModels.Posts;
    using WindowToTheSociety.Web.ViewModels.Search;

    public class HomeController : BaseController
    {
        private readonly ISearchService searchService;
        private readonly IPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(
            ISearchService searchService,
            IPostsService postsService, 
            UserManager<ApplicationUser> userManager)
        {
            this.searchService = searchService;
            this.postsService = postsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            string currentUserId = this.userManager.GetUserId(this.User);
            ListPostsInHomePageViewModel viewModel = this.postsService.GetPostsForHomePage(currentUserId);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Search()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Search(string searchStr)
        {
            SearchViewModel viewModel = this.searchService.Search(searchStr);

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
