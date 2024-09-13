using QPR_Application.Models.DTO.Request;
using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public interface IChangePasswordRepo
    {
        public Task<bool> ChangePassword(ChangePassword cp, registration User, string ip);
    }
}
