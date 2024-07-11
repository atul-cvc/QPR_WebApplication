using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;
using System.Net.Http;
using System.Text.Json;

namespace QPR_Application.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginRepo _loginRepo;
        private readonly IHttpContextAccessor _httpContext;
        public LoginController(ILogger<HomeController> logger, ILoginRepo loginRepo, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _loginRepo = loginRepo;
            _httpContext = httpContext;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Login login)
        {
            
            registration User = new registration();
            User = await _loginRepo.Login(login);
            if (User != null)
            {
                string userObj = JsonSerializer.Serialize(User);
                //_httpContext.HttpContext.Session.SetString("CurrentUser", userObj);
                _httpContext.HttpContext.Session.SetString("UserName", User.name);

                if (User.logintype == "ROLE_COORD")
                {
                    return RedirectToAction("Index", "Admin");
                }
                if (User.logintype == "ROLE_CVO")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
