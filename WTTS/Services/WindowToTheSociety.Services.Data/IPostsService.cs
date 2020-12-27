namespace WindowToTheSociety.Services.Data
{
    using System.Threading.Tasks;

    using WindowToTheSociety.Web.ViewModels.Posts;

    public interface IPostsService
    {
        Task CreatePost(string photoPath, string text, string userId, string pageId);

        ListPostsInHomePageViewModel GetPostsForHomePage(string currentUserId);
    }
}
