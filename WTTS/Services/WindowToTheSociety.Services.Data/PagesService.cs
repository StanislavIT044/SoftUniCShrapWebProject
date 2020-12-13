namespace WindowToTheSociety.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;

    public class PagesService : IPagesService
    {
        private readonly IRepository<Page> pageRepository;
        private readonly IRepository<Photo> photosRepository;

        public PagesService(IRepository<Page> pageRepository, IRepository<Photo> photosRepository)
        {
            this.pageRepository = pageRepository;
            this.photosRepository = photosRepository;
        }

        public async Task CreatePage(string title, string userId)
        {
            Page page = new Page
            {
                Title = title,
                ApplicationUserId = userId,
            };

            await this.pageRepository.AddAsync(page);
            await this.pageRepository.SaveChangesAsync();
        }

        public async Task AppendPicture(string title, string pictureUrl)
        {
            Page page = this.pageRepository.All().FirstOrDefault(x => x.Title == title);
            Photo photo = this.photosRepository.AllAsNoTracking().FirstOrDefault(x => x.PictureUrl == pictureUrl);

            page.PhotoId = photo.Id;

            this.pageRepository.Update(page);
            await this.pageRepository.SaveChangesAsync();
        }

        public string GetIdByTitle(string title)
        {
            Page page = this.pageRepository.AllAsNoTracking().FirstOrDefault(x => x.Title == title);

            return page.Id;
        }
    }
}
