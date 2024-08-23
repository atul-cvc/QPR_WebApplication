using QPR_Application.Models.Entities;

namespace QPR_Application.Models.ViewModels
{
    public class PreventiveVigilanceActivitiesViewModel
    {
        public vigilanceactivitiescvcqrs VigilanceActivities { get; set; }
        public IFormFile? FormFile { get; set; }
    }
}
