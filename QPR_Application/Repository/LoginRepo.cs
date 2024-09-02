﻿using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Util.VerifyPassword;


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
                //UserDetails.User = await _dbContext.registration.FirstOrDefaultAsync(i => i.userid == user.Username && user.Password == i.password && i.status == "t" && i.islocked == "0");
                UserDetails.User = await _dbContext.registration.FirstOrDefaultAsync(i => i.userid == user.Username && i.status == "t" && i.islocked == "0");
                Boolean passwordVerified = false;
                if (UserDetails.User != null)
                {
                    passwordVerified = new VerifyPassword().VerifyUserPassword(user, UserDetails.User);
                }
                if (UserDetails.User != null && passwordVerified)
                {
                    UserDetails.OrgDetails = await _dbContext.orgadd.FirstOrDefaultAsync(i => i.orgnam1 == UserDetails.User.organisation);
                    return UserDetails;
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
