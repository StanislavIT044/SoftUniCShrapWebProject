namespace WindowToTheSociety.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WindowToTheSociety.Services.Data;
    using WindowToTheSociety.Web.ViewModels;
    using WindowToTheSociety.Web.ViewModels.Search;

    public class HomeController : BaseController
    {
        private readonly ISearchService searchService;

        public HomeController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return this.View();
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
