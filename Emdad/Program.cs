using Emdad.Models;
using Emdad.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddScoped<IRepository<Sectors>, SectorsRepository>();
builder.Services.AddScoped<IRepository<SectorsServices>, SectorsServicesRepository>();
builder.Services.AddScoped<IRepository<FormField>, FormFieldRepository>();
builder.Services.AddScoped<IRepository<Submission>, SubmissionRepository>();
builder.Services.AddScoped<IRepository<SubmissionData>, SubmissionDataRepository>();
builder.Services.AddScoped<IRepository<PublicSubmission>, PublicSubmissionRepository>();
builder.Services.AddScoped<IRepository<Citizen>, CitizenRepository>();
builder.Services.AddScoped<IRepository<FeedbackAndSuggestion>, FeedbackAndSuggestionRepository>();
builder.Services.AddScoped<IRepository<CitizensSettings>, CitizensSettingsRepository>();
builder.Services.AddScoped<PasswordService>();



builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<EmdadContext>();


builder.Services.AddDbContext<EmdadContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon").ToString());
});
builder.Services.Configure<IdentityOptions>(x =>
{
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireDigit = false;
    x.Password.RequireLowercase = false;
    x.Password.RequireUppercase = false;
    x.Password.RequiredLength = 3;

});
//// Add Authentication
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie();

// Configure Cookie options
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/RegisterAndLogin"; // change to your actual login page route
});
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("SuperAdminOnly", policy => policy.RequireRole("SuperAdmin"));
});

var app = builder.Build();

//  Role seeding here
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Citizen", "Admin", "SuperAdmin" };

    foreach (var role in roles)
    {
        var exists = await roleManager.RoleExistsAsync(role);
        if (!exists)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
    // Create default SuperAdmin
    string SuperAdminEmail = "superadmin@example.com";
    string SuperAdminPassword = "SuperSecure123!";

    if (await userManager.FindByEmailAsync(SuperAdminEmail) == null)
    {
        var superAdmin = new IdentityUser { UserName = SuperAdminEmail, Email = SuperAdminEmail, EmailConfirmed = true };
        var result = await userManager.CreateAsync(superAdmin, SuperAdminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
        }
    }
    string AdminEmail = "admin@example.com";
    string AdminPassword = "AdminSecure123!";
    if (await userManager.FindByEmailAsync(AdminEmail) == null)
    {
        var Admin = new IdentityUser { UserName = AdminEmail, Email = AdminEmail, EmailConfirmed = true };
        var result = await userManager.CreateAsync(Admin, AdminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(Admin, "Admin");
        }
    }
}

app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(

    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(

   name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
