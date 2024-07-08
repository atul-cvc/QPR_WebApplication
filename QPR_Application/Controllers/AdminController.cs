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
        //private static string _userObj;
        private readonly IHttpContextAccessor _httpContext;

        //private registration _user;
        public AdminController(ILogger<HomeController> logger, IAdminRepo adminRepo, IHttpContextAccessor httpContext)
        {
            // HttpContext.Session.GetString("CurrentUser").ToString();
            //var user = JsonSerializer.Deserialize<registration>(_userObj);
            _logger = logger;
            _adminRepo = adminRepo;
            _httpContext = httpContext;

            //var userObj = _httpContext.HttpContext.Session.GetString("CurrentUser");

        }

        // GET: Admin
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("UserName") != null)
                ViewBag.UserName = _httpContext.HttpContext.Session.GetString("UserName");
            //var userName = ;
            return View();
        }
    }
}
