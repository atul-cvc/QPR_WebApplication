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
        public async Task<orgadd> GetOrgDetails(string Id)
        {
            var user = await qPRContext.orgadd.FirstOrDefaultAsync(u => u.Id == Convert.ToInt64(Id));

            return user;
        }


        public async Task<orgadd> EditSave(orgadd orgadd)
        {
            qPRContext.orgadd.Update(orgadd);

            // Save changes to the database
            await qPRContext.SaveChangesAsync();
            return orgadd;
        }
        public async Task Delete(long id)
        {
            // Find the entity by ID
            var entity = await qPRContext.orgadd.FindAsync(id);

            if (entity != null)
            {
                // Remove the entity
                qPRContext.orgadd.Remove(entity);

                // Save changes to the database
                await qPRContext.SaveChangesAsync();
            }
        }
    }
}
