using NuGet.RuntimeModel;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;

namespace QPR_Application.Repository
{
    public interface IManageUserRepo
    {
        public Task<IEnumerable<AllUsersViewModel>> GetAllUsers();
        public Task<IEnumerable<registration>> GetAllUsersForPasswordHashing();
        public Task<AllUsersViewModel> GetUserDetails(string Id);
        //public Task<registration> GetEditUserDetails(string Id);
        public Task<EditUserViewModel> GetEditUserDetails(string Id);
        public Task<Boolean> CreateUser(CreateUserViewModel createUserVM);
        public Task EditUser(EditUserViewModel User);
    }
}
