namespace WindowToTheSociety.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;

    public class PhotosService : IPhotosService
    {
        private readonly IRepository<ApplicationUser> usersRepository;
        private readonly IUsersSurvice usersSurvice;
        private readonly IRepository<Page> pageRepository;
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<Photo> photosRepository;

        public PhotosService(IRepository<ApplicationUser> usersRepository, IRepository<Photo> photosRepository, IUsersSurvice usersSurvice, IRepository<Page> pageRepository, IRepository<Post> postRepository)
        {
            this.usersRepository = usersRepository;
            this.photosRepository = photosRepository;
            this.usersSurvice = usersSurvice;
            this.pageRepository = pageRepository;
            this.postRepository = postRepository;
        }

        public async Task AppendPhoto(string filePath, string userId, string pageId, PhotoType type)
        {
            Photo oldPhoto = this.photosRepository.All().FirstOrDefault(x => x.ApplicationUserId == userId && x.PhotoType == type);
            Photo photo = new Photo();

            if (userId != null)
            {
                photo = await this.CreatePhoto(filePath, userId, null, type);
            }

            if (pageId != null)
            {
                photo = await this.CreatePhoto(filePath, null, pageId, type);
            }

            if (photo.PhotoType == (PhotoType)1 || photo.PhotoType == (PhotoType)2)
            {
                if (oldPhoto != null)
                {
                    this.photosRepository.Delete(oldPhoto);
                }
            }

            if (userId != null)
            {
                ApplicationUser user = this.usersSurvice.GetUserById(userId);
                user.Photos.Add(photo);
                await this.usersRepository.SaveChangesAsync();
            }

            if (pageId != null)
            {
                List<Page> pages = this.pageRepository.All().Where(x => x.Id == pageId).ToList();
                Page page = this.pageRepository.All().Where(x => x.Id == pageId).FirstOrDefault(); // TODO: repair page is null
                List<Post> posts = this.postRepository.All().Where(x => x.PageId == pageId).ToList();
                posts.FirstOrDefault(x => x.CreatedOn.ToString("f") == DateTime.UtcNow.ToString("f")).PhotoId = photo.Id;
                page.Posts = posts;
                await this.pageRepository.SaveChangesAsync();
            }
        }

        public async Task<Photo> CreatePhoto(string filePath, string userId, string pageId, PhotoType type)
        {
            Photo photo = new Photo
            {
                PictureUrl = filePath,
                PhotoType = type,
                CreatedOn = DateTime.UtcNow,
                ApplicationUserId = userId,
                PageId = pageId,
            };

            await this.photosRepository.AddAsync(photo);
            await this.photosRepository.SaveChangesAsync();

            return photo;
        }
    }
}
