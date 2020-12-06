namespace WindowToTheSociety.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Web.ViewModels.Photos;

    public class PhotosService : IPhotosService
    {
        private readonly IRepository<ApplicationUser> usersRepository;
        private readonly IUsersSurvice usersSurvice;
        private readonly IRepository<Photo> photosRepository;

        public PhotosService(IRepository<ApplicationUser> usersRepository, IRepository<Photo> photosRepository, IUsersSurvice usersSurvice)
        {
            this.usersRepository = usersRepository;
            this.photosRepository = photosRepository;
            this.usersSurvice = usersSurvice;
        }

        public async Task CreatePhoto(string filePath, string userId, PhotoType type)
        {
            Photo photo = new Photo
            {
                PictureUrl = filePath,
                PhotoType = type,
                CreatedOn = DateTime.UtcNow,
                ApplicationUserId = userId,
            };

            await this.photosRepository.AddAsync(photo);
            await this.AppendPhoto(photo, userId);
            await this.photosRepository.SaveChangesAsync();
        }

        private async Task AppendPhoto(Photo photo, string userId)
        {
            Photo oldPhoto = this.photosRepository.All().FirstOrDefault(x => x.ApplicationUserId == userId && x.PhotoType == photo.PhotoType);
            if (oldPhoto != null)
            {
                this.photosRepository.Delete(oldPhoto);
            }

            ApplicationUser user = this.usersSurvice.GetUserById(userId);

            user.Photos.Add(photo);
            await this.usersRepository.SaveChangesAsync();
        }
    }
}
