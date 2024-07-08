using Microsoft.AspNetCore.Mvc;
using QPR_Application.Models.Entities;
using QPR_Application.Models.Utility;
using QPR_Application.Repository;

namespace QPR_Application.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepo _userRepo;
        public UserController(ILogger<HomeController> logger, IUserRepo userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> AllUsers()
        {
            IEnumerable<registration> allUsers = await _userRepo.GetAllUsers();
            return View(allUsers);
        }

        // GET: UserController/Details/5
        public async Task<IActionResult> Details(string Id)
        {
            var User = await _userRepo.GetUserDetails(Id);
            return View(User);
        }
    }
}
