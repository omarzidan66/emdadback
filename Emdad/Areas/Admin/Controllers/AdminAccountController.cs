using Emdad.Areas.Admin.ViewModels;
using Emdad.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Emdad.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccountController : Controller
    {
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly UserManager<IdentityUser> UserManager;
        private readonly EmdadContext context;
        public AdminAccountController(SignInManager<IdentityUser> _SignInManage, UserManager<IdentityUser> _UserManager, EmdadContext _context)
        {
            SignInManager = _SignInManage;
            UserManager = _UserManager;
            context = _context;
        }


        [HttpGet]
        public IActionResult CreateAdmin()
        {
            var sectors = context.Sectors
                .Select(s => new SelectListItem
                {
                    Value = s.SectorsId.ToString(),
                    Text = s.SectorsName
                }).ToList();

            var model = new CreateAdminViewModel
            {
                AvailableSectors = sectors
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(CreateAdminViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.AvailableSectors = context.Sectors
            //        .Select(s => new SelectListItem
            //        {
            //            Value = s.SectorsId.ToString(),
            //            Text = s.SectorsName
            //        }).ToList();

            //    return View(model);
            //}

            var identityUser = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                EmailConfirmed = true
            };

            var identityResult = await UserManager.CreateAsync(identityUser, model.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                    ModelState.AddModelError("", error.Description);

                model.AvailableSectors = context.Sectors
                    .Select(s => new SelectListItem
                    {
                        Value = s.SectorsId.ToString(),
                        Text = s.SectorsName
                    }).ToList();

                return View(model);
            }

            await UserManager.AddToRoleAsync(identityUser, "Admin");

            // Add to Admins table
            var newAdmin = new Admins
            {
                IdentityUserId = identityUser.Id,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                AssignedSectorId = model.AssignedSectorId,
                IsActive = true,
                IsStopped = false,
                LastActiveAt = DateTime.Now
            };

            context.Admins.Add(newAdmin);
            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Admin created successfully and assigned to sector.";
            return RedirectToAction("AdminManage", "Admin");
        }


        // GET: Login
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Login
        
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
            {
                ViewData["ReturnUrl"] = returnUrl;

                if (!ModelState.IsValid)
                    return View(model);

                var user = await UserManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    var result = await SignInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        var roles = await UserManager.GetRolesAsync(user);

                    if (roles.Contains("SuperAdmin"))
                        return RedirectToAction("AdminManage", "Admin");
                    else if (roles.Contains("Admin"))
                        return RedirectToAction("Index", "Home");

                }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

        [HttpGet]
        public IActionResult SectorLogin(int sectorId)
        {
            var sector = context.Sectors.Find(sectorId);
            if (sector == null) return NotFound();

            var model = new SectorLoginViewModel
            {
                SectorId = sectorId,
                SectorName = sector.SectorsName
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SectorLogin(SectorLoginViewModel model)
        {
            //if (!ModelState.IsValid)
            //    return View(model);

            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View(model);
            }

            // Ensure this user is in Admins table
            var admin = context.Admins.FirstOrDefault(a => a.IdentityUserId == user.Id);
            if (admin == null || admin.AssignedSectorId != model.SectorId)
            {
                return RedirectToAction("AccessDenied", "AdminAccount");
            }

            var result = await SignInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Login failed");
                return View(model);
            }

            // Redirect to sector-specific dashboard
            return RedirectToAction("ManageSector", "Home", new { id = model.SectorId });
        }


        // Logout
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login", "AdminAccount");
        }
    }
}
