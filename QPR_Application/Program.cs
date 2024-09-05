using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;
using QPR_Application.Util;

var time = 30; //Minutes
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<QPRContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));

builder.Services.AddHttpContextAccessor();

// Register repositories or services
builder.Services.AddTransient<ILoginRepo, LoginRepo>();
builder.Services.AddTransient<IAdminRepo, AdminRepo>();
builder.Services.AddTransient<IManageUserRepo, ManageUserRepo>();
builder.Services.AddTransient<IManageQprRepo, ManageQprRepo>();
builder.Services.AddScoped<QPRUtilility>();
builder.Services.AddScoped<IQprRepo, QprRepo>();
builder.Services.AddScoped<IRequestsRepo, RequestsRepo>();
builder.Services.AddTransient<IOrgRepo, OrgRepo>();
builder.Services.AddTransient<IComplaintsRepo, ComplaintsRepo>();
builder.Services.AddTransient<IChangePasswordRepo, ChangePasswordRepo>();


// Add services required for sessions

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ExcludeCVO", policy =>
        policy.RequireAssertion(context =>
            !context.User.IsInRole("ROLE_CVO")));
});

//[Authorize(Policy = "ExcludeCVO")]
//public IActionResult RestrictedAction()
//{
//    // Your action logic here
//    return View();
//}

builder.Services.AddSession(options =>
{
    // Configure session options here
    options.IdleTimeout = TimeSpan.FromMinutes(time); // Session timeout period
    options.Cookie.HttpOnly = true; // Cookie settings
    options.Cookie.IsEssential = true; // Make the session cookie essential
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Login/Index";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(time);
        option.Cookie.HttpOnly = true;
        option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        option.Cookie.SameSite = SameSiteMode.Strict;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Logout/Index");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
