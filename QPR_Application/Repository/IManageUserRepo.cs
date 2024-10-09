﻿using NuGet.RuntimeModel;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;

namespace QPR_Application.Repository
{
    public interface IManageUserRepo
    {
        public Task<IEnumerable<AllUsersViewModel>> GetAllUsers();
        public Task<AllUsersViewModel> GetUserDetails(string Id);
        public Task<registration> GetEditUserDetails(string Id);
        public Boolean CreateUser(registration registration);
        public void EditUser(registration registration);
    }
}
