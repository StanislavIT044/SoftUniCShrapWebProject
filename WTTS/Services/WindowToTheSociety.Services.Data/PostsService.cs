namespace WindowToTheSociety.Services.Data
{
    using System.Threading.Tasks;

    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;

    public class PostsService : IPostsService
    {
        private readonly IRepository<ApplicationUser> usersRepository;
        private readonly IRepository<Photo> photosRepository;

        public PostsService(IRepository<ApplicationUser> usersRepository, IRepository<Photo> photosRepository)
        {
            this.usersRepository = usersRepository;
            this.photosRepository = photosRepository;
        }

        public async Task CreatePost() // TODO: Add in interface
        {

        }
    }
}
