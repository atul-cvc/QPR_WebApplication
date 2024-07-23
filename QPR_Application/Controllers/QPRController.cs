using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QPR_Application.Models.DTO.Response;
using System.Text.Json;
using QPR_Application.Repository;
using NuGet.Protocol;
using System.Text.RegularExpressions;
using QPR_Application.Models.Entities;
using QPR_Application.Models.DTO.Request;
using Microsoft.AspNetCore.Authorization;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_CVO")]
    public class QPRController : Controller
    {
        private readonly ILogger<QPRController> _logger;
        private readonly IQprRepo _qprRepo;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IComplaintsRepo _complaintsRepo;
        private registration? userObject;

        public QPRController(ILogger<QPRController> logger, IQprRepo qprRepo, IHttpContextAccessor httpContext, IComplaintsRepo complaintsRepo)
        {
            _logger = logger;
            _qprRepo = qprRepo;
            _httpContext = httpContext;
            _complaintsRepo = complaintsRepo;
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
        public async Task<IActionResult> Index(GetQPR qprDetails)
        {
            string UserId = _httpContext.HttpContext.Session.GetString("UserName").ToString();
            if (ModelState.IsValid)
            {
                string qprDet = JsonSerializer.Serialize(qprDetails);
                var refNum = await _qprRepo.GetReferenceNumber(qprDetails, UserId);
                _httpContext.HttpContext.Session.SetString("referenceNumber", refNum);

                return RedirectToAction("Complaints");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Complaints()
        {
            try
            {
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                complaintsqrs complaint = await _complaintsRepo.GetComplaintsData(refNum);
                return View(complaint);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Complaints(complaintsqrs complaint)
        {
            return RedirectToAction("VigilanceInvestigation");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveComplaints(complaintsqrs complaint)
        {
            try
            {
                ViewBag.ComplaintsMessage = "Saved";
                return RedirectToAction("Complaints");
            } catch (Exception ex)
            {
                ViewBag.ComplaintsMessage = "Error occured while saving. Please try after sometime";
            }
            return RedirectToAction("Complaints");
        }
        
        public async Task<IActionResult> VigilanceInvestigation(string message="")
        {
            try
            {
                if (!String.IsNullOrEmpty(message)) {
                    ViewBag.ComplaintsMessage = message;
                }
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                var vigInv = await _complaintsRepo.GetVigilanceInvestigationData(refNum);
                return View(vigInv);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveVigInvestigation(viginvestigationqrs vigInv)
        {
            string message = "";
            try
            {
                message = "Saved";
                
            } catch (Exception ex)
            {
                message = "Error occured while saving. Please try after sometime";
            }
            return RedirectToAction("VigilanceInvestigation", "QPR", new { message = message });
            //return RedirectToAction("VigilanceInvestigation", "QPR", new { message = "2" });
        }
        public async Task<IActionResult> ProsecutionSanctions()
        {
            try
            {
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                var prosSec = await _complaintsRepo.GetProsecutionSanctionsData(refNum);
                return View(prosSec);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }
        public IActionResult SaveProsecutionSanctions(viginvestigationqrs vigInv)
        {
            string message = "";
            try
            {
                message = "Saved";

            }
            catch (Exception ex)
            {
                message = "Error occured while saving. Please try after sometime";
            }
            return RedirectToAction("ProsecutionSanctions", new { message = message });
            //return RedirectToAction("VigilanceInvestigation", "QPR", new { message = "2" });
        }
        public async Task<IActionResult> DepartmentalProceedings()
        {
            try
            {
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                var deptProc = await _complaintsRepo.GetDepartmentalProceedingsData(refNum);
                return View(deptProc);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AdviceOfCVC()
        {
            try
            {
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                var adviceOfCVC = await _complaintsRepo.GetAdviceOfCVCData(refNum);
                return View(adviceOfCVC);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> StatusofPendencyFIandCACases()
        {
            try
            {
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                var data = await _complaintsRepo.GetStatusPendencyData(refNum);
                return View(data);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> PunitiveVigilance()
        {
            try
            {
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                var data = await _complaintsRepo.GetPunitiveVigilanceData(refNum);
                return View(data);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> PreventiveVigilance()
        {
            try
            {
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                var data = await _complaintsRepo.GetPreventiveVigilanceData(refNum);
                return View(data);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> PreventiveVigilanceActivities()
        {
            try
            {
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                var data = await _complaintsRepo.GetPreventiveVigilanceActiviteiesData(refNum);
                return View(data);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        public IActionResult Instructions()
        {
            return View();
        }
    }
}
