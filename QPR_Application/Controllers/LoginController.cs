﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;
using System.Security.Claims;
using System.Text.Json;

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

            if (ModelState.IsValid)
            {
                registration User = new registration();
                User = await _loginRepo.Login(login);
                //User.userid = login.Username;
                //User.password = login.Password;
                //User.logintype = "ROLE_COORD";

                if (User != null)
                {
                    string userObj = JsonSerializer.Serialize(User);
                    if (!String.IsNullOrEmpty(userObj))
                    {
                        _httpContext.HttpContext.Session.SetString("CurrentUser", userObj);
                        _httpContext.HttpContext.Session.SetString("UserName", User.userid);

                        List<Claim> claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, User.userid),
                            new Claim(ClaimTypes.Name, User.userid),
                            new Claim(ClaimTypes.Role, User.logintype)
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

                    if (User.logintype == "ROLE_COORD")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    if (User.logintype == "ROLE_CVO")
                    {
                        return RedirectToAction("Index", "QPR");
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
