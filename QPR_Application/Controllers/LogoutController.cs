using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QPR_Application.Repository;

namespace QPR_Application.Controllers
{
    public class LogoutController : Controller
    {
        private readonly ILogger<LogoutController> _logger;
        private readonly IHttpContextAccessor _httpContext;
        public LogoutController(ILogger<LogoutController> logger, IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public IActionResult Index()
        {
            _httpContext.HttpContext.Session.Remove("CurrentUser");
            _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            foreach (var cookie in HttpContext.Request.Cookies.Keys)
            {
                // Delete each cookie by its name
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Index", "Login");
        }
    }
}
