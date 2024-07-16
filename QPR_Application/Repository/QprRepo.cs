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
            SqlConnection conn = new SqlConnection(connString);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string refNum = "" ;
            try
            {
                string query = "select referencenumber from qpr where userid = \'"+ UserId +"\' and qtryear = 2019 and qtrreport = 1 ";
                SqlCommand cmd = new SqlCommand(query, conn);
                var refNumber = cmd.ExecuteScalar();
                if(refNumber != null)
                {
                    refNum = Convert.ToString(refNumber);
                }
            }
            catch (Exception ex) { }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return refNum;
        }
    }
}
