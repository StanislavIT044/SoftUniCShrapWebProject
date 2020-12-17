namespace WindowToTheSociety.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;
    using WindowToTheSociety.Services.Mapping;
    using WindowToTheSociety.Web.ViewModels.Pages;

    public class PagesService : IPagesService
    {
        private readonly IRepository<Page> pageRepository;
        private readonly IRepository<Photo> photosRepository;
        private readonly IRepository<ApplicationUser> usersRepository;

        public PagesService(IRepository<Page> pageRepository, IRepository<Photo> photosRepository, IRepository<ApplicationUser> usersRepository)
        {
            this.pageRepository = pageRepository;
            this.photosRepository = photosRepository;
            this.usersRepository = usersRepository;
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

        public SelectPagesViewModel GetSelectPagesViewModel(string userId)
        {
            List<Page> usersPages = this.pageRepository
                .All()
                .Where(x => x.ApplicationUserId == userId)
                .ToList();

            SelectPagesViewModel pages = new SelectPagesViewModel();
            pages.AllPages = new List<SelectPageViewModel>();

            foreach (var page in usersPages)
            {
                Photo photo = this.photosRepository.All().FirstOrDefault(x => x.PageId == page.Id);

                SelectPageViewModel modelPage = new SelectPageViewModel()
                {
                    Id = page.Id,
                    Title = page.Title,
                };

                if (photo != null)
                {
                    modelPage.CoverPhotoUrl = photo.PictureUrl;
                }

                pages.AllPages.Add(modelPage);
            }

            return pages;
        }

        public PageViewModel GetPageViewModel(string pageId)
        {
            Page page = this.pageRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Id == pageId);
            Photo photo = this.photosRepository.AllAsNoTracking().FirstOrDefault(x => x.PageId == pageId);
            ApplicationUser user = this.usersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == page.ApplicationUserId);

            PageViewModel pageViewModel = new PageViewModel
            {
                Id = page.Id,
                Title = page.Title,
                ApplicationUser = user,
                CreatedOn = page.CreatedOn,
                Posts = page.Posts,
            };

            if (photo != null)
            {
                pageViewModel.CoverPhotoUrl = photo.PictureUrl;
            }

            return pageViewModel;
        }
    }
}
