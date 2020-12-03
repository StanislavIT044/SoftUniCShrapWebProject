﻿namespace WindowToTheSociety.Services.Data
{
    using System.Threading.Tasks;

    using WindowToTheSociety.Data.Models;

    using WindowToTheSociety.Web.ViewModels.Users;

    public interface IUsersSurvice
    {
        UsersProfileViewModel GetById(string userId);

        Task AppendProfilePicture(string pictureId, string userId);
    }
}