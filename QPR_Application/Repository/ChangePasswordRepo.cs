using Microsoft.Data.SqlClient;
using QPR_Application.Models.DTO.Request;
using System.Data;
using System.Configuration;
using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public class ChangePasswordRepo : IChangePasswordRepo
    {
        private readonly QPRContext _dbContext;
        private readonly IConfiguration _config;
        public ChangePasswordRepo(QPRContext DbContext, IConfiguration config)
        {
            _dbContext = DbContext;
            _config = config;
        }
        public async Task<bool> ChangePassword(ChangePassword cp, string UserId, string ip)
        {
            var connString = _config.GetConnectionString("SQLConnection");

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("ChangePassword", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the stored procedure
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@NewPassword", cp.NewPassword);
                        cmd.Parameters.AddWithValue("@Ip", ip);

                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using a logging framework)
                // _logger.LogError(ex, "An error occurred while changing the password.");

                // Optionally rethrow the exception or handle it as needed
                // throw;

                return false;
            }
        }
    }
}
