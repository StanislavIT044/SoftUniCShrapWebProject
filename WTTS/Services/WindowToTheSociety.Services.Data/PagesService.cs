namespace WindowToTheSociety.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;

    public class PagesService : IPagesService
    {
        private readonly IRepository<Page> pageRepository;

        public PagesService(IRepository<Page> pageRepository)
        {
            this.pageRepository = pageRepository;
        }

        public async Task CreatePage(string title, string pictureUrl, string userId)
        {
            Page page = new Page
            {
                Title = title,
                ApplicationUserId = userId,
            };

            await this.pageRepository.AddAsync(page);
            await this.pageRepository.SaveChangesAsync();
        }

        private void AppendPicture(string title)
        {
            Page page = this.pageRepository.AllAsNoTracking().FirstOrDefault(x => x.Title == title);
        }
    }
}
