using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;
using QPR_Application.Repository;
using QPR_Application.Util;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_CVO")]
    public class RequestsController : Controller
    {
        private readonly IRequestsRepo _requestRepo;
        private readonly IQprCRUDRepo _qprCRUDRepo;
        private readonly IHttpContextAccessor _httpContext;
        private readonly QPRUtility _QPRUtility;
        List<SelectListItem> quarterItems = QPRUtility.quarterItems;
        public RequestsController(IHttpContextAccessor httpContext, IRequestsRepo requestRepo, IQprCRUDRepo qprCRUDRepo, QPRUtility QPRUtility)
        {
            _requestRepo = requestRepo;
            _qprCRUDRepo = qprCRUDRepo;
            _httpContext = httpContext;
            _QPRUtility = QPRUtility;
        }

        //[Authorize(Roles = "ROLE_CVO")]
        public async Task<IActionResult> RaiseRequest()
        {
            UserRequestsViewModel userReqVM = new UserRequestsViewModel();
            userReqVM.RequestSubjects = await _requestRepo.GetRequestSubjects();
            var quarterList = new SelectList(quarterItems, "Value", "Text");
            ViewBag.Quarters = quarterList;

            List<Years> years = await _qprCRUDRepo.GetQPRRequestYear();
            int currentYear = DateTime.Now.Year;
            if (years.Count == 0 || years.Last().Year != currentYear.ToString())
            {
                years.Add(new Years { Year = currentYear.ToString() });
            }
            ViewBag.Years = years;
            return View(userReqVM);
        }

        //[Authorize(Roles = "ROLE_CVO")]
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

        public async Task<IActionResult> ViewRequestStatus()
        {
            try
            {
                List<UserRequests> userRequests = await _requestRepo.GetUserRquestsCVO();
                return View(userRequests);
            }
            catch (Exception ex)
            {
            }
            return View(null);
        }
    }
}
