namespace WindowToTheSociety.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Web.ViewModels.Users;

    public class PicturesService : IPicturesService
    {
        private readonly IRepository<ProfilePicture> profilePicturesRepository;
        private readonly IRepository<CoverPhoto> coverPhotoRepository;
        private readonly IUsersSurvice usersSurvice;

        public PicturesService(IRepository<ProfilePicture> profilePicturesRepository, IRepository<CoverPhoto> coverPhotoRepository, IUsersSurvice usersSurvice)
        {
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
            await this.usersSurvice.AppendProfilePicture(picture.Id, userId);
            await this.profilePicturesRepository.SaveChangesAsync();
        }

        public async Task CreateCoverPhoto()
        {
            throw new NotImplementedException();
        }
    }
}
