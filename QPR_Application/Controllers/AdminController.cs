using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;
using System.Text.Json;

namespace QPR_Application.Controllers
{
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
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("UserName") != null)
                ViewBag.UserName = _httpContext.HttpContext.Session.GetString("UserName");
            //var userName = ;
            return View();
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
