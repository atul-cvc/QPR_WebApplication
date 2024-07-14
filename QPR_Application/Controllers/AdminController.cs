using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;
using System.Text.Json;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;


namespace QPR_Application.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdminRepo _adminRepo;
        private readonly IHttpContextAccessor _httpContext;

        //private registration _user;
        public AdminController(ILogger<HomeController> logger, IAdminRepo adminRepo, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _adminRepo = adminRepo;
            _httpContext = httpContext;
        }

        // GET: Admin
        public IActionResult Index()
        {
            //if (_httpContext.HttpContext.Session.GetString("UserName") != null)
                ViewBag.UserName = _httpContext.HttpContext.Session.GetString("UserName");
            //var userName = ;
            return View();
        }
        public IActionResult Logout()
        {
            _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //var userName = ;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> QPR()
        {
            var qprList = await _adminRepo.GetAllQprs(); 
            return View(qprList);
        }

        public IActionResult EditQPR()
        {
            return View();
        }
    }
}
