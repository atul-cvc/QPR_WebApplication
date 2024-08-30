using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;
using QPR_Application.Models.DTO.Response;


namespace QPR_Application.Repository
{
    public class LoginRepo : ILoginRepo
    {
        private readonly QPRContext _dbContext;
        public LoginRepo(QPRContext DbContext)
        {
            _dbContext = DbContext;
        }
        public async Task<UserDetails> Login(Login user)
        {
            try
            {
                UserDetails UserDetails = new UserDetails();
                UserDetails.User = await _dbContext.registration.FirstOrDefaultAsync(i => i.userid == user.Username && user.Password == i.password && i.status == "t" && i.islocked == "0");
                if (UserDetails.User != null)
                {
                    UserDetails.OrgDetails = await _dbContext.orgadd.FirstOrDefaultAsync(i => i.orgnam1 == UserDetails.User.organisation);
                }
                //await _dbContext.Database.CloseConnectionAsync();
                //var connection = _dbContext.Database.GetDbConnection();
                //if (connection.State == System.Data.ConnectionState.Open)
                //{
                //}
                //    await connection.CloseAsync();
                return UserDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
