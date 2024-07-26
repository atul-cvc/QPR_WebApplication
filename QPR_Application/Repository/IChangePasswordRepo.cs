using QPR_Application.Models.DTO.Request;

namespace QPR_Application.Repository
{
    public interface IChangePasswordRepo
    {
        public Task<bool> ChangePassword(ChangePassword cp, string UserId, string ip);
    }
}
