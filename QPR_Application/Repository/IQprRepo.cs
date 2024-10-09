using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;

namespace QPR_Application.Repository
{
    public interface IQPRRepo
    {
        public Task<complaintsqrs> GetComplaintsData(string refNum);
        public Task<viginvestigationqrs?> GetVigilanceInvestigationData(string refNum);
        public Task<prosecutionsanctionsqrs?> GetProsecutionSanctionsData(string refNum);
        public Task<ProsecutionSanctionsViewModel?> GetProsecutionSanctionsViewData(string refNum);
        public Task<DepartmentalProceedingsViewModel?> GetDepartmentalProceedingsViewModel(string refNum);
        public Task<departmentalproceedingsqrs?> GetDepartmentalProceedingsData(string refNum);
        public Task<AdviceOfCvcViewModel?> GetAdviceOfCVCViewModel(string refNum);
        public Task<adviceofcvcqrs?> GetAdviceOfCVCData(string refNum);
        public Task<StatusOfPendencyViewModel?> GetStatusPendencyViewModel(string refNum);
        public Task<statusofpendencyqrs?> GetStatusPendencyData(string refNum);
        public Task<punitivevigilanceqrs?> GetPunitiveVigilanceData(string refNum);
        public Task<PreventiveVigilanceViewModel?> GetPreventiveVigilanceViewModel(string refNum);
        public Task<preventivevigilanceqrs?> GetPreventiveVigilanceData(string refNum);
        //public Task<PreventiveVigilanceActivitiesViewModel?> GetPreventiveVigilanceActivitiesViewModel(string refNum);
        public Task<vigilanceactivitiescvcqrs?> GetPreventiveVigilanceActivitiesData(string refNum);
        public Task<CVO_TrainingViewModel> GetCVOTrainingViewModel(string refNum);
        public Task<List<Training_CVO>> GetCVOTrainings(string refNum);
        public Task<List<MasterTraining>> GetTrainingsNameList();
        public Task<List<string>> GetAllQPRIds(string qprYear);
        public List<ficasesqpr> GetFurtherClarification(string OrgCode);
        public List<cacasesqpr> GetCommentsAwaited(string OrgCode);
        public Task<List<ficasesqpr>> GetFICases(string refNum);
        public Task<List<cacasesqpr>> GetCACases(string refNum);
    }
}
