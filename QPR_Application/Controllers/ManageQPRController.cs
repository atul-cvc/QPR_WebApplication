
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using QPR_Application.Models.DTO.Request;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;
using QPR_Application.Repository;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_ADMIN,ROLE_COORD")]
    public class ManageQPRController : Controller
    {
        private readonly ILogger<ManageQPRController> _logger;
        private readonly IManageQprRepo _manageQprRepo;
        private readonly IHttpContextAccessor _httpContext;


        public ManageQPRController(ILogger<ManageQPRController> logger, IManageQprRepo manageQprRepo, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _manageQprRepo = manageQprRepo;
            _httpContext = httpContext;
        }

        public async Task<IActionResult> Index()
        {
            var qprList = await _manageQprRepo.GetAllQprs();
            return View(qprList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(SearchById search)
        {
            return RedirectToAction("EditQpr", new { id = search.Id });
        }

        public IActionResult EditQpr(string id)
        {
            try
            {
                EditQPRViewModel vm = new EditQPRViewModel();
                vm.qpr = _manageQprRepo.GetQPRByRef(id);
                var finalSubmitOptions = new List<SelectListItem>{
                new SelectListItem { Value = "t", Text = "Yes" },
                new SelectListItem { Value = "f", Text = "No" }
            };
                var EmploymentTypeOptions = new List<SelectListItem>{
                new SelectListItem { Value = "Full Time", Text = "Full Time" },
                new SelectListItem { Value = "Part Time", Text = "Part Time" }
            };
                //vm.EmploymentType = 
                if (!string.IsNullOrEmpty(vm.qpr.fulltime) && !string.IsNullOrEmpty(vm.qpr.parttime))
                {
                    if (vm.qpr.fulltime == "t") vm.EmploymentType = "Full Time";
                    if (vm.qpr.parttime == "t") vm.EmploymentType = "Part Time";
                }
                else
                {
                    vm.EmploymentType = string.Empty;
                }
                ViewBag.FinalSubmitOptions = new SelectList(finalSubmitOptions, "Value", "Text", vm.qpr.finalsubmit);
                ViewBag.EmploymentTypeOptions = new SelectList(EmploymentTypeOptions, "Value", "Text", vm.EmploymentType);
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Edit QPR failed to load");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditQpr(EditQPRViewModel vm)
        {
            try
            {
                if(vm.qpr.finalsubmit == "f")
                {
                    vm.qpr.finalsubmitdate = string.Empty;
                }
                if (vm.qpr.finalsubmit == "t" && string.IsNullOrEmpty(vm.qpr.finalsubmitdate))
                {
                    vm.qpr.finalsubmitdate = DateTime.Now.ToString();
                }

                switch (vm.EmploymentType)
                {
                    case "Full Time":
                        vm.qpr.fulltime = "t";
                        vm.qpr.parttime = "f";
                        break;
                    case "Part Time":
                        vm.qpr.fulltime = "f";
                        vm.qpr.parttime = "t";
                        break;
                }



                _manageQprRepo.UpdateQpr(vm.qpr);
                return RedirectToAction("Details", new { id = vm.qpr.referencenumber, message = "Saved" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Editing QPR failed.");
                return RedirectToAction("Details", new { id = vm.qpr.referencenumber, message = "Error" });
            }
        }

        public IActionResult Details(string id, string message = "")
        {
            ViewBag.ComplaintsMessage = message;
            var qpr = _manageQprRepo.GetQPRByRef(id);

            if (!string.IsNullOrEmpty(qpr.fulltime) && !string.IsNullOrEmpty(qpr.parttime))
            {
                //ViewBag.EmployementType = (qpr.fulltime == "t") ? "Full Time" : "";
                if (qpr.fulltime == "t")
                {
                    ViewBag.EmployementType = "Full Time";
                }
                if (qpr.parttime == "t")
                {
                    ViewBag.EmployementType = "Part Time";
                }
            }
            else
            {
                ViewBag.EmployementType = "N/A";
            }

            if (!string.IsNullOrEmpty(qpr.finalsubmit))
            {
                qpr.finalsubmit = qpr.finalsubmit == "t" ? "Submitted" : "Not Submitted";
            }
            else
            {
                qpr.finalsubmit = "N/A";
            }

            if (string.IsNullOrEmpty(qpr.finalsubmitdate))
            {
                qpr.finalsubmitdate = "N/A";
            }

            ViewBag.QtrName = qpr.qtrreport switch
            {
                "1" => "January to March",
                "2" => "April to June",
                "3" => "July to September",
                "4" => "October to December",
                _ => ""
            };
            return View(qpr);
        }
    }
}
