using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace QPR_Application.Controllers
{   
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

        [Authorize]
        public async Task<IActionResult> Index()
        {
            //string _user = _httpContext.HttpContext.Session.GetString("UserName");
            //"UserName"
            //"ipAddress"
            //"UserRole"
            //orgcode
            _httpContext.HttpContext.Session.Remove("CurrentUser");
            _httpContext.HttpContext.Session.Remove("UserName");
            _httpContext.HttpContext.Session.Remove("ipAddress");
            _httpContext.HttpContext.Session.Remove("UserRole");
            _httpContext.HttpContext.Session.Remove("orgcode");
            _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            foreach (var cookie in HttpContext.Request.Cookies.Keys)
            {
                // Delete each cookie by its name
                Response.Cookies.Delete(cookie);
            }
            await _dbContext.Database.CloseConnectionAsync();
            var connection = _dbContext.Database.GetDbConnection();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                await connection.CloseAsync();
            }
            _logger.LogInformation("User logged out successfully.");
            return RedirectToAction("LogoutSuccessfull");
        }

        public IActionResult LogoutSuccessfull()
        {
            return View();
        }
    }
}
