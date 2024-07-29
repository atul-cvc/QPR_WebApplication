using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.DTO.Request;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;
using System;
using System.Data;

namespace QPR_Application.Repository
{
    public class QprRepo : IQprRepo
    {
        private readonly QPRContext _dbContext;
        private readonly IConfiguration _config;
        private string _connString =string.Empty;
        public QprRepo(QPRContext DbContext, IConfiguration config)
        {
            _dbContext = DbContext;
            //_config = config;
            _connString = config.GetConnectionString("SQLConnection");
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
            //var connString = _config.GetConnectionString("SQLConnection");
            string refNum = "" ;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("GetReferenceNumber", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@QtrYear", qprDetails.SelectedYear);
                    cmd.Parameters.AddWithValue("@QtrReport", qprDetails.SelectedQuarter);

                    conn.Open();
                    refNum = Convert.ToString(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex) { }
            return refNum;
        }

        public async Task<string> GenerateReferenceNumber(GetQPR qprDetails, string UserId, string ip)
        {
            string refNum = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("CreateQPRReferenceNumber", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@QtrYear", qprDetails.SelectedYear);
                    cmd.Parameters.AddWithValue("@QtrReport", qprDetails.SelectedQuarter);
                    cmd.Parameters.AddWithValue("@ContactNumber", qprDetails.CVCContactNo);
                    cmd.Parameters.AddWithValue("@Fulltime", qprDetails.CVOFulltime);
                    cmd.Parameters.AddWithValue("@Parttime", qprDetails.CVOParttime);
                    cmd.Parameters.AddWithValue("@ipAddress", ip);

                    conn.Open();
                    refNum = Convert.ToString(cmd.ExecuteScalar());
                    if (String.IsNullOrEmpty(refNum))
                    {

                    }
                }
            }
            catch (Exception ex) { }
            return refNum;
        }

        public async Task<string> GetPreviousReferenceNumber(string UserId)
        {
            string refNum = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetPreviousQprId", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the stored procedure
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        conn.Open();
                        refNum = Convert.ToString(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex) { }
            return refNum;
        }
        public async Task<string> GetPreviousReferenceNumber(string UserId, string qtrYear, string qtrReport)
        {
            string refNum = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetPreviousQprId", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the stored procedure
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        conn.Open();
                        refNum = Convert.ToString(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex) { }
            return refNum;
        }
    }
}
