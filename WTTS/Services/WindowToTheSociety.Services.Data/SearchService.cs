namespace WindowToTheSociety.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Web.ViewModels.Search;

    public class SearchService : ISearchService
    {
        private readonly IRepository<ApplicationUser> usersRepository;
        private readonly IRepository<Page> pagesRepository;
        private readonly IRepository<Photo> photosRepository;

        public SearchService(
            IRepository<ApplicationUser> usersRepository,
            IRepository<Page> pagesRepository,
            IRepository<Photo> photosRepository)
        {
            this.usersRepository = usersRepository;
            this.pagesRepository = pagesRepository;
            this.photosRepository = photosRepository;
        }

        public SearchViewModel Search(string searchStr)
        {
            SearchViewModel viewModel = new SearchViewModel();

            string[] tokens = searchStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            viewModel.Users = this.GetUsers(tokens);
            viewModel.Pages = new List<Page>();

            return viewModel;
        }

        private ICollection<ApplicationUser> GetUsers(string[] tokens)
        {
            ICollection<ApplicationUser> users = this.usersRepository.All().ToList();

            foreach (var token in tokens)
            {
                users.Where(x => x.FirstName.Contains(token) || x.Surname.Contains(token));
            }

            return users;
        }

        private ICollection<Page> GetPages(string searchStr)
        {
            return null;
        }
    }
}
