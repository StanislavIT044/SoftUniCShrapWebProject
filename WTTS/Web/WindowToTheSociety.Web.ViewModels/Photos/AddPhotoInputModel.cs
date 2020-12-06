namespace WindowToTheSociety.Web.ViewModels.Photos
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using WindowToTheSociety.Data.Models;

    public class AddPhotoInputModel
    {
        [Required]
        public IFormFile Picture { get; set; }

        public PhotoType Type { get; set; }
    }
}
