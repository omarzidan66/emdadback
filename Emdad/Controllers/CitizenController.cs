using Emdad.Models;
using Emdad.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System;

namespace Emdad.Controllers
{
    
    public class CitizenController : Controller
    {
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly UserManager<IdentityUser> UserManager;
        private readonly EmdadContext context;
        public readonly IRepository<Citizen> Repository;

        public CitizenController(SignInManager<IdentityUser> _SignInManager,
            UserManager<IdentityUser> _UserManager,
            EmdadContext _context,
            IRepository<Citizen> CitizenRepository)
        {
            SignInManager = _SignInManager;
            UserManager = _UserManager;
            context = _context;
            Repository = CitizenRepository;
        }
        // GET: UserController
        public ActionResult Index()
        {
            return View(Repository.View());
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Citizen collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Requred Value for All Inputs...!");
                    return View(collection);
                }

                // Create user in AspNetUsers (Identity)
                var identityUser = new IdentityUser
                {
                    UserName = collection.CitizenNationalId,
                    Email = collection.CitizenEmail
                };

                var result = await UserManager.CreateAsync(identityUser, collection.CitizenPassword);

                if (result.Succeeded)
                {
                    // Get the newly created user ID from Identity
                    var userId = identityUser.Id;

                    // Assign the Identity User ID to your custom User table

                    collection.CreateId = userId; // Use the real creator's ID in production
                    collection.EditId = userId;

                    // Add the user to your custom User table
                    Repository.Add(collection);

                    return RedirectToAction(nameof(Index));
                }

                // If Identity user creation fails, show errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
