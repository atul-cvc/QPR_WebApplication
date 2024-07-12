using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public class ManageUserRepo : IManageUserRepo
    {
        private readonly QPRContext _dbContext;
        public ManageUserRepo(QPRContext DbContext)
        {
            _dbContext = DbContext;
        }

        public Boolean CreateUser(registration registerUser)
        {
            Boolean status = false;
            try
            {
                _dbContext.registration.Add(registerUser);
                _dbContext.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void EditUser(registration user)
        {
            //throw new NotImplementedException();
            try
            {
                _dbContext.registration.Update(user);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        public async Task<IEnumerable<registration>> GetAllUsers()
        {
            return await _dbContext.registration.ToListAsync();
        }

        public async Task<registration> GetUserDetails(string Id)
        {
            var user = await _dbContext.registration.FirstOrDefaultAsync(u => u.usercode == Convert.ToInt64(Id));
            if (user != null)
                return user;
            return null;
        }

    }
}
