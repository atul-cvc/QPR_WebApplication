using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public class LoginRepo : ILoginRepo
    {
        private readonly QPRContext _dbContext;
        public LoginRepo(QPRContext DbContext)
        {
            _dbContext = DbContext;
        }
        public async Task<registration> Login(Login user)
        {
            var USER = await _dbContext.registration.FirstOrDefaultAsync(i => i.userid == user.Username);
            if (USER != null)
            {
                if (user.Password == USER.password)
                {
                    return USER;
                }
            }
            return null;
        }
    }
}
