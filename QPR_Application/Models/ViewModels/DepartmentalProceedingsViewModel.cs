using QPR_Application.Models.Entities;

namespace QPR_Application.Models.ViewModels
{
    public class DepartmentalProceedingsViewModel
    {
        public departmentalproceedingsqrs Departmentalproceedingsqrs { get; set; }
        public List<againstchargedtable> AgainstChargedTables { get; set; }
        public againstchargedtable NewAgainstChargedTable { get; set; }
        public DepartmentalProceedingsViewModel()
        {
            Departmentalproceedingsqrs = new departmentalproceedingsqrs();
            AgainstChargedTables = new List<againstchargedtable>();
        }
    }
}
