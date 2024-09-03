using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public interface IRequestsRepo
    {
        public Task<List<QPRRequestSubjects>> GetRequestSubjects();
        public Task SaveUserRequest(UserRequests _userRequest);
    }
}
