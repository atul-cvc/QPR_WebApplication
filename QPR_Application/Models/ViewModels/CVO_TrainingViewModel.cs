using QPR_Application.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QPR_Application.Models.ViewModels
{
    public class CVO_TrainingViewModel
    {
        public List<SelectListItem> MasterTrainingList { get; set; }
        public List<Training_CVO> Training_CVO_List { get; set; }
        public Training_CVO New_Training_CVO { get; set; }
        public int Total_Training_Programs_Conducted { get; set; }
        public int Total_Employees_Trained { get; set; }
        public CVO_TrainingViewModel()
        {
            MasterTrainingList = new List<SelectListItem>();
            Training_CVO_List = new List<Training_CVO>();
            Total_Employees_Trained = 0;
            Total_Training_Programs_Conducted = 0;
        }

    }
}
