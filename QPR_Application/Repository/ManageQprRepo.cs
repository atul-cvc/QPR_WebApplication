using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;
using System.Reflection;

namespace QPR_Application.Repository
{
    public class ManageQprRepo : IManageQprRepo
    {
        private readonly QPRContext _dbContext;
        public ManageQprRepo(QPRContext DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<IEnumerable<qpr>> GetAllQprs()
        {
            IEnumerable<qpr> myQprlist = null;
            try
            {
                //myQrlist = await _dbContext.qpr.Take(1000).ToListAsync();
                myQprlist = await _dbContext.qpr.AsNoTracking().ToListAsync();
                return myQprlist;
            }
            catch (Exception ex)
            {
            }
            return myQprlist;
        }

        public qpr GetQPRByRef(string id)
        {
            qpr? qprReport = null;
            qprReport = _dbContext.qpr.Find(Convert.ToInt64(id));
            return qprReport;
        }

        public void UpdateQpr(qpr qpr)
        {
            try
            {
                _dbContext.qpr.Update(qpr);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
