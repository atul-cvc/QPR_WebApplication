using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_CVO")]
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
            //return View();
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
        }
    }
}
