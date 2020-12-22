namespace WindowToTheSociety.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using WindowToTheSociety.Data.Common.Repositories;

    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Web.ViewModels.Users;

    public class UsersService : IUsersSurvice
    {
        private readonly IRepository<ApplicationUser> usersRepository;
        private readonly IRepository<Photo> photosRepository;
        private readonly IRepository<Post> postRepository;

        public UsersService(IRepository<ApplicationUser> users, IRepository<Photo> photosRepository, IRepository<Post> postRepository)
        {
            this.usersRepository = users;
            this.photosRepository = photosRepository;
            this.postRepository = postRepository;
        }

        public ListAllUsersViewModel GetAllUsersViewModel(string userId)
        {
            List<ApplicationUser> users = this.usersRepository.All().Where(x => x.Id != userId).ToList();
            ListAllUsersViewModel viewModel = new ListAllUsersViewModel();
            viewModel.AllUsers = new List<AllUsersViewModel>();

            foreach (var user in users)
            {
                List<Photo> photos = this.photosRepository.All().Where(x => x.ApplicationUserId == user.Id).ToList();
                AllUsersViewModel userViewModel = new AllUsersViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    Surname = user.Surname,
                };

                if (photos.Count() != 0)
                {
                    if (photos.Contains(photos.FirstOrDefault(x => (int)x.PhotoType == 1)))
                    {
                        userViewModel.ProfilePictureUrl = photos.FirstOrDefault(x => x.PhotoType == (PhotoType)1).PictureUrl;
                    }

                    userViewModel.Photos = photos;
                }

                viewModel.AllUsers.Add(userViewModel);
            }

            return viewModel;
        }

        public UsersProfileViewModel GetProfileViewModelById(string userId)
        {
            ApplicationUser user = this.usersRepository.All().FirstOrDefault(x => x.Id == userId);
            List<Post> posts = this.postRepository.All().Where(x => x.ApplicationUserId == userId).ToList();
            List<Photo> photos = this.photosRepository.All().Where(x => x.ApplicationUserId == userId).ToList();

            UsersProfileViewModel viewModel = new UsersProfileViewModel
            {
                FirstName = user.FirstName,
                Surname = user.Surname,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                Photos = photos,
            };

            if (posts != null)
            {
               viewModel.Posts = posts;
            }

            if (photos.Count != 0)
            {
                if (photos.Contains(photos.FirstOrDefault(x => (int)x.PhotoType == 1)))
                {
                    viewModel.ProfilePictureUrl = photos.FirstOrDefault(x => (int)x.PhotoType == 1).PictureUrl;
                }

                if (photos.Contains(photos.FirstOrDefault(x => (int)x.PhotoType == 2)))
                {
                    viewModel.CoverPhtotoUrl = photos.FirstOrDefault(x => (int)x.PhotoType == 2).PictureUrl;
                }
            }

            return viewModel;
        }

        public ApplicationUser GetUserById(string userId)
        {
            ApplicationUser user = this.usersRepository.All().FirstOrDefault(x => x.Id == userId);

            return user;
        }
    }
}
