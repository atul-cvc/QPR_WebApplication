using QPR_Application.Models.Entities;

namespace QPR_Application.Models.ViewModels
{
    public class ProsecutionSanctionsViewModel
    {
        public prosecutionsanctionsqrs Prosecutionsanctionsqrs { get; set; }
        public List<agewisependency> Agewisependency { get; set; }
        public agewisependency NewAgewisependency { get; set; }

        public ProsecutionSanctionsViewModel()
        {
            Agewisependency = new List<agewisependency>();
        }
    }
}
