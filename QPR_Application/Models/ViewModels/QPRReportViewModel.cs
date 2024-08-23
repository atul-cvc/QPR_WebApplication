using QPR_Application.Models.Entities;

namespace QPR_Application.Models.ViewModels
{
    public class QPRReportViewModel
    {
        public complaintsqrs complaints {  get; set; }
        public viginvestigationqrs vigInvestigation {  get; set; }
        public ProsecutionSanctionsViewModel prosecVM { get; set; }
        public DepartmentalProceedingsViewModel deptVM { get; set; }
        public AdviceOfCvcViewModel adviceViewModel { get; set; }
        public StatusOfPendencyViewModel statusVM { get; set; }
        public punitivevigilanceqrs punitiveVig {  get; set; }
        public PreventiveVigilanceViewModel preventiveViewModel { get; set; }
        public vigilanceactivitiescvcqrs preventiveActivitiesVM { get; set; }

    }
}
