using QPR_Application.Models.Entities;

namespace QPR_Application.Models.ViewModels
{
    public class ProsecutionSanctionsViewModel
    {
        public prosecutionsanctionsqrs Prosecutionsanctionsqrs { get; set; }
        public List<agewisependency> Agewisependency { get; set; }

        //public ProsecutionSanctionsViewModel(prosecutionsanctionsqrs prosSec)
        //{
        //    Prosecutionsanctionsqrs = prosSec;
        //}
        //public ProsecutionSanctionsViewModel(prosecutionsanctionsqrs prosSec, List<agewisependency> ageWise)
        //{
        //    Prosecutionsanctionsqrs = prosSec;
        //    Agewisependency = ageWise;
        //}
    }
}
