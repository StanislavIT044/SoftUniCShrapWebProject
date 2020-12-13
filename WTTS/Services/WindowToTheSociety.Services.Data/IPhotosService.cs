namespace WindowToTheSociety.Services.Data
{
    using System.Threading.Tasks;

    using WindowToTheSociety.Data.Models;

    public interface IPhotosService
    {
        Task AppendPhoto(string filePath, string userId, PhotoType type);

        Task<Photo> CreatePhoto(string filePath, string userId, string pageId, PhotoType type);
    }
}
