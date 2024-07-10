using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;
using System.Collections.Generic;

namespace QPR_Application.Repository
{
    public class AdminRepo : IAdminRepo
    {
        private readonly QPRContext _dbContext;
        public AdminRepo(QPRContext DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<IEnumerable<qpr>> GetAllQprs()
        {
            IEnumerable<qpr> myQrlist = null;  
            try
            {
                myQrlist = await _dbContext.qpr.Take(100).ToListAsync();
                return myQrlist;
            }
            catch (Exception ex)
            {
                
            }
            return myQrlist;
        }
    }
}
