namespace WindowToTheSociety.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Photo
    {
        public Photo()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
