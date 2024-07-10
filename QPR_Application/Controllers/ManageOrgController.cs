using Microsoft.AspNetCore.Mvc;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;

namespace QPR_Application.Controllers
{
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
            return View(orgadd);
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Save()
        {
            return View();
        }
        public IActionResult Edit(string id)
        {

            return View();
        }
        public async Task<IActionResult> Save1(orgadd org)
        {
            await orgRepo.SaveOrg(org);
            return View();
        }
    }
}



