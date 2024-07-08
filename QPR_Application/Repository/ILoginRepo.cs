using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public interface ILoginRepo
    {
        public Task<Login> Login(Login user);
    }
}
