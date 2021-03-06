﻿namespace WindowToTheSociety.Services.Data
{
    using WindowToTheSociety.Data.Models;

    using WindowToTheSociety.Web.ViewModels.Users;

    public interface IUsersSurvice
    {
        ListAllUsersViewModel GetAllUsersViewModel(string userId);

        UsersProfileViewModel GetProfileViewModelById(string userId);

        ApplicationUser GetUserById(string userId);
    }
}
