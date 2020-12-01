namespace WindowToTheSociety.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class AddPictureInputModel
    {
        [Required]
        public IFormFile Picture { get; set; }

        public string Type { get; set; }
    }
}
