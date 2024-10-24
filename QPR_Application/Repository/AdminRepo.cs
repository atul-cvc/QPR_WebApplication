using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.DTO.Utility;
using QPR_Application.Models.Entities;
using QPR_Application.Util;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace QPR_Application.Repository
{
    public class AdminRepo : IAdminRepo
    {
        private readonly QPRContext _dbContext;
        private readonly IConfiguration _config;
        private readonly ILogger<AdminRepo> _logger;
        private readonly IManageUserRepo _manageUserRepo;

        public AdminRepo(QPRContext dbContext, ILogger<AdminRepo> logger, IManageUserRepo manageUserRepo)
        {
            _dbContext = dbContext;
            _logger = logger;
            _manageUserRepo = manageUserRepo;
        }

        public async Task<AdminSettings> GetAdminSettingsAsync()
        {
            try
            {
                return await _dbContext.AdminSettings.AsNoTracking().FirstOrDefaultAsync(a => a.IsActive == true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch admin settings table");
                AdminSettings _as = new AdminSettings();
                _as.Test_String = "GetAdminSettingsAsync() repo method failed.";
                return _as;
            }
        }

        public async Task HashAllUsers()
        {
            try
            {
                IEnumerable<registration> allUsers = await _manageUserRepo.GetAllUsersForPasswordHashing();
                foreach (var _User in allUsers)
                {
                    if (string.IsNullOrEmpty(_User.PasswordSalt))
                    {
                        //registration _user = _User;
                        HashWithSaltResult hs = new PasswordHashingUtil().CreateHashWithSalt(_User.password, 64, SHA512.Create());
                        _User.password = hs.Digest;
                        _User.PasswordSalt = hs.Salt;

                        _dbContext.registration.Update(_User);
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to hash all users' passwords");
                throw ex;
            }
        }

        public Boolean UpdateAdminSettings(AdminSettings model)
        {
            try
            {
                _dbContext.AdminSettings.Update(model);
                _dbContext.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                _logger.LogError(ex,"");
                return false;
            }
        }
        
    }
}
