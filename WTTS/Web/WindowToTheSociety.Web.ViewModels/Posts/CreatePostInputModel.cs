namespace WindowToTheSociety.Web.ViewModels.Posts
{
    using Microsoft.AspNetCore.Http;

    public class CreatePostInputModel
    {
        public string Text { get; set; }

        public IFormFile Photo { get; set; }
    }
}
