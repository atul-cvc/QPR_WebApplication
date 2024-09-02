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

namespace QPR_Application.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginRepo _loginRepo;
        private readonly IHttpContextAccessor _httpContext;
        public LoginController(ILogger<LoginController> logger, ILoginRepo loginRepo, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _loginRepo = loginRepo;
            _httpContext = httpContext;

        }
        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = _httpContext.HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                if (claimUser.IsInRole("ROLE_COORD"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                if (claimUser.IsInRole("ROLE_CVO"))
                {
                    return RedirectToAction("Index", "QPR");
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Login login)
        {
            try
            {                
                string ipAdd = Response.HttpContext.Connection.RemoteIpAddress.ToString();

                if (ipAdd == "::1")
                {
                    // Filter and return the first IPv4 address found
                    ipAdd = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
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
                            _httpContext?.HttpContext?.Session.SetString("ipAddress", ipAdd);
                            _httpContext?.HttpContext?.Session.SetString("orgcode", uDetails.OrgDetails.orgcod);

                            List<Claim> claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, uDetails.User.userid),
                            new Claim(ClaimTypes.Name, uDetails.User.userid),
                            new Claim(ClaimTypes.Role, uDetails.User.logintype)
                        };
                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            AuthenticationProperties properties = new AuthenticationProperties()
                            {
                                AllowRefresh = true,
                                IsPersistent = false
                            };

                            await _httpContext.HttpContext.SignInAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity),
                                properties);
                        }

                        if (uDetails.User.logintype == "ROLE_COORD")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        if (uDetails.User.logintype == "ROLE_CVO")
                        {
                            return RedirectToAction("Index", "QPR");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");
        }
    }
}
