using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public interface IAdminRepo
    {
        public Task<AdminSettings> GetAdminSettingsAsync();
        public Task HashAllUsers();
        public Boolean UpdateAdminSettings(AdminSettings model);
    }
}
