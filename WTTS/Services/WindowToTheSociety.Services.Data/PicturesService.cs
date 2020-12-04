namespace WindowToTheSociety.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Web.ViewModels.Pictures;

    public class PicturesService : IPicturesService
    {
        private readonly IRepository<ApplicationUser> usersRepository;
        private readonly IRepository<ProfilePicture> profilePicturesRepository;
        private readonly IRepository<CoverPhoto> coverPhotoRepository;
        private readonly IUsersSurvice usersSurvice;

        public PicturesService(IRepository<ApplicationUser> usersRepository, IRepository<ProfilePicture> profilePicturesRepository, IRepository<CoverPhoto> coverPhotoRepository, IUsersSurvice usersSurvice)
        {
            this.usersRepository = usersRepository;
            this.profilePicturesRepository = profilePicturesRepository;
            this.coverPhotoRepository = coverPhotoRepository;
            this.usersSurvice = usersSurvice;
        }

        public async Task CreateProfilePicture(AddPictureInputModel input, string filePath, string userId)
        {
            ProfilePicture picture = new ProfilePicture
            {
                PictureUrl = filePath,
                CreatedOn = DateTime.UtcNow,
                ApplicationUserId = userId,
            };

            await this.profilePicturesRepository.AddAsync(picture);
            await this.AppendProfilePicture(picture.Id, userId);
            await this.profilePicturesRepository.SaveChangesAsync();
        }

        public async Task CreateCoverPhoto(AddPictureInputModel input, string filePath, string userId)
        {
            CoverPhoto picture = new CoverPhoto
            {
                PictureUrl = filePath,
                CreatedOn = DateTime.UtcNow,
                ApplicationUserId = userId,
            };

            await this.coverPhotoRepository.AddAsync(picture);
            await this.AppendCoverPhoto(picture.Id, userId);
            await this.profilePicturesRepository.SaveChangesAsync();
        }

        public async Task AppendProfilePicture(string pictureId, string userId)
        {
            ProfilePicture picture = this.profilePicturesRepository.All().Where(x => x.ApplicationUserId == userId).FirstOrDefault();
            if (picture != null)
            {
                this.profilePicturesRepository.Delete(picture);
            }

            ApplicationUser user = this.usersSurvice.GetUserById(userId);

            user.ProfilePictureId = pictureId;
            await this.usersRepository.SaveChangesAsync();
        }

        public async Task AppendCoverPhoto(string pictureId, string userId)
        {
            CoverPhoto photo = this.coverPhotoRepository.All().Where(x => x.ApplicationUserId == userId).FirstOrDefault();
            if (photo != null)
            {
                this.coverPhotoRepository.Delete(photo);
            }

            ApplicationUser user = this.usersSurvice.GetUserById(userId);

            user.CoverPhotoId = pictureId;
            await this.usersRepository.SaveChangesAsync();
        }
    }
}
