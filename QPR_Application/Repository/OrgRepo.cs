using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public class OrgRepo : IOrgRepo
    {
        private readonly QPRContext _qPRContext;
        public OrgRepo(QPRContext qPRContext)
        {
            _qPRContext = qPRContext;
        }
        public async Task<IEnumerable<orgadd>> Getallorg()
        {
            return await _qPRContext.orgadd.ToListAsync();
        }
        public async Task SaveOrg(orgadd newOrg)
        {
            _qPRContext.orgadd.Add(newOrg);
            await _qPRContext.SaveChangesAsync();
        }

        //public async Task SaveOrg(orgadd newOrg)
        //{
        //    qPRContext.orgadd.Add(newOrg);
        //    await qPRContext.SaveChangesAsync();
        //}
        public async Task<orgadd> GetOrgDetails(string Id)
        {
            var user = await _qPRContext.orgadd.FirstOrDefaultAsync(u => u.Id == Convert.ToInt64(Id));
            return user;
        }


        public async Task<orgadd> EditSave(orgadd orgadd)
        {
            try
            {
                _qPRContext.orgadd.Update(orgadd);

                // Save changes to the database
                await _qPRContext.SaveChangesAsync();
                return orgadd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Delete(long id)
        {
            // Find the entity by ID
            var entity = await _qPRContext.orgadd.FindAsync(id);

            if (entity != null)
            {
                // Remove the entity
                _qPRContext.orgadd.Remove(entity);

                // Save changes to the database
                await _qPRContext.SaveChangesAsync();
            }
        }
    }
}
