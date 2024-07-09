using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public interface IUserRepo
    {
        public Task<IEnumerable<registration>> GetAllUsers();
        public Task<registration> GetUserDetails(string Id);
    }
}
