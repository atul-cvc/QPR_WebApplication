using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QPR_Application.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public AdminController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }        
    }
}
