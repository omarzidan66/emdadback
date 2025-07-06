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

namespace Emdad.Controllers
{
    [Authorize(Roles = "Citizen")]

    public class CitizensSettingsController : Controller
    {
        private readonly EmdadContext context;
        public IRepository<Citizen> Citizen { get; }
        private readonly PasswordService passwordService;


        public CitizensSettingsController(EmdadContext _context,
            IRepository<Citizen> _Citizen,
            PasswordService _passwordService)
        {
            context = _context;
            Citizen = _Citizen;
            passwordService = _passwordService;
        }

        // GET: CitizensSettings
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var citizen = await context.Citizen.FirstOrDefaultAsync(c => c.CreateId == userId);

            // Optional: Handle if user/citizen is null
            if (citizen == null)
            {
                return NotFound("Citizen not found.");
            }

            ViewBag.CitizenNationalId = citizen.CitizenNationalId;
            ViewBag.CitizenEmail = citizen.CitizenEmail;
            

            var settings = await context.CitizensSettings.ToListAsync();
            return View(settings);
        }
        //[HttpGet]
        //public IActionResult ChangePassword()
        //{
        //    // Fetch the list of citizens from the database or context
        //    var citizens = context.CitizensSettings.ToList();  // Or use your specific logic to get the citizens

        //    // Create a ViewModel to hold the citizens data if necessary (e.g., to bind to inputs)
        //    var model = citizens.Select(citizen => new ChangePasswordViewModel
        //    {
        //        CitizenId = citizen.CitizenNationalId,
        //        // Populate other necessary properties (if any) like CurrentPassword, NewPassword, ConfirmPassword
        //    }).ToList();

        //    // Pass the model (list of citizens with ChangePasswordViewModel) to the view
        //    return View(model);
        //}
        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(IFormCollection form)
        //{
        //    // Loop through the submitted form data and handle password change for each citizen
        //    foreach (var citizen in context.CitizensSettings)
        //    {
        //        // Get the current, new, and confirm passwords for this citizen
        //        var currentPassword = form["CurrentPassword_" + citizen.CitizenNationalId];
        //        var newPassword = form["NewPassword_" + citizen.CitizenNationalId];
        //        var confirmPassword = form["ConfirmPassword_" + citizen.CitizenNationalId];

        //        // Implement your password change logic here
        //        var result =  passwordService.ChangePassword(citizen.CitizenNationalId, currentPassword, newPassword, confirmPassword);

        //        if (!result)
        //        {
        //            // Handle error, e.g., log or add error to ModelState
        //            ModelState.AddModelError("", $"Error changing password for Citizen ID {citizen.CitizenNationalId}");
        //        }
        //    }

        //    // If the form is valid, redirect to a success page
        //    if (ModelState.IsValid)
        //    {
        //        return RedirectToAction("PasswordChangedSuccess");
        //    }

        //    // Otherwise, return the same view with error messages
        //    return View();
        //}
        // GET: CitizensSettings/Details/5
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

        // POST: CitizensSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // POST: CitizensSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
