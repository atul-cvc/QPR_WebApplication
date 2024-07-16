using QPR_Application.Models.DTO.Request;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public interface IQprRepo
    {
        public Task<List<Years>> GetYears();

        public Task<string> GetReferenceNumber(GetQPR qprDetails, string UserId);

        //public Task<complaintsqrs> GetComplaintsqrs();
    }
}
