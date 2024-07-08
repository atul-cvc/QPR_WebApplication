using QPR_Application.Models.Entities;
using QPR_Application.Models.Utility;

namespace QPR_Application.Repository
{
    public interface IUserRepo
    {
        public Task<IEnumerable<registration>> GetAllUsers();
        public Task<registration> GetUserDetails(string Id);
    }
}
