using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;
using System.Linq;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_ADMIN,ROLE_COORD")]
    public class ManageOrgController : Controller
    {
        private readonly IOrgRepo orgRepo;
        public ManageOrgController(IOrgRepo _org)
        {
            orgRepo = _org;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<orgadd> orgadd;
            //try
            //{
                orgadd = await orgRepo.Getallorg();
            //}
            //catch (Exception ex)
            //{
            //}
            return View(orgadd.OrderByDescending(n=>n.Id));
        }
        public async Task<IActionResult> Details(string id)
        {
            var details = await orgRepo.GetOrgDetails(id);
            return View(details);
        }
        public IActionResult Save()
        {
            return View();
        }
        public async Task<IActionResult> Edit(string id)
        {
            var details=await orgRepo.GetOrgDetails(id);
            return View(details);
        }

        public IActionResult Edit1(orgadd orgadd)
        {
            var details =  orgRepo.EditSave(orgadd);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(long id)
        {
            await orgRepo.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Save1(orgadd org)
        {
            await orgRepo.SaveOrg(org);
            return RedirectToAction("Index");
        }
    }
}



