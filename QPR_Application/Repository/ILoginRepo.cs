using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public interface ILoginRepo
    {
        public Task<UserDetails> Login(Login user);
    }
}
