using Microsoft.AspNetCore.Mvc;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;

namespace QPR_Application.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        public DashboardController(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.VAW_URL = _httpContext?.HttpContext?.Session.GetString("VAW_URL") ?? "";            
            ViewBag.IsQPREnabled = Convert.ToBoolean(_httpContext?.HttpContext?.Session.GetString("IsQPREnabled"));
            return View();
        }
    }
}
