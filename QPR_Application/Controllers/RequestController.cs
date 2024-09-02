using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QPR_Application.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "ROLE_CVO")]
        public IActionResult RaiseRequest()
        {
            return View();
        }
    }
}
