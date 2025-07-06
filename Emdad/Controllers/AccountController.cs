using Emdad.Models;
using Emdad.Models.Repositories;
using Emdad.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Emdad.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly UserManager<IdentityUser> UserManager;
        private readonly EmdadContext context;
        private readonly IRepository<Citizen> Repository;
        private readonly IPasswordHasher<IdentityUser> _passwordHasher;
        public AccountController(SignInManager<IdentityUser> _SignInManager,
            UserManager<IdentityUser> _UserManager,
            EmdadContext _context,
            IRepository<Citizen> _Repository,
            IPasswordHasher<IdentityUser> passwordHasher
            )
        {
            SignInManager = _SignInManager;
            UserManager = _UserManager;
            context = _context;
            Repository = _Repository;
            _passwordHasher = passwordHasher;

        }

        [HttpGet]
        public IActionResult RegisterAndLogin()
        {
            var model = new AuthPageViewModel
            {
                Login = new LoginModel(),
                Register = new RegisterModel(),
                Citizen = new Citizen()
            };
            return View(model);
        }

        // GET: AccountController
        public ActionResult Index()
        {
            var data = UserManager.Users;

            return View(data);
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthPageViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            

            var user = await UserManager.FindByNameAsync(model.Login.CitizenNationalId);
            if (user != null)
            {
                var result = await SignInManager.PasswordSignInAsync(user, model.Login.Password, model.Login.RememberMe, false);

                if (result.Succeeded)
                {
                    var roles = await UserManager.GetRolesAsync(user);

                    
                        return RedirectToAction("Index", "Home");

                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AuthPageViewModel collection)
        {
            try
            {
                

                var user = new IdentityUser
                {
                    Email = collection.Register.Email,
                    UserName = collection.Register.CitizenNationalId,
                    PasswordHash = collection.Register.Password,
                };
              
                var result = await UserManager.CreateAsync(user, collection.Register.Password); 


                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, "Citizen");

                    var hashedPassword = _passwordHasher.HashPassword(user, collection.Register.Password);

                    var citizen = new Citizen
                    {
                        CitizenNationalId = collection.Register.CitizenNationalId,
                        CitizenEmail = collection.Register.Email,
                        CitizenPassword = hashedPassword,
                        CitizenConfirmPassword = hashedPassword,
                        CreateId = user.Id, 
                        EditId = user.Id,
                        IsActive = true,
                    };
                    context.Citizen.Add(citizen);
                    await context.SaveChangesAsync();
                    await SignInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("RegisterAndLogin", collection); 
            }
            catch
            {
                return View("RegisterAndLogin", collection); 
            }
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
