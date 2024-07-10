using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public interface IAdminRepo
    {
        public Task<IEnumerable<qpr>> GetAllQprs();
        
    }
}
