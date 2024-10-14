using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Util;


namespace QPR_Application.Repository
{
    public class LoginRepo : ILoginRepo
    {
        private readonly QPRContext _dbContext;
        private readonly ILogger<LoginRepo> _logger;
        public LoginRepo(ILogger<LoginRepo> logger, QPRContext DbContext)
        {
            _dbContext = DbContext;
            _logger = logger;
        }
        public async Task<UserDetails> Login(Login user)
        {
            try
            {
                UserDetails UserDetails = new UserDetails();
                Boolean passwordVerified = false;
                //UserDetails.User = await _dbContext.registration.FirstOrDefaultAsync(i => i.userid == user.Username && user.Password == i.password && i.status == "t" && i.islocked == "0");
                UserDetails.User = await _dbContext.registration.FirstOrDefaultAsync(i => i.userid == user.Username && i.status == "t" && i.islocked == "0");
            
                if (UserDetails.User != null)
                {
                    passwordVerified = new PasswordHashingUtil().VerifyUserPassword(user, UserDetails.User);
                    if (passwordVerified)
                    {
                        UserDetails.OrgDetails = await _dbContext.tbl_MasterMinistryNew.FirstOrDefaultAsync(i => i.OrgName == UserDetails.User.organisation);
                        UserDetails.OrgDetails_ADD = await _dbContext.orgadd.AsNoTracking().FirstOrDefaultAsync(i => i.orgnam1 == UserDetails.User.organisation);
                        return UserDetails;
                    }
                    else
                    {
                        _logger.LogError("Incorrect Username or Password.");
                    }
                }
                else
                {
                    _logger.LogError("Incorrect Username or Password.");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
