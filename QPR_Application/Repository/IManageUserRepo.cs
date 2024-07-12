using NuGet.RuntimeModel;
using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public interface IManageUserRepo
    {
        public Task<IEnumerable<registration>> GetAllUsers();
        public Task<registration> GetUserDetails(string Id);
        public Boolean CreateUser(registration registration);
        public void EditUser(registration registration);
    }
}
