using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QPR_Application.Models.Entities;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.DTO.Utility;
using QPR_Application.Repository;
using System.Security.Claims;
using System.Text.Json;
using System.Net;
using QPR_Application.Util;
using System.Security.Cryptography;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using QPR_Application.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using ASPSnippets.Captcha;

namespace QPR_Application.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginRepo _loginRepo;
        private readonly IHttpContextAccessor _httpContext;
        private readonly OTP_Util _otp_Util;

        //public static Captcha staticCaptcha = null;
        private static Captcha Captcha = null;

        public LoginController(ILogger<LoginController> logger, ILoginRepo loginRepo, IHttpContextAccessor httpContext, OTP_Util otp_Util)
        {
            _logger = logger;
            _loginRepo = loginRepo;
            _httpContext = httpContext;
            _otp_Util = otp_Util;
        }
        public IActionResult Index()
        {
            Login login = new Login();
            try
            {
                string ipAdd = Response.HttpContext.Connection.RemoteIpAddress.ToString();
                if (ipAdd == "::1")
                {
                    // Filter and return the first IPv4 address found
                    ipAdd = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
                }
                _httpContext?.HttpContext?.Session.SetString("ipAddress", ipAdd);

                //ClaimsPrincipal claimUser = _httpContext.HttpContext.User;
                //if (claimUser.Identity.IsAuthenticated)
                //{
                //    if (claimUser.IsInRole("ROLE_COORD"))
                //    {
                //        return RedirectToAction("Index", "Admin");
                //    }
                //    if (claimUser.IsInRole("ROLE_CVO"))
                //    {
                //        return RedirectToAction("Index", "QPR");
                //    }
                //    if (claimUser.IsInRole("ROLE_SO"))
                //    {
                //        return RedirectToAction("PendingRequests", "So");
                //    }
                //}

                login.ImageData = GenerateNewCaptcha();


                //staticCaptcha = this.Captcha;
                //_httpContext?.HttpContext?.Session.SetString("captchaExpression", this.Captcha.Expression.toString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login Page view failed");
            }

            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(login);
                }
                else if (!Captcha.IsValid(login.CaptchaAnswer))
                {
                    ViewBag.Error = "Captcha invalid";
                    login.ImageData = GenerateNewCaptcha();
                    return View(login);
                }

                if (ModelState.IsValid)
                {
                    UserDetails uDetails = await _loginRepo.Login(login);

                    if (uDetails.User != null)
                    {

                        string userObj = JsonSerializer.Serialize(uDetails.User);
                        if (!String.IsNullOrEmpty(userObj))
                        {
                            _httpContext?.HttpContext?.Session.SetString("CurrentUser", userObj);
                            _httpContext?.HttpContext?.Session.SetString("UserName", uDetails.User.userid);
                            _httpContext?.HttpContext?.Session.SetString("UserRole", uDetails.User.logintype);
                            _httpContext?.HttpContext?.Session.SetString("UserMobileNumber", uDetails.User.logintype);
                            _httpContext?.HttpContext?.Session.SetString("UserRole", uDetails.User.logintype);

                            if (uDetails.OrgDetails != null)
                            {
                                _httpContext?.HttpContext?.Session.SetString("orgcode", uDetails.OrgDetails.orgcod);
                                _httpContext?.HttpContext?.Session.SetString("OrgName", uDetails.OrgDetails.orgnam1);
                            }

                            // Trigger OTP Verification 
                            //if (SendOTP(uDetails.User.mobilenumber, uDetails.User.email))
                            //{
                            //    return RedirectToAction("VerifyOTP");
                            //}
                            //else
                            //{
                            //    return View(login);
                            //}

                            // Bypass OTP Verification
                            return AuthenticateUser(uDetails.User);
                        }
                        else
                        {
                            _logger.LogError("User Object not found");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User login failed");

            }
            return RedirectToAction("Index");
        }

        public IActionResult VerifyOTP()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Verify otp failed");
                return PartialView("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VerifyOTP(VerifyOTPViewModel verifyVM)
        {
            try
            {
                var userObj = _httpContext?.HttpContext?.Session.GetString("CurrentUser");
                var sessionOtp = _httpContext?.HttpContext?.Session.GetString("SESSION_OTP");
                if (IsOtpValid(sessionOtp, verifyVM.Otp) && !string.IsNullOrEmpty(userObj))
                {
                    //registration user = DeserializeUserDetails(userObj);
                    registration user = DeserializeJson<registration>(userObj);
                    return AuthenticateUser(user);
                }
                else
                {
                    ViewBag.OTPErrorMsg = "Invalid OTP";
                    _logger.LogInformation("Invalid OTP");
                    return View(verifyVM);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Verify otp failed");
                return PartialView("Error");
            }
        }

        private bool IsOtpValid(string? sessionOtp, string? inputOtp)
        {
            return !string.IsNullOrEmpty(sessionOtp) && sessionOtp.Equals(inputOtp);
        }

        private registration DeserializeUserDetails(string userObj)
        {
            return JsonSerializer.Deserialize<registration>(userObj) ?? new registration();
        }

        private T DeserializeJson<T>(string jsonString)
        {
            return JsonSerializer.Deserialize<T>(jsonString) ?? Activator.CreateInstance<T>();
        }

        public bool SendOTP(string mobile, string email)
        {
            try
            {
                string _session_OTP = _otp_Util.SendOTP(mobile, email);
                _httpContext?.HttpContext?.Session.SetString("SESSION_OTP", _session_OTP);
                //_logger.LogInformation("OTP sent successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Send OTP failed at login.");
                return false;
            }
        }

        public async void AddAuth(registration user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.userid),
                new Claim(ClaimTypes.Name, user.userid),
                new Claim(ClaimTypes.Role, user.logintype)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = false
            };

            await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
            _logger.LogInformation("User logged in successfully.");
        }

        public IActionResult RouteUserProfile(string logintype)
        {
            if (logintype == "ROLE_COORD" || logintype == "ROLE_ADMIN")
            {
                return RedirectToAction("Index", "Admin");
            }
            if (logintype == "ROLE_CVO")
            {
                return RedirectToAction("Index", "QPR");
            }
            if (logintype == "ROLE_SO")
            {
                return RedirectToAction("PendingRequests", "SO");
            }
            return RedirectToAction("Index");
        }

        public string GenerateNewCaptcha()
        {
            Captcha = new Captcha(200, 40, 20f, "#FFFFFF", "#61028D", Mode.AlphaNumeric);
            return Captcha.ImageData;
        }

        public IActionResult AuthenticateUser(registration user)
        {
            AddAuth(user);
            return RouteUserProfile(user.logintype);
        }
    }
}
