using Microsoft.EntityFrameworkCore;
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
        //public Task<string> GetPreviousReferenceNumber(string UserId);
        public string GetPreviousReferenceNumber(string UserId, string qtrYear, string qtrReport);
        public Task CreateComplaints(complaintsqrs complaint);
        public Task SaveComplaints(complaintsqrs complaint);
        public Task CreateVigilanceInvestigation(viginvestigationqrs vigInv);
        public Task SaveVigilanceInvestigation(viginvestigationqrs vigInv);

        //public Task<complaintsqrs> GetComplaintsqrs();
    }
}
