using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QPR_Application.Models.Entities;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Repository;
using System.Security.Claims;
using System.Text.Json;
using System.Net;
using QPR_Application.Util;
using QPR_Application.Models.ViewModels;
using ASPSnippets.Captcha;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using QPR_Application.Models.Util;
//using Newtonsoft.Json;

namespace QPR_Application.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginRepo _loginRepo;
        private readonly IHttpContextAccessor _httpContext;
        private readonly OTP_Util _otp_Util;
        private readonly JwtSettings _jwtSettings;

        //public static Captcha staticCaptcha = null;
        private static Captcha Captcha = null;

        public LoginController(ILogger<LoginController> logger, ILoginRepo loginRepo, IHttpContextAccessor httpContext, OTP_Util otp_Util, IOptions<JwtSettings> jwtSettings)
        {
            _logger = logger;
            _loginRepo = loginRepo;
            _httpContext = httpContext;
            _otp_Util = otp_Util;
            _jwtSettings = jwtSettings.Value;
            //var jwt_secretkey = config.GetValue("JwtSettings:secret_key") ?? String.Empty;
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

                login.ImageData = GenerateNewCaptcha();

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

                        string userObj = System.Text.Json.JsonSerializer.Serialize(uDetails.User);
                        if (!String.IsNullOrEmpty(userObj))
                        {
                            _httpContext?.HttpContext?.Session.SetString("CurrentUser", userObj);
                            _httpContext?.HttpContext?.Session.SetString("UserName", uDetails.User.userid);
                            _httpContext?.HttpContext?.Session.SetString("UserRole", uDetails.User.logintype);
                            _httpContext?.HttpContext?.Session.SetString("UserMobileNumber", uDetails.User.logintype);
                            _httpContext?.HttpContext?.Session.SetString("UserRole", uDetails.User.logintype);
                            string _key = CryptoEngine.GenerateRandomKey();
                            var model = new
                            {
                                Userid = uDetails.User.email,
                                LoginType = uDetails.User.logintype,
                                Pass = login.Password,
                                CvoOrgCode = uDetails.OrgDetails.OrgCode,
                                OrgName = uDetails.OrgDetails.OrgName
                            };
                            //var modelStr = JsonSerializer.Serialize(model);
                            var m = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                            var passStr = CryptoEngine.Encrypt(m, _key);
                            //var s = CryptoEngine.Decrypt(passStr, _key);
                            var s = System.Net.WebUtility.UrlEncode(passStr);
                            _key = System.Net.WebUtility.UrlEncode(_key);
                            _httpContext?.HttpContext?.Session.SetString("loginToken", s);
                            _httpContext?.HttpContext?.Session.SetString("loginkey", _key);
                            if (uDetails.OrgDetails != null)
                            {
                                if (!String.IsNullOrEmpty(uDetails.OrgDetails.OrgCode))
                                {
                                    _httpContext?.HttpContext?.Session.SetString("orgcode", uDetails.OrgDetails.OrgCode);

                                }

                                _httpContext?.HttpContext?.Session.SetString("OrgName", uDetails.OrgDetails.OrgName);
                            }
                            else
                            {
                                _httpContext?.HttpContext?.Session.SetString("orgcode", uDetails.OrgDetails_ADD.orgcod);
                                _httpContext?.HttpContext?.Session.SetString("OrgName", uDetails.OrgDetails_ADD.orgnam1);
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
            return System.Text.Json.JsonSerializer.Deserialize<registration>(userObj) ?? new registration();
        }

        private T DeserializeJson<T>(string jsonString)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(jsonString) ?? Activator.CreateInstance<T>();
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

            //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //AuthenticationProperties properties = new AuthenticationProperties()
            //{
            //    AllowRefresh = true,
            //    IsPersistent = false
            //};

            //await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuthentication");

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = false
            };
            //var tokenString = GenerateJwtToken(claims);

            await _httpContext.HttpContext.SignInAsync("MyCookieAuthentication", new ClaimsPrincipal(claimsIdentity), properties);

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

        private string GenerateJwtToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yourSecretKey"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: "yourIssuer",
                audience: "yourAudience",
                claims: claims,
                expires: expiry,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
