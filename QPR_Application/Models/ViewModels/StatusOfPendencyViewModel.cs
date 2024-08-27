using QPR_Application.Models.Entities;

namespace QPR_Application.Models.ViewModels
{
    public class StatusOfPendencyViewModel
    {
        public statusofpendencyqrs StatusOfPendency { get; set; }
        public List<ficasesqpr> FiCasesQPRs { get; set; }
        public List<cacasesqpr> CaCasesQPRs { get; set; }
        public ficasesqpr NewFICase { get; set; }
        public cacasesqpr NewCACase { get; set; }

        public StatusOfPendencyViewModel()
        {
            StatusOfPendency = new statusofpendencyqrs();
            FiCasesQPRs = new List<ficasesqpr>();
            CaCasesQPRs = new List<cacasesqpr>();
        }
    }
}
