using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;
//using System.Web.Mvc;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_ADMIN,ROLE_COORD")]
    public class ManageOrgController : Controller
    {
        private readonly ILogger<ManageOrgController> _logger;
        private readonly IOrgRepo orgRepo;
        public ManageOrgController(ILogger<ManageOrgController> logger,IOrgRepo _org)
        {
            _logger = logger;
            orgRepo = _org;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<orgadd> orgadd;
            try
            {
                orgadd = await orgRepo.Getallorg();
                return View(orgadd.OrderByDescending(n => n.Id));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Admin");
            }
        }
        public IActionResult CreateOrganisation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrganisation(orgadd org)
        {
            await orgRepo.SaveOrg(org);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(string id, string message = "")
        {
            ViewBag.ComplaintsMessage = message;
            var details = await orgRepo.GetOrgDetails(id);
            return View(details);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var details=await orgRepo.GetOrgDetails(id);
            return View(details);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(orgadd orgadd)
        {
            try
            {
                var details = orgRepo.EditSave(orgadd);
                return RedirectToAction("Details", new { id = orgadd.Id, message = "Saved" });
            }
            catch (Exception ex) {
                _logger.LogError(ex, "");
                return RedirectToAction("Details", new { id = orgadd.Id, message = "Error" });
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            await orgRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}



