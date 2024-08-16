using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.DTO.Request;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;

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
        public Task CreateProsecutionSanctionsViewModel(ProsecutionSanctionsViewModel prosecViewModel);
        public Task SaveProsecutionSanctionsViewModel(ProsecutionSanctionsViewModel prosecViewModel);
        public Task CreateDepartmentalProceedings(DepartmentalProceedingsViewModel deptViewModel);
        public Task SaveDepartmentalProceedings(DepartmentalProceedingsViewModel deptViewModel, departmentalproceedingsqrs prevData);
        public Task CreateAdviceCVC(AdviceOfCvcViewModel adviceVM);
        public Task SaveAdviceCVC(AdviceOfCvcViewModel adviceVM);
        //, adviceofcvcqrs prevData
        public Task CreateStatusPendency(StatusOfPendencyViewModel statusVM);
        public Task SaveStatusPendency(StatusOfPendencyViewModel statusVM);
        public Task CreatePunitiveVigilance(punitivevigilanceqrs pVig);
        public Task SavePunitiveVigilance(punitivevigilanceqrs pVig);




        public Task DeleteAgeWisePendency(int pend_id);
        public Task DeleteAgainstChargedOfficers(int pend_id);
        public Task DeleteCvcAdvice(int pend_id);
        public Task DeleteAppeleateAuthority(int pend_id);
        public Task DeleteFiCaseRow(int id);
        public Task DeleteCaCaseRow(int id);

        //public Task<complaintsqrs> GetComplaintsqrs();
    }
}
