namespace WindowToTheSociety.Services.Data
{
    using WindowToTheSociety.Web.ViewModels;

    public interface IUsersSurvice
    {
        ProfileViewModel FindUserById(string userId);
    }
}
