using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.ViewModels;
using QPR_Application.Repository;

namespace QPR_Application.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly IRequestsRepo _requestRepo;
        private readonly IQprRepo _qprRepo;
        private readonly IHttpContextAccessor _httpContext;
        public RequestsController(IHttpContextAccessor httpContext,IRequestsRepo requestRepo,IQprRepo qprRepo)
        {
            _requestRepo = requestRepo;
            _qprRepo = qprRepo;
            _httpContext = httpContext;
        }

        [Authorize(Roles = "ROLE_CVO")]
        public async Task<IActionResult> RaiseRequest()
        {
            UserRequestsViewModel userReqVM = new UserRequestsViewModel();
            userReqVM.RequestSubjects = await _requestRepo.GetRequestSubjects();
            List<SelectListItem> quarterItems = new List<SelectListItem>
                    {new SelectListItem { Value = "", Text = "Select" },
                        new SelectListItem { Value = "1", Text = "January to March" },
                        new SelectListItem { Value = "2", Text = "April to June" },
                        new SelectListItem { Value = "3", Text = "July to September" },
                        new SelectListItem { Value = "4", Text = "October to December" }
                    };
            var quarterList = new SelectList(quarterItems, "Value", "Text");
            ViewBag.Quarters = quarterList;

            List<Years> years = await _qprRepo.GetQPRRequestYear();
            int currentYear = DateTime.Now.Year;
            if (years.Count == 0 || years.Last().Year != currentYear.ToString())
            {
                years.Add(new Years { Year = currentYear.ToString() });
            }
            ViewBag.Years = years;
            return View(userReqVM);
        }

        [Authorize(Roles = "ROLE_CVO")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RaiseRequest(UserRequestsViewModel userReqVM)
        {
            try
            {
                await _requestRepo.SaveUserRequest(userReqVM.UserRequest);
                return RedirectToAction("ViewRequestStatus");
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        public IActionResult ViewRequestStatus()
        {
            return View();
        }
    }
}
