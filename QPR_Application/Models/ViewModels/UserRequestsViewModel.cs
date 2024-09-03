using QPR_Application.Models.Entities;

namespace QPR_Application.Models.ViewModels
{
    public class UserRequestsViewModel
    {
        public UserRequests UserRequest { get; set; }
        public List<QPRRequestSubjects> RequestSubjects { get; set; }
    }
}
