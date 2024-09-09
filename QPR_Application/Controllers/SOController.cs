using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_SO")]
    [Route("User/[controller]")]
    public class SOController : Controller
    {
        private readonly IRequestsRepo _requestRepo;
        public SOController(IRequestsRepo requestRepo)
        {
            _requestRepo = requestRepo;
        }
        public IActionResult Index()
        {
            return RedirectToAction("PendingRequests");
        }

        [HttpGet("PendingRequests")]
        public async Task<IActionResult> PendingRequests()
        {
            try
            {
                List<UserRequests> pendingUserRequests = await _requestRepo.GetUserRquestsSO("true");
                //List<UserRequests> userRequests = await _requestRepo.GetUserRquestsCVO(); 
                return View(pendingUserRequests);
            }
            catch (Exception ex)
            {
            }
            return View(null);
        }

        [HttpGet("ApprovedRequests")]
        public async Task<IActionResult> ApprovedRequests()
        {
            try
            {
                List<UserRequests> approvedUserRequests = await _requestRepo.GetUserRquestsSO("false");
                return View(approvedUserRequests);
            }
            catch (Exception ex)
            {
            }
            return View(null);
        }

        [HttpGet("ViewRequestDetails")]
        public async Task<IActionResult> ViewRequestDetails(string request_id)
        {
            try
            {
                UserRequests userRequest = await _requestRepo.GetUserRquestById(Convert.ToInt32(request_id)) ?? new UserRequests();
                return View(userRequest);
            }
            catch (Exception ex)
            {
            }
            return View(null);
        }

        [HttpPost("UpdateRequestDetails")]
        public async Task<IActionResult> UpdateRequestDetails(UserRequests updatedRequest)
        {
            return RedirectToAction("PendingRequests");
        }
    }
}
