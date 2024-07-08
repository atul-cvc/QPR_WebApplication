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
        public async Task<Login> Login(Login user)
        {
            var USER = await _dbContext.Login.FirstOrDefaultAsync(i => i.Username == user.Username);
            if (USER != null)
            {
                if (user.Password == USER.Password)
                {
                    return USER;
                }
            }
            return null;
        }
    }
}
