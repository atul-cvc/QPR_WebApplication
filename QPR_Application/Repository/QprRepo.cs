using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.DTO.Request;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;
using System.Data;

namespace QPR_Application.Repository
{
    public class QprRepo : IQprRepo
    {
        private readonly QPRContext _dbContext;
        private readonly IConfiguration _config;
        public QprRepo(QPRContext DbContext, IConfiguration config)
        {
            _dbContext = DbContext;
            _config = config;
        }

        public async Task<List<Years>> GetYears()
        {
            List<Years> years = new List<Years>();
            try
            {
                years = await _dbContext.Set<Years>().FromSqlRaw("EXEC GetYearsFromQPR").ToListAsync();
                return years;
            }
            catch (Exception ex)
            {
            }
            return years;
        }

        public async Task<string> GetReferenceNumber(GetQPR qprDetails, string UserId)
        {
            var connString = _config.GetConnectionString("SQLConnection");
            string refNum = "" ;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand("GetReferenceNumber", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.Add("@QtrYear", SqlDbType.Int).Value = 2019;
                    cmd.Parameters.Add("@QtrReport", SqlDbType.Int).Value = 1;

                    conn.Open();
                    refNum = Convert.ToString(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex) { }
            return refNum;
        }
    }
}
