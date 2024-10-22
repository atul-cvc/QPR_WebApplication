using Microsoft.AspNetCore.Mvc;
using QPR_Application.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using QPR_Application.Util;
using QPR_Application.Models.DTO.Utility;

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

        public IActionResult KeyService()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult KeyService(string key_str)
        {
            string enc_str = CryptoEngine.EncryptNew(key_str);
            string dec_str = CryptoEngine.DecryptNew(enc_str);
            ViewBag.KeyResult = enc_str;
            return View();
        }

    }

}
