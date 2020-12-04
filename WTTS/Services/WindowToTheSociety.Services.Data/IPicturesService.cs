namespace WindowToTheSociety.Services.Data
{
    using System.Threading.Tasks;

    using WindowToTheSociety.Web.ViewModels.Pictures;

    public interface IPicturesService
    {
        Task CreateProfilePicture(AddPictureInputModel input, string filePath, string userId);

        Task CreateCoverPhoto(AddPictureInputModel input, string filePath, string userId);
    }
}
