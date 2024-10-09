using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;
using QPR_Application.Repository;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_ADMIN,ROLE_COORD")]
    public class ManageUserController : Controller
    {
        private readonly ILogger<ManageUserController> _logger;
        private readonly IManageUserRepo _manageUserRepo;
        public ManageUserController(ILogger<ManageUserController> logger, IManageUserRepo manageUserRepo)
        {
            _logger = logger;
            _manageUserRepo = manageUserRepo;
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
            ViewBag.LoginTypes = new SelectList(loginTypes, "Value", "Text", "Select");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(registration registerUser)
        {
            try
            {
                _manageUserRepo.CreateUser(registerUser);
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
            registration User = await _manageUserRepo.GetEditUserDetails(Id);

            //Populate the dropdown options
            var loginTypes = new List<SelectListItem>{
                new SelectListItem { Value = "ROLE_CVO", Text = "ROLE_CVO" },
                new SelectListItem { Value = "ROLE_COORD", Text = "ROLE_COORD" },
                new SelectListItem { Value = "ROLE_ADMIN", Text = "ROLE_ADMIN" }
            };
            ViewBag.LoginTypes = new SelectList(loginTypes, "Value", "Text", User.logintype);

            return View(User);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(registration User)
        {
            try
            {
                _manageUserRepo.EditUser(User);
                return RedirectToAction("Details", new { id = User.usercode.ToString() });
            }
            catch (Exception ex)
            {
                ViewBag.CreateExceptionMsg = "User edit failed \n" + ex.Message.ToString();
                return View();
            }
        }

    }
}
