using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public interface IManageQprRepo
    {
        public Task<IEnumerable<qpr>> GetAllQprs();
        public qpr GetQPRByRef(string id);
        public void UpdateQpr(qpr qpr);

    }
}
