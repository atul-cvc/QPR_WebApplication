using QPR_Application.Models.Entities;
using QPR_Application.Models.DTO.Response;

namespace QPR_Application.Models.ViewModels
{
    public class AdviceOfCvcViewModel
    {
        public adviceofcvcqrs AdviceOfCvc { get; set; }
        public List<cvcadvicetable> CvcAdvices { get; set; }
        public List<appellateauthoritytable> AppeleateAuthorities { get; set; }
        public cvcadvicetable NewCvcAdvice { get; set; }
        public appellateauthoritytable NewAppeleateAuthority { get; set; }
        public List<TypesAdviceCVC> StageTypes { get; set; }
        public List<string> NatureTypes { get; set; }
        public AdviceOfCvcViewModel()
        {
            AdviceOfCvc = new adviceofcvcqrs();
            CvcAdvices = new List<cvcadvicetable>();
            AppeleateAuthorities = new List<appellateauthoritytable>();
        }
    }
}
