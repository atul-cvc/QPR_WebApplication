using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public interface IRequestsRepo
    {
        public Task<List<QPRRequestSubjects>> GetRequestSubjects();
        public Task SaveUserRequest(UserRequests userRequest);
        public Task<List<UserRequests>> GetUserRequests(string userId, string IsActive);
        public Task<List<UserRequests>> GetUserRquestsCVO();
        public Task<List<UserRequests>> GetUserRquestsSO(string IsActive);
        public Task<UserRequests> GetUserRquestById(int request_id);
        public Task UpdateRequestSO(UserRequests userRequest);
    }
}
