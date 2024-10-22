using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QPR_Application.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace QPR_Application.Controllers
{
    [Authorize]
    public class LogoutController : Controller
    {
        private readonly ILogger<LogoutController> _logger;
        private readonly IHttpContextAccessor _httpContext;
        private readonly QPRContext _dbContext;
        public LogoutController(ILogger<LogoutController> logger, IHttpContextAccessor httpContext, QPRContext dbContext)
        {
            _httpContext = httpContext;
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index()
        {
            try
            {
                // Clear session
                _httpContext.HttpContext.Session.Remove("CurrentUser");
                _httpContext.HttpContext.Session.Remove("UserName");
                _httpContext.HttpContext.Session.Remove("ipAddress");
                _httpContext.HttpContext.Session.Remove("UserRole");
                _httpContext.HttpContext.Session.Remove("orgcode");

                // Sign out
                await _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                // Delete cookies
                foreach (var cookie in _httpContext.HttpContext.Request.Cookies.Keys)
                {
                    _httpContext.HttpContext.Response.Cookies.Delete(cookie);
                }

                // Log success
                _logger.LogInformation("User logged out successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during logout.");
                // Optionally, handle the error or redirect to an error page
            }

            return RedirectToAction("Index", "Login");
        }

        public IActionResult LogoutSuccessfull()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
