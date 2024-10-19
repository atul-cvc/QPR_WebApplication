using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;
using System.Collections.Generic;

namespace QPR_Application.Repository
{
    public class AdminRepo : IAdminRepo
    {
        private readonly QPRContext _dbContext;
        private readonly IConfiguration _config;
        private readonly ILogger<AdminRepo> _logger;

        public AdminRepo(QPRContext dbContext, ILogger<AdminRepo> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<AdminSettings> GetAdminSettingsAsync()
        {
            try
            {
                return await _dbContext.AdminSettings.AsNoTracking().FirstOrDefaultAsync(a => a.IsActive == true);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch admin settings");
                return null;
            }
        }
    }
}
