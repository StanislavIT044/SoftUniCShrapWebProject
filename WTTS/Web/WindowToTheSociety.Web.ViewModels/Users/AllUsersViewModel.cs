namespace WindowToTheSociety.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;

    using WindowToTheSociety.Data.Models;

    public class AllUsersViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string ProfilePictureUrl { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
