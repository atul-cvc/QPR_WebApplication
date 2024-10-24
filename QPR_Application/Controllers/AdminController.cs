using Microsoft.AspNetCore.Mvc;
using QPR_Application.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using QPR_Application.Util;
using QPR_Application.Models.Entities;
using System.Security.Cryptography;
using System.Text;

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
        
        public IActionResult KeyServiceMD5()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult KeyServiceMD5(string input, string hash)
        {
            //string enc_str = CryptoEngine.EncryptNew(key_str);
            //string dec_str = CryptoEngine.DecryptNew(enc_str);
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // Compute the hash
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // Format as a hexadecimal string
                }
                ViewBag.KeyResult = sb.ToString().Equals(hash);
            }


            //ViewBag.KeyResult = enc_str;
            return View();
        }

        public async Task<IActionResult> GenererateHashedPasswords()
        {
            try
            {
                await _adminRepo.HashAllUsers();
                return Content("All users' password hashed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hashing all users' password failed");
                return Content(ex.Message);
            }
        }

        public async Task<IActionResult> AdminSettings()
        {
            AdminSettings adminSettings = await _adminRepo.GetAdminSettingsAsync();
            return View(adminSettings);
        }

        public async Task<IActionResult> EditAdminSettings()
        {
            AdminSettings adminSettings = await _adminRepo.GetAdminSettingsAsync();
            return View(adminSettings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAdminSettings(AdminSettings adminSettings)
        {
            Boolean updateStatus = false;
            try
            {
                if (string.IsNullOrEmpty(adminSettings.Test_String))
                    adminSettings.Test_String = string.Empty;
                updateStatus = _adminRepo.UpdateAdminSettings(adminSettings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Admin settings update failed.");
            }

            if (updateStatus)
            {
                ViewBag.SuccessMessage = "Update Successful";
            }
            else
            {
                ViewBag.ErrMessage = "Update Failed";
            }
            return View(adminSettings);
        }

    }

}
