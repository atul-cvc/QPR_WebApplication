using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;
using QPR_Application.Models.Utility;

namespace QPR_Application.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly QPRContext _dbContext;
        public UserRepo(QPRContext DbContext)
        {
            _dbContext = DbContext;
        }
        public async Task<IEnumerable<registration>> GetAllUsers()
        {
            return await _dbContext.registration.ToListAsync();
        }

        public async Task<registration> GetUserDetails(string Id)
        {
            return await _dbContext.registration.FirstOrDefaultAsync(u => u.usercode == Convert.ToInt64(Id));
        }
    }
}
