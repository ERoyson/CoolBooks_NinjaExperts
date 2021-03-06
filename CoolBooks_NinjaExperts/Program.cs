using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CoolBooks_NinjaExperts.Data;
using CoolBooks_NinjaExperts.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CoolBooks_NinjaExpertsContextConnection");

builder.Services.AddDbContext<CoolBooks_NinjaExpertsContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<UserInfo>(options => options.SignIn.RequireConfirmedAccount = false) // true if u want to send an confirmation email.
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CoolBooks_NinjaExpertsContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication(); //Verifies the user and creates the security context (cookie)
app.UseAuthorization(); //Verifies the users permissions to access different pages

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();