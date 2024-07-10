using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public class OrgRepo : IOrgRepo
    {
        private readonly QPRContext qPRContext; 
        public OrgRepo(QPRContext qPR)
        {
            qPRContext = qPR;
        }
        public async Task<IEnumerable<orgadd>> Getallorg()
        {
            return await qPRContext.orgadd.ToListAsync();
        }
        public async Task SaveOrg(orgadd newOrg)
        {
            qPRContext.orgadd.Add(newOrg);
            await qPRContext.SaveChangesAsync();
        }

        //public async Task SaveOrg(orgadd newOrg)
        //{
        //    qPRContext.orgadd.Add(newOrg);
        //    await qPRContext.SaveChangesAsync();
        //}
    }
}
