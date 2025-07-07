using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Emdad.Models;
using Emdad.Models.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Emdad.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Emdad.Controllers
{
    [Authorize(Roles = "Citizen")]

    public class CitizensSettingsController : Controller

    {
        private readonly UserManager<IdentityUser> UserManager;
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly EmdadContext context;
        public IRepository<Citizen> Citizen { get; }
        public IRepository<CitizensSettings> CitizensSettings { get; }

        private readonly PasswordService passwordService;


        public CitizensSettingsController(EmdadContext _context,
            IRepository<Citizen> _Citizen,
            PasswordService _passwordService,
            IRepository<CitizensSettings> _CitizensSettings,
            UserManager<IdentityUser> _UserManager,
            SignInManager<IdentityUser> _SignInManager)
        {
            context = _context;
            Citizen = _Citizen;
            passwordService = _passwordService;
            CitizensSettings = _CitizensSettings;
            UserManager = _UserManager;
            SignInManager = _SignInManager;
        }

        // GET: CitizensSettings
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var citizen = context.Citizen.FirstOrDefault(c => c.CreateId == userId);
            var data = new ChangePasswordViewModel
            {
                ListCitizensSettings = CitizensSettings.ViewClient()
                     .Where(s => s.CitizenNationalId == citizen.CitizenNationalId)
                     .ToList(),
                CitizenId = citizen.CitizenNationalId
            };
            return View(data);
        }
        // GET: /CitizensSettings/ChangePassword
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await UserManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{UserManager.GetUserId(User)}'.");
            }

            var model = new ChangePasswordViewModel
            {
                CitizenId = user.Id 
            };
            return View(model);
        }


        // POST: /CitizensSettings/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            

            var user = await UserManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{UserManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await UserManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            await SignInManager.RefreshSignInAsync(user);
            // Add a success message to display to the user
            TempData["StatusMessage"] = "Your password has been changed successfully.";

            return RedirectToAction(); 
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizensSettings = await context.CitizensSettings
                .FirstOrDefaultAsync(m => m.CitizensSettingsId == id);
            if (citizensSettings == null)
            {
                return NotFound();
            }

            return View(citizensSettings);
        }

        // GET: CitizensSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitizensSettingsId,CitizenNationalId,CitizenFullName,CitizenEmail,CitizenPhone,CitizenLocation,CitizenAbout,IsActive,IsDelete,CreateId,CreateDate,EditId,EditDate")] CitizensSettings citizensSettings)
        {
            if (ModelState.IsValid)
            {
                context.Add(citizensSettings);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(citizensSettings);
        }

        // GET: CitizensSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizensSettings = await context.CitizensSettings.FindAsync(id);
            if (citizensSettings == null)
            {
                return NotFound();
            }
            return View(citizensSettings);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitizensSettingsId,CitizenNationalId,CitizenFullName,CitizenEmail,CitizenPhone,CitizenLocation,CitizenAbout,IsActive,IsDelete,CreateId,CreateDate,EditId,EditDate")] CitizensSettings citizensSettings)
        {
            if (id != citizensSettings.CitizensSettingsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(citizensSettings);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitizensSettingsExists(citizensSettings.CitizensSettingsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(citizensSettings);
        }

        // GET: CitizensSettings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizensSettings = await context.CitizensSettings
                .FirstOrDefaultAsync(m => m.CitizensSettingsId == id);
            if (citizensSettings == null)
            {
                return NotFound();
            }

            return View(citizensSettings);
        }

        // POST: CitizensSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var citizensSettings = await context.CitizensSettings.FindAsync(id);
            if (citizensSettings != null)
            {
                context.CitizensSettings.Remove(citizensSettings);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitizensSettingsExists(int id)
        {
            return context.CitizensSettings.Any(e => e.CitizensSettingsId == id);
        }
    }
}
