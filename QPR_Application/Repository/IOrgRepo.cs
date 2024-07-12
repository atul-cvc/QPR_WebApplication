using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public interface IOrgRepo
    {
        public Task<IEnumerable<orgadd>> Getallorg();
        Task SaveOrg(orgadd orgadd);
        public Task<orgadd> GetOrgDetails(string Id);
        public Task<orgadd> EditSave(orgadd orgadd);
        public Task Delete(long id);
    }
}
