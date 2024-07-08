using Microsoft.AspNetCore.Mvc;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;

namespace QPR_Application.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginRepo _loginRepo;
        public LoginController(ILogger<HomeController> logger, ILoginRepo loginRepo)
        {
            _logger = logger;
            _loginRepo = loginRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Login login)
        {
            Login User = new Login();
            User = await _loginRepo.Login(login);
            //return RedirectToAction("UserDetails", User);
            if (User != null)
            {
                if (User.Role == "ROLE_ADMIN")
                {
                    return RedirectToAction("Index", "Admin", User);
                }
                if (User.Role == "ROLE_CVO")
                {
                    return RedirectToAction("Index", "Home", User);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
