using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;

namespace QPR_Application.Repository
{
    public interface IComplaintsRepo
    {
        public Task<complaintsqrs> GetComplaintsData(string refNum);
        public Task<viginvestigationqrs?> GetVigilanceInvestigationData(string refNum);
        public Task<ProsecutionSanctionsViewModel?> GetProsecutionSanctionsData(string refNum);
        public Task<departmentalproceedingsqrs?> GetDepartmentalProceedingsData(string refNum);
        public Task<adviceofcvcqrs?> GetAdviceOfCVCData(string refNum);
        public Task<statusofpendencyqrs?> GetStatusPendencyData(string refNum);
        public Task<punitivevigilanceqrs?> GetPunitiveVigilanceData(string refNum);
        public Task<preventivevigilanceqrs?> GetPreventiveVigilanceData(string refNum);
        public Task<vigilanceactivitiescvcqrs?> GetPreventiveVigilanceActiviteiesData(string refNum);
    }
}
