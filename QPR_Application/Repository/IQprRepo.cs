using QPR_Application.Models.DTO.Request;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public interface IQprRepo
    {
        public Task<List<Years>> GetYears();

        public Task<string> GetReferenceNumber(GetQPR qprDetails, string UserId);
        public Task<string> GenerateReferenceNumber(GetQPR qprDetails, string UserId, string ip);
        public Task<string> GetPreviousReferenceNumber(string UserId);
        public Task<string> GetPreviousReferenceNumber(string UserId, string qtrYear, string qtrReport);

        //public Task<complaintsqrs> GetComplaintsqrs();
    }
}
