namespace WindowToTheSociety.Web.ViewModels.Pages
{
    using Microsoft.AspNetCore.Http;

    public class CreatePageInputModel
    {
        public string Title { get; set; }

        public IFormFile Picture { get; set; }
    }
}
