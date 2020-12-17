namespace WindowToTheSociety.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;

    using WindowToTheSociety.Data.Models;

    public class UsersProfileViewModel
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string CoverPhtotoUrl { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
