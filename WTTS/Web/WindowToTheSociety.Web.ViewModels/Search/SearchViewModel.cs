namespace WindowToTheSociety.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using WindowToTheSociety.Data.Models;

    public class SearchViewModel
    {
        public ICollection<ApplicationUser> Users { get; set; }

        public ICollection<Page> Pages { get; set; }
    }
}
