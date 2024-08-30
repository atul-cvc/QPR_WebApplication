using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QPR_Application.Repository;
using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;

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
            //_logger = logger;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            _httpContext.HttpContext.Session.Remove("CurrentUser");
            _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            foreach (var cookie in HttpContext.Request.Cookies.Keys)
            {
                // Delete each cookie by its name
                Response.Cookies.Delete(cookie);
            }
            await _dbContext.Database.CloseConnectionAsync();
            var connection = _dbContext.Database.GetDbConnection();
            if(connection.State == System.Data.ConnectionState.Open)
            {
                await connection.CloseAsync();
            }
            return RedirectToAction("Index", "Login");
        }
    }
}
