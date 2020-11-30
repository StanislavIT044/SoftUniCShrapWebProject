namespace WindowToTheSociety.Services.Data
{
    using WindowToTheSociety.Web.ViewModels.Users;

    public interface IUsersSurvice
    {
        UsersProfileViewModel GetById(string userId);
    }
}
