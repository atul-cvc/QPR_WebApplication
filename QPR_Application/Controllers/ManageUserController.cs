using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;

namespace QPR_Application.Controllers
{
    public class ManageUserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepo _userRepo;
        public ManageUserController(ILogger<HomeController> logger, IUserRepo userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public async Task<ActionResult> AllUsers()
        {
            IEnumerable<registration> allUsers = await _userRepo.GetAllUsers();
            return View(allUsers);
        }

        public IActionResult CreateUser()
        {
            //Populate the dropdown options
            var loginTypes = new List<SelectListItem>{
                new SelectListItem { Value = "ROLE_CVO", Text = "ROLE_CVO" },
                new SelectListItem { Value = "ROLE_COORD", Text = "ROLE_COORD" },
                new SelectListItem { Value = "ROLE_ADMIN", Text = "ROLE_ADMIN" }
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
                _userRepo.CreateUser(registerUser);
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
            var User = await _userRepo.GetUserDetails(Id);
            return View(User);
        }

        public async Task<IActionResult> Edit(string Id)
        {
            var User = await _userRepo.GetUserDetails(Id);

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
                _userRepo.EditUser(User);
                return RedirectToAction("Details", "User", new { id = User.usercode.ToString() });
            }
            catch (Exception ex)
            {
                ViewBag.CreateExceptionMsg = "User edit failed \n" + ex.Message.ToString();
                return View();
            }
        }

    }
}
