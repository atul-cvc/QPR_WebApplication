using Microsoft.AspNetCore.Mvc;
using QPR_Application.Models.Entities;
using QPR_Application.Models.DTO.Request;
using System.Text.Json;
using QPR_Application.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace QPR_Application.Controllers
{
    [Authorize]
    public class ChangePasswordController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IChangePasswordRepo _changePasswordRepo;
        private registration? userObject;

        public ChangePasswordController(IHttpContextAccessor httpContext, IChangePasswordRepo changePasswordRepo)
        {
            _httpContext = httpContext;
            _changePasswordRepo = changePasswordRepo;
        }
        public IActionResult Index()
        {
            return RedirectToAction("ChangePassword");
        }
        public IActionResult ChangePassword(string code = "")
        {
            try
            {
                if (_httpContext.HttpContext.Session.GetString("CurrentUser") != null)
                {
                    userObject = JsonSerializer.Deserialize<registration>(_httpContext.HttpContext.Session.GetString("CurrentUser"));
                    ViewBag.User = userObject;

                    if (!string.IsNullOrEmpty(code))
                    {
                        string message = "";
                        switch (code)
                        {
                            case "iop": message = "Incorrect Old Password"; break;
                            case "npop": message = "New password cannot be same as old password"; break;
                            case "npsop": message = "New Password cannot be same as last two previous passwords"; break;
                            default: message = ""; break;
                        }
                        ViewBag.ComplaintsMessage = message;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePassword cp)
        {
            try
            {
                var ip = _httpContext.HttpContext.Connection.RemoteIpAddress.ToString();
                userObject = JsonSerializer.Deserialize<registration>(_httpContext.HttpContext.Session.GetString("CurrentUser"));
                if (userObject != null)
                {
                    if (cp.OldPassword != userObject.password)
                    {
                        return RedirectToAction("ChangePassword", new { code = "iop" });
                    }

                    if (cp.NewPassword == userObject.password)
                    {
                        return RedirectToAction("ChangePassword", new { code = "npop" });
                    }

                    if (cp.NewPassword == userObject.passwordone || cp.NewPassword == userObject.passwordtwo)
                    {
                        return RedirectToAction("ChangePassword", new { code = "npsop" });
                    }

                    // call change password service
                    var changeSuccessful = await _changePasswordRepo.ChangePassword(cp, userObject.userid, ip);
                }
            }
            catch (Exception ex)
            {
            }
            return View();
            //return RedirectToAction("Index", "Login");
        }
    }
}
