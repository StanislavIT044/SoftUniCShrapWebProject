namespace WindowToTheSociety.Web.ViewModels.Pages
{
    using System;
    using System.Collections.Generic;

    using WindowToTheSociety.Data.Models;

    public class PageViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string PhotoId { get; set; }

        public string CoverPhotoUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
