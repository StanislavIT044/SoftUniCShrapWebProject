namespace WindowToTheSociety.Services.Data
{
    using System.Threading.Tasks;

    public interface IPagesService
    {
        Task CreatePage(string title, string pictureUrl, string userId);
    }
}
