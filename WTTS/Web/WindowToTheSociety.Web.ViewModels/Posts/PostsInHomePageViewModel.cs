namespace WindowToTheSociety.Web.ViewModels.Posts
{
    using System;

    using WindowToTheSociety.Data.Models;

    public class PostsInHomePageViewModel
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public string PhotoId { get; set; }

        public Photo Photo { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string UsersProfilePictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
