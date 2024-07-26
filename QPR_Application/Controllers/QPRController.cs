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
using System;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_CVO")]
    public class QPRController : Controller
    {
        private readonly ILogger<QPRController> _logger;
        private readonly IQprRepo _qprRepo;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IComplaintsRepo _complaintsRepo;
        private readonly IChangePasswordRepo _changePasswordRepo;
        private registration? userObject;

        public QPRController(ILogger<QPRController> logger, IQprRepo qprRepo, IHttpContextAccessor httpContext, IComplaintsRepo complaintsRepo, IChangePasswordRepo changePasswordRepo)
        {
            _logger = logger;
            _qprRepo = qprRepo;
            _httpContext = httpContext;
            _complaintsRepo = complaintsRepo;
            _changePasswordRepo = changePasswordRepo;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                if (_httpContext.HttpContext?.Session.GetString("referenceNumber") != null)
                {
                    _httpContext.HttpContext.Session.Remove("referenceNumber");
                }

                if (_httpContext.HttpContext?.Session.GetString("CurrentUser") != null)
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
        public async Task<IActionResult> Complaints(string message = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.ComplaintsMessage = message;
                }
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                if (!String.IsNullOrEmpty(refNum))
                {
                    complaintsqrs complaint = await _complaintsRepo.GetComplaintsData(refNum);
                    return View(complaint);
                }
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
            string message = "";
            try
            {
                message = "Saved";
            }
            catch (Exception ex)
            {
                message = "Error occured while saving. Please try after sometime";
            }
            return RedirectToAction("Complaints", "QPR", new { message });
        }
        public async Task<IActionResult> VigilanceInvestigation(string message = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.ComplaintsMessage = message;
                }
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                if (!String.IsNullOrEmpty(refNum))
                {

                    var vigInv = await _complaintsRepo.GetVigilanceInvestigationData(refNum);
                    return View(vigInv);
                }
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

            }
            catch (Exception ex)
            {
                message = "Error occured while saving. Please try after sometime";
            }
            return RedirectToAction("VigilanceInvestigation", "QPR", new { message });
            //return RedirectToAction("VigilanceInvestigation", "QPR", new { message = "2" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VigilanceInvestigation(vigilanceactivitiescvcqrs vigInv)
        {
            try
            {
                return RedirectToAction("ProsecutionSanctions");
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public async Task<IActionResult> ProsecutionSanctions(string message = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.ComplaintsMessage = message;
                }
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                if (!String.IsNullOrEmpty(refNum))
                {
                    var prosSec = await _complaintsRepo.GetProsecutionSanctionsData(refNum);
                    return View(prosSec);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveProsecutionSanctions(prosecutionsanctionsqrs prosec)
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
            return RedirectToAction("ProsecutionSanctions", new { message });
            //return RedirectToAction("VigilanceInvestigation", "QPR", new { message = "2" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProsecutionSanctions(prosecutionsanctionsqrs prosec)
        {
            try
            {
                return RedirectToAction("DepartmentalProceedings");
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public async Task<IActionResult> DepartmentalProceedings(string message = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.ComplaintsMessage = message;
                }
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                if (!String.IsNullOrEmpty(refNum))
                {
                    var deptProc = await _complaintsRepo.GetDepartmentalProceedingsData(refNum);
                    return View(deptProc);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveDepartmentalProceedings()
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
            return RedirectToAction("DepartmentalProceedings", new { message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DepartmentalProceedings(departmentalproceedingsqrs deptProc)
        {
            try
            {
                return RedirectToAction("AdviceOfCVC");
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        public async Task<IActionResult> AdviceOfCVC(string message = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.ComplaintsMessage = message;
                }
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                if (!String.IsNullOrEmpty(refNum))
                {
                    var adviceOfCVC = await _complaintsRepo.GetAdviceOfCVCData(refNum);
                    return View(adviceOfCVC);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveAdviceOfCVC()
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
            return RedirectToAction("AdviceOfCVC", new { message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdviceOfCVC(adviceofcvcqrs advicecvc)
        {
            try
            {
                return RedirectToAction("StatusofPendencyFIandCACases");
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public async Task<IActionResult> StatusofPendencyFIandCACases(string message = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.ComplaintsMessage = message;
                }
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                if (!String.IsNullOrEmpty(refNum))
                {
                    var data = await _complaintsRepo.GetStatusPendencyData(refNum);
                    return View(data);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveStatusofPendencyFIandCACases()
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
            return RedirectToAction("StatusofPendencyFIandCACases", new { message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StatusofPendencyFIandCACases(statusofpendencyqrs statusPend)
        {
            try
            {
                return RedirectToAction("PunitiveVigilance");
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        public async Task<IActionResult> PunitiveVigilance(string message = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.ComplaintsMessage = message;
                }
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                if (!String.IsNullOrEmpty(refNum))
                {
                    var data = await _complaintsRepo.GetPunitiveVigilanceData(refNum);
                    return View(data);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavePunitiveVigilance(punitivevigilanceqrs pvig)
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
            return RedirectToAction("PunitiveVigilance", new { message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PunitiveVigilance(punitivevigilanceqrs pvig)
        {
            try
            {
                return RedirectToAction("PreventiveVigilance");
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public async Task<IActionResult> PreventiveVigilance(string message = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.ComplaintsMessage = message;
                }
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                if (!String.IsNullOrEmpty(refNum))
                {
                    var data = await _complaintsRepo.GetPreventiveVigilanceData(refNum);
                    return View(data);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavePreventiveVigilance(preventivevigilanceqrs prev_vig)
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
            return RedirectToAction("PreventiveVigilance", new { message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PreventiveVigilance(preventivevigilanceqrs prev_vig)
        {
            try
            {
                return RedirectToAction("PreventiveVigilanceActivities");
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public async Task<IActionResult> PreventiveVigilanceActivities(string message = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.ComplaintsMessage = message;
                }
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                if (!String.IsNullOrEmpty(refNum))
                {
                    var data = await _complaintsRepo.GetPreventiveVigilanceActiviteiesData(refNum);
                    return View(data);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavePreventiveVigilanceActivities(vigilanceactivitiescvcqrs activities)
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
            return RedirectToAction("PreventiveVigilanceActivities", new { message = message});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PreventiveVigilanceActivities(vigilanceactivitiescvcqrs activities)
        {
            try
            {
                return RedirectToAction("Download QPR");
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        public IActionResult Instructions()
        {
            return View();
        }

        public IActionResult ChangePassword(string code = "")
        {
            try
            {
                if (_httpContext.HttpContext.Session.GetString("CurrentUser") != null)
                {
                    userObject = JsonSerializer.Deserialize<registration>(_httpContext.HttpContext.Session.GetString("CurrentUser"));
                    ViewBag.User = userObject;

                    if (!string.IsNullOrEmpty(code))
                    {
                        string message = "";
                        switch (code)
                        {
                            case "iop": message = "Incorrect Old Password"; break;
                            case "npop": message = "New password cannot be same as old password"; break;
                            case "npsop": message = "New Password cannot be same as last two previous passwords"; break;
                            default: message = ""; break;
                        }
                        ViewBag.ComplaintsMessage = message;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePassword cp)
        {
            try
            {
                var ip = _httpContext.HttpContext.Connection.RemoteIpAddress.ToString();
                userObject = JsonSerializer.Deserialize<registration>(_httpContext.HttpContext.Session.GetString("CurrentUser"));
                if (userObject != null)
                {
                    if (cp.OldPassword != userObject.password)
                    {
                        return RedirectToAction("ChangePassword", new { code = "iop" });
                    }

                    if (cp.NewPassword == userObject.password)
                    {
                        return RedirectToAction("ChangePassword", new { code = "npop" });
                    }

                    if (cp.NewPassword == userObject.passwordone || cp.NewPassword == userObject.passwordtwo)
                    {
                        return RedirectToAction("ChangePassword", new { code = "npsop" });
                    }

                    // call change password service
                    var changeSuccessful = await _changePasswordRepo.ChangePassword(cp, userObject.userid, ip);
                }
            }
            catch (Exception ex)
            {
            }
            return View();
            //return RedirectToAction("Index", "Login");
        }
    }
}
