using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.DTO.Request;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public class QprRepo : IQprRepo
    {
        private readonly QPRContext _dbContext;
        public QprRepo(QPRContext DbContext)
        {
            _dbContext = DbContext;
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

        public async Task<string> GetReferenceNumber(GetQPR qprDetails)
        {
            try
            {
                var result = _dbContext.Database.SqlQuery<string>("EXEC GetReferenceNumber @qpr_qtr @qpr_year", qprDetails.SelectedQuarter, qprDetails.SelectedYear)).FirstOrDefault();
                string referenceNumber = result ?? ""; // Handle null case if necessary
                Console.WriteLine("Reference Number: " + referenceNumber);
                //string refNum = await _dbContext.Database.ExecuteSqlCommand("EXEC GetReferenceNumber").ToListAsync();
            }
            catch (Exception ex) { }
            throw new NotImplementedException();
        }


        //public async Task<complaintsqrs> GetComplaintsqrs(GetQPR qprDetails)
        //{
        //    return await _dbContext.complaint
        //}

    }
}
