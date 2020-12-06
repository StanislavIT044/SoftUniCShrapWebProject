namespace WindowToTheSociety.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using WindowToTheSociety.Data.Common.Repositories;
    using WindowToTheSociety.Data.Models;

    public class PostsService : IPostsService
    {
        private readonly IRepository<ApplicationUser> usersRepository;
        private readonly IRepository<Photo> photosRepository;
        private readonly IRepository<Post> postRepository;

        public PostsService(IRepository<ApplicationUser> usersRepository, IRepository<Post> postRepository, IRepository<Photo> photosRepository)
        {
            this.usersRepository = usersRepository;
            this.photosRepository = photosRepository;
            this.postRepository = postRepository;
        }

        public async Task CreatePost(string photoPath, string text, string userId)
        {
            Photo photo = this.photosRepository.All().FirstOrDefault(x => x.PictureUrl == photoPath);

            Post post = new Post
            {
                Text = text,
                PhotoId = photo.Id,
                ApplicationUserId = userId,
                CreatedOn = DateTime.UtcNow,
            };

            await this.postRepository.AddAsync(post);
            await this.postRepository.SaveChangesAsync();
        }
    }
}
