using Microsoft.AspNetCore.Mvc;
using QPR_Application.Models.DTO.Request;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;
using System.Text.Json;

namespace QPR_Application.Controllers
{
    public class ComplaintsController : Controller
    {

        private readonly ILogger<ComplaintsController> _logger;
        private readonly IHttpContextAccessor _httpContext;

        public ComplaintsController(ILogger<ComplaintsController> logger, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _httpContext = httpContext;
        }
        public IActionResult Index()
        {
            string qprDet = _httpContext.HttpContext.Session.GetString("QPRDetails");
            GetQPR qprDetails = JsonSerializer.Deserialize<GetQPR>(qprDet);
            return View();
        }
    }
}
