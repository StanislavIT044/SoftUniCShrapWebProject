namespace WindowToTheSociety.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using WindowToTheSociety.Web.ViewModels.Pages;
    using WindowToTheSociety.Web.ViewModels.Users;

    public class SearchViewModel
    {
        public ICollection<AllUsersViewModel> Users { get; set; }

        public ICollection<SelectPageViewModel> Pages { get; set; }
    }
}
