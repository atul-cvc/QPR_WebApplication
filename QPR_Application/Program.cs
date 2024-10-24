using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<QPRContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection").ToString()));

// Register repositories or services
builder.Services.AddTransient<ILoginRepo, LoginRepo>();

//builder.Services.AddTransient<IUserRepo, UserRepo>();
//builder.Services.AddTransient<IAdviceOfCVCRepo, AdviceOfCVCRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
