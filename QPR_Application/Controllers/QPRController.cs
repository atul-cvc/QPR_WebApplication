using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QPR_Application.Models.DTO.Response;
using System.Text.Json;
using QPR_Application.Repository;
using NuGet.Protocol;
using System.Text.RegularExpressions;
using QPR_Application.Models.Entities;
using QPR_Application.Models.DTO.Request;

namespace QPR_Application.Controllers
{
    public class QPRController : Controller
    {
        private readonly ILogger<QPRController> _logger;
        private readonly IQprRepo _qprRepo;
        private readonly IHttpContextAccessor _httpContext;
        //private qpr QPR;
        private registration userObject;

        public QPRController(ILogger<QPRController> logger, IQprRepo qprRepo, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _qprRepo = qprRepo;
            _httpContext = httpContext;

        }

        public async Task<IActionResult> Index()
        {
            try
            {
                if (_httpContext.HttpContext.Session.GetString("CurrentUser") != null)
                {
                    userObject = JsonSerializer.Deserialize<registration>(_httpContext.HttpContext.Session.GetString("CurrentUser"));
                    ViewBag.User = userObject;
                }
                List<SelectListItem> quarterItems = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "January to March" },
                    new SelectListItem { Value = "2", Text = "April to June" },
                    new SelectListItem { Value = "3", Text = "July to September" },
                    new SelectListItem { Value = "4", Text = "October to December" }
                };
                var quarterList = new SelectList(quarterItems, "Value", "Text");
                ViewBag.Quarters = quarterList;

                List<Years> years = await _qprRepo.GetYears();
                int currentYear = DateTime.Now.Year;
                if (years.Count == 0 || years[0].Year != currentYear.ToString())
                {
                    years.Insert(0, new Years { Year = currentYear.ToString() });
                }
                ViewBag.Years = years;
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(GetQPR qprDetails)
        {
            if (ModelState.IsValid)
            {
                string qprDet = JsonSerializer.Serialize(qprDetails);

                //_httpContext.HttpContext.Session.SetString("QPRDetails", qprDet);
                return RedirectToAction("Index", "Complaints");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
