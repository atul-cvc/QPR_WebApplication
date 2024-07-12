using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;
using System.Text.Json;

namespace QPR_Application.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminRepo _adminRepo;
        private readonly IHttpContextAccessor _httpContext;

        //private registration _user;
        public AdminController(ILogger<AdminController> logger, IAdminRepo adminRepo, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _adminRepo = adminRepo;
            _httpContext = httpContext;
        }

        // GET: Admin
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                myMethod();
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public void myMethod()
        {

        }
    }
}
