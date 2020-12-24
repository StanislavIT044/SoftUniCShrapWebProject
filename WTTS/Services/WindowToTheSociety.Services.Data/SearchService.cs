namespace WindowToTheSociety.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Web.ViewModels.Pages;
    using WindowToTheSociety.Web.ViewModels.Search;
    using WindowToTheSociety.Web.ViewModels.Users;

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

            if (searchStr == null)
            {
                viewModel.Users = new List<AllUsersViewModel>();
                viewModel.Pages = new List<SelectPageViewModel>();
                return viewModel;
            }

            string[] tokens = searchStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            viewModel.Users = this.GetUsers(tokens);
            viewModel.Pages = this.GetPages(tokens);

            return viewModel;
        }

        private ICollection<AllUsersViewModel> GetUsers(string[] tokens)
        {
            List<AllUsersViewModel> users = this.usersRepository.All().Select(x => new AllUsersViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                Surname = x.Surname,
            }).ToList();
            List<AllUsersViewModel> matchedUsers = new List<AllUsersViewModel>();

            foreach (var user in users)
            {
                foreach (var token in tokens)
                {
                    if (user.FirstName.Contains(token) || user.Surname.Contains(token))
                    {
                        if (!matchedUsers.Contains(user))
                        {
                            if (this.photosRepository.All().Contains(this.photosRepository.All().FirstOrDefault(x => x.PhotoType == (PhotoType)1 && x.ApplicationUserId == user.Id)))
                            {
                                user.ProfilePictureUrl = this.photosRepository.All().FirstOrDefault(x => x.PhotoType == (PhotoType)1 && x.ApplicationUserId == user.Id).PictureUrl;
                            }

                            matchedUsers.Add(user);
                        }
                    }
                }
            }

            return matchedUsers;
        }

        private ICollection<SelectPageViewModel> GetPages(string[] tokens)
        {
            List<SelectPageViewModel> pages = this.pagesRepository.All().Select(x => new SelectPageViewModel
            {
                Id = x.Id,
                Title = x.Title,
            }).ToList();
            List<SelectPageViewModel> matchedPages = new List<SelectPageViewModel>();

            foreach (var page in pages)
            {
                foreach (var token in tokens)
                {
                    if (page.Title.Contains(token))
                    {
                        if (!matchedPages.Contains(page))
                        {
                            if (this.photosRepository.All().Contains(this.photosRepository.All().FirstOrDefault(x => x.PhotoType == (PhotoType)2 && x.PageId == page.Id)))
                            {
                                page.CoverPhotoUrl = this.photosRepository.All().FirstOrDefault(x => x.PhotoType == (PhotoType)2 && x.PageId == page.Id).PictureUrl;
                            }

                            matchedPages.Add(page);
                        }
                    }
                }
            }

            return matchedPages;
        }
    }
}
