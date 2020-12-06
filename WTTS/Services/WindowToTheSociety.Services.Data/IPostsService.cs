namespace WindowToTheSociety.Services.Data
{
    using System.Threading.Tasks;

    public interface IPostsService
    {
        Task CreatePost(string photoPath, string text, string userId);
    }
}
