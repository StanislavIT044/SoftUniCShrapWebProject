namespace WindowToTheSociety.Services.Data
{
    using System.Linq;

    using WindowToTheSociety.Data.Common.Repositories;

    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Web.ViewModels.Users;

    public class UsersService : IUsersSurvice
    {
        private readonly IRepository<ApplicationUser> usersRepository;

        public UsersService(IRepository<ApplicationUser> users)
        {
            this.usersRepository = users;
        }

        public UsersProfileViewModel GetProfileViewModelById(string userId)
        {
            UsersProfileViewModel viewModel = this.usersRepository
                .AllAsNoTracking()
                .Where(x => x.Id == userId)
                .Select(x => new UsersProfileViewModel
                {
                    FirstName = x.FirstName,
                    Surname = x.Surname,
                    BirthDate = x.BirthDate,
                    Gender = x.Gender,
                    ProfilePictureUrl = x.Photos.FirstOrDefault(x => (int)x.PhotoType == 1).PictureUrl,
                    CoverPhtotoUrl = x.Photos.FirstOrDefault(x => (int)x.PhotoType == 2).PictureUrl,
                })
                .FirstOrDefault();

            return viewModel;
        }

        public ApplicationUser GetUserById(string userId)
        {
            ApplicationUser user = this.usersRepository.All().FirstOrDefault(x => x.Id == userId);

            return user;
        }
    }
}
