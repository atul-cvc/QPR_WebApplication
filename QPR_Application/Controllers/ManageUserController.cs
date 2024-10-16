using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;
using QPR_Application.Repository;
using QPR_Application.Util;
using System.Threading.Tasks;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_ADMIN,ROLE_COORD")]
    public class ManageUserController : Controller
    {
        private readonly ILogger<ManageUserController> _logger;
        private readonly IManageUserRepo _manageUserRepo;
        private readonly QPRUtility _qPRUtility;
        public ManageUserController(ILogger<ManageUserController> logger, IManageUserRepo manageUserRepo, QPRUtility qPRUtility)
        {
            _logger = logger;
            _manageUserRepo = manageUserRepo;
            _qPRUtility = qPRUtility;
        }

        public async Task<ActionResult> AllUsers()
        {
            IEnumerable<AllUsersViewModel> allUsers = await _manageUserRepo.GetAllUsers();
            return View(allUsers);
        }

        public IActionResult CreateUser()
        {
            //Populate the dropdown options
            var loginTypes = new List<SelectListItem>{
                new SelectListItem { Value = "ROLE_CVO", Text = "ROLE_CVO" },
                new SelectListItem { Value = "ROLE_COORD", Text = "ROLE_COORD" },
                new SelectListItem { Value = "ROLE_ADMIN", Text = "ROLE_ADMIN" },
                new SelectListItem { Value = "ROLE_SO", Text = "ROLE_SO" },
            };

            var isLocked = new List<SelectListItem>{
                new SelectListItem { Value = "1", Text = "Yes" },
                new SelectListItem { Value = "0", Text = "No" }
            };

            CreateUserViewModel createUserVM = new CreateUserViewModel();
            createUserVM.organizationList = _qPRUtility.GetOrganisationsList();

            ViewBag.LoginTypes = new SelectList(loginTypes, "Value", "Text", "Select");
            ViewBag.EmpLock = new SelectList(isLocked, "Value", "Text", "Select");

            return View(createUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserViewModel createUserVM)
        {
            try
            {
                bool x = await _manageUserRepo.CreateUser(createUserVM);
                return RedirectToAction("AllUsers");
            }
            catch (Exception ex)
            {
                ViewBag.CreateExceptionMsg = "User not created \n" + ex.Message.ToString();
                return View();
            }
        }

        // GET: UserController/Details/5
        public async Task<IActionResult> Details(string Id)
        {
            AllUsersViewModel User = await _manageUserRepo.GetUserDetails(Id);
            return View(User);
        }

        public async Task<IActionResult> Edit(string Id)
        {
            try
            {
                EditUserViewModel User = await _manageUserRepo.GetEditUserDetails(Id);
                //Populate the dropdown options
                var loginTypes = new List<SelectListItem>{
                    new SelectListItem { Value = "ROLE_CVO", Text = "ROLE_CVO" },
                    new SelectListItem { Value = "ROLE_COORD", Text = "ROLE_COORD" },
                    new SelectListItem { Value = "ROLE_ADMIN", Text = "ROLE_ADMIN" },
                    new SelectListItem { Value = "ROLE_SO", Text = "ROLE_SO" }
                };
                var isLocked = new List<SelectListItem>{
                    new SelectListItem { Value = "1", Text = "Yes" },
                    new SelectListItem { Value = "0", Text = "No" }
                };
                var StatusList = new List<SelectListItem>{
                    new SelectListItem { Value = "t", Text = "Active" },
                    new SelectListItem { Value = "f", Text = "Inactive" }
                };
                User.OrgCode = _qPRUtility.GetOrganisationCode(User.OrgName);
                ViewBag.EmpLock = new SelectList(isLocked, "Value", "Text", User.IsLocked);
                ViewBag.LoginTypes = new SelectList(loginTypes, "Value", "Text", User.LoginType);
                ViewBag.OrganisationsList = new SelectList(_qPRUtility.GetOrganisationsList(), "Value", "Text", User.OrgCode);
                ViewBag.StatusList = new SelectList(StatusList, "Value", "Text", User.Status);
                
                return View(User);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Edit page failed to load.");
                return RedirectToAction("AllUsers");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel User)
        {
            try
            {
                await _manageUserRepo.EditUser(User);
                return RedirectToAction("Details", new { id = User.UserCode.ToString() });
            }
            catch (Exception ex)
            {
                ViewBag.CreateExceptionMsg = "User edit failed \n" + ex.Message.ToString();
                return View();
            }
        }

    }
}
