using Microsoft.AspNetCore.Mvc;

namespace QPR_Application.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        public DashboardController(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public IActionResult Index()
        {
            ViewBag.VAW_URL = _httpContext?.HttpContext?.Session.GetString("VAW_URL") ?? "";
            return View();
        }
    }
}
