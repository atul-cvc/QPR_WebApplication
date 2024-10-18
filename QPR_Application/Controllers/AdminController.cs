using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_ADMIN,ROLE_COORD")]
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
            ViewBag.UserName = "User";
            ViewBag.LoginToken = _httpContext?.HttpContext?.Session.GetString("loginToken");
            ViewBag.LoginKey = _httpContext?.HttpContext?.Session.GetString("loginkey");
            if (_httpContext.HttpContext != null)
            {
                if (_httpContext.HttpContext.Session.GetString("UserName") != null)
                {
                    ViewBag.UserName = _httpContext.HttpContext.Session.GetString("UserName");
                }
            }
            return View();
        }
    }
}
