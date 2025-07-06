using Emdad.Models;
using Microsoft.AspNetCore.Mvc;
using Emdad.ViewModels;
using Emdad.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
namespace Emdad.Controllers
{

    [Authorize]
    [Authorize(Roles = "Citizen")]

    public class HomeController : Controller
    {
        public EmdadContext context { get; }
        public IRepository<Sectors> Sectors { get; }
        public IRepository<SectorsServices> SectorsServices { get; }

        public IRepository<Submission> Submission { get; }
        public IRepository<PublicSubmission> PublicSubmission { get; }
        public IRepository<Citizen> Citizen { get; }
        public IRepository<FeedbackAndSuggestion> FeedbackAndSuggestion { get; }
        public IRepository<CitizensSettings> CitizensSettings { get; }
        private readonly SignInManager<IdentityUser> SignInManager;


        public HomeController(EmdadContext _context,
            IRepository<Sectors> _Sectors,
            IRepository<Submission> _Submission,
            IRepository<PublicSubmission> _PublicSubmission,
            IRepository<Citizen> _CitizenRepository,
            IRepository<FeedbackAndSuggestion> _FeedbackAndSuggestion,
            IRepository<SectorsServices> _SectorsServices,
            IRepository<CitizensSettings> _CitizensSettings,
            SignInManager<IdentityUser> _SignInManager
            )
        {
            context = _context;
            Sectors = _Sectors;
            Submission = _Submission;
            PublicSubmission = _PublicSubmission;
            Citizen = _CitizenRepository;
            FeedbackAndSuggestion = _FeedbackAndSuggestion;
            SectorsServices = _SectorsServices;
            CitizensSettings = _CitizensSettings;
            SignInManager = _SignInManager;
        }
        

        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var citizen = context.Citizen.FirstOrDefault(c => c.CreateId == userId);

            var data = new HomeModel
            {
                ListSectors = Sectors.ViewClient(),
                ListSectorsServices = SectorsServices.ViewClient(),
                ListSubmission = Submission
                     .ViewClient()
                     .Where(s => s.CitizenNationalId == citizen.CitizenNationalId)
                     .ToList(),
                PublicSubmission = new PublicSubmission {
                    CitizenNationalId = citizen?.CitizenNationalId,
                    CitizenEmail = citizen?.CitizenEmail,
                },
                FeedbackAndSuggestion = new FeedbackAndSuggestion
                {
                    CitizenNationalId = citizen?.CitizenNationalId,
                    CitizenEmail = citizen?.CitizenEmail,
                },

                ListCitizen = Citizen.ViewClient(),
                CitizensSettings = new CitizensSettings
                {
                    CitizenNationalId = citizen?.CitizenNationalId,
                    
                }
            };

            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HomeModel model)
        {
            try
            {

                if (model?.PublicSubmission != null)
                {
                    // 1. Get current user ID
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var userName = User.FindFirst(ClaimTypes.Name)?.Value;
                    model.PublicSubmission.CreateId = userId;
                    model.PublicSubmission.EditId = userId;


                    // 2. Save the PublicSubmission
                    PublicSubmission.Add(model.PublicSubmission);
                    context.SaveChanges(); // To generate PublicSubmissionId

                    // 4. Create new Submission (leave name for admin later)
                    var newSubmission = new Submission
                    {
                        SectorsId = 0, // Admin can override this later
                        SubmissionName = model.PublicSubmission.PublicSubmissionFullName,
                        SubmissionStatus = "مقبول",
                        CreateId = userId,
                        EditId = userId,
                        CitizenNationalId = model.PublicSubmission.CitizenNationalId,

                        
                    };

                    Submission.Add(newSubmission);
                    context.SaveChanges();

                    TempData["FormSuccess"] = "تم إرسال البلاغ بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
                if (model?.FeedbackAndSuggestion != null)
                {
                    // 1. Get current user ID
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    model.FeedbackAndSuggestion.CreateId = userId;
                    model.FeedbackAndSuggestion.EditId = userId;

                    // 2. Save the PublicSubmission
                    FeedbackAndSuggestion.Add(model.FeedbackAndSuggestion);
                    context.SaveChanges(); // To generate PublicSubmissionId

                    TempData["FormSuccess"] = "تم إرسال البلاغ بنجاح.";
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                TempData["FormError"] = "حدث خطأ غير متوقع أثناء حفظ البلاغ.";
                return RedirectToAction("Index", "Home");
            }
            return View(model);

        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("RegisterAndLogin", "Account");
        }
    }
}
