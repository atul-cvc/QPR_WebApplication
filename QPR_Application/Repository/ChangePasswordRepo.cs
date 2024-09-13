using Microsoft.Data.SqlClient;
using QPR_Application.Models.DTO.Request;
using System.Data;
using System.Configuration;
using QPR_Application.Models.Entities;
using QPR_Application.Util;
using QPR_Application.Models.DTO.Utility;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace QPR_Application.Repository
{
    public class ChangePasswordRepo : IChangePasswordRepo
    {
        private readonly QPRContext _dbContext;
        private readonly IConfiguration _config;
        private readonly ILogger<ChangePasswordRepo> _logger;
        public ChangePasswordRepo(ILogger<ChangePasswordRepo> logger,QPRContext DbContext, IConfiguration config)
        {
            _logger = logger;
            _dbContext = DbContext;
            _config = config;
        }
        public async Task<bool> ChangePassword(ChangePassword cp, registration User, string ip)
        {
            var connString = _config.GetConnectionString("SQLConnection");

            try
            {

                HashWithSaltResult hs = new PasswordHashingUtil().CreateHashWithSalt(cp.ConfirmPassword, 64, SHA512.Create());

                User.passwordtwo = User.passwordone;
                User.passwordone = User.password;
                User.password = hs.Digest;
                User.PasswordSalt = hs.Salt;
                User.lastmodified = DateTime.Now.ToString();

                _dbContext.registration.Update(User);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
