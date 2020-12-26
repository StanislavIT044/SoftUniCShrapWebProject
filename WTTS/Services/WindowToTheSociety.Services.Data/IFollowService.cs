namespace WindowToTheSociety.Services.Data
{
    using System.Threading.Tasks;

    public interface IFollowService
    {
        Task Follow(string currentUser, string id);

        Task Unfollow(string currentUser, string id);
    }
}
