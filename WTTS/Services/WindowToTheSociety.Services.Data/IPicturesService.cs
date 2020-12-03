namespace WindowToTheSociety.Services.Data
{
    using System.Threading.Tasks;

    using WindowToTheSociety.Data.Models;

    using WindowToTheSociety.Web.ViewModels.Users;

    public interface IPicturesService
    {
        Task CreateProfilePicture(AddPictureInputModel input, string filePath, string userId);

        Task CreateCoverPhoto();
    }
}
