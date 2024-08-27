using QPR_Application.Models.Entities;

namespace QPR_Application.Models.ViewModels
{
    public class PreventiveVigilanceViewModel
    {
        public preventivevigilanceqrs PreventiveVigilanceQRS { get; set; }
        public List<preventivevigi_a_qpr> PrevVigiA {  get; set; }
        public List<preventivevigi_b_qpr> PrevVigiB {  get; set; }
        public preventivevigi_a_qpr NewPreventivVigi_A { get; set; }
        public preventivevigi_b_qpr NewPreventivVigi_B { get; set; }
        public IFormFile? FormFile { get; set; }

        public PreventiveVigilanceViewModel()
        {
            PreventiveVigilanceQRS = new preventivevigilanceqrs();
            PrevVigiA = new List<preventivevigi_a_qpr>();
            PrevVigiB = new List<preventivevigi_b_qpr>();
        }
    }
}
