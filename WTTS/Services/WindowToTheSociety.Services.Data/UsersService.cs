namespace WindowToTheSociety.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

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
                    ProfilePictureUrl = x.ProfilePicture.PictureUrl,
                    CoverPhtotoUrl = x.CoverPhoto.PictureUrl,
                })
                .FirstOrDefault();

            return viewModel;
        }

        public async Task AppendProfilePicture(string pictureId, string userId)
        {
            ApplicationUser user = this.GetUserById(userId);

            user.ProfilePictureId = pictureId;
            await this.usersRepository.SaveChangesAsync();
        }

        public async Task AppendCoverPhoto(string pictureId, string userId)
        {
            ApplicationUser user = this.GetUserById(userId);

            user.CoverPhotoId = pictureId;
            await this.usersRepository.SaveChangesAsync();
        }

        public ApplicationUser GetUserById(string userId)
        {
            ApplicationUser user = this.usersRepository.All().First(x => x.Id == userId);

            return user;
        }
    }
}
