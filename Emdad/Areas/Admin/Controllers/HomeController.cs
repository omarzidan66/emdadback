using Emdad.Models.Repositories;
using Emdad.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Emdad.Areas.Admin.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Emdad.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]


    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly UserManager<IdentityUser> UserManager;
        public EmdadContext context { get; }
        public IRepository<Sectors> Sectors { get; }
        public IRepository<SectorsServices> SectorsServices { get; }

        public IRepository<Submission> Submission { get; }
        public IRepository<PublicSubmission> PublicSubmission { get; }
        public IRepository<Citizen> Citizen { get; }
        public IRepository<FeedbackAndSuggestion> FeedbackAndSuggestion { get; }
        public IRepository<SubmissionData> SubmissionData { get; }
        public IRepository<FormField> FormField { get; }

        public HomeController(EmdadContext _context,
            IRepository<Sectors> _Sectors,
            IRepository<Submission> _Submission,
            IRepository<PublicSubmission> _PublicSubmission,
            IRepository<Citizen> _CitizenRepository,
            IRepository<FeedbackAndSuggestion> _FeedbackAndSuggestion,
            IRepository<SectorsServices> _SectorsServices,
            IRepository<SubmissionData> _SubmissionData,
            IRepository<FormField> _FormField,
            SignInManager<IdentityUser> _SignInManager,
            UserManager<IdentityUser> _UserManager
            )
        {
            context = _context;
            Sectors = _Sectors;
            Submission = _Submission;
            PublicSubmission = _PublicSubmission;
            Citizen = _CitizenRepository;
            FeedbackAndSuggestion = _FeedbackAndSuggestion;
            SectorsServices = _SectorsServices;
            SubmissionData = _SubmissionData;
            FormField = _FormField;
            SignInManager = _SignInManager;
            UserManager = _UserManager;
        }

        public IActionResult SubmissionDetails(int id)
        {
            var data = (from d in context.SubmissionData
                        join f in context.FormFields on d.FormFieldId equals f.FormFieldId
                        where d.SubmissionId == id && d.IsActive && !d.IsDelete
                        select new SubmissionFieldViewModel
                        {
                            Label = f.FormFieldLabel,
                            Value = d.SubmissionDataValue,
                            Type = f.FormFieldType.ToLower()
                        }).ToList();

            var viewModel = new SubmissionDetailViewModel
            {
                SubmissionId = id,
                Fields = data
            };

            return View(viewModel);
        }

        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var citizen = context.Citizen.FirstOrDefault(c => c.CreateId == userId);

            var data = new HomeModel
            {
                ListSectors = Sectors.ViewClient(),
                ListSectorsServices = SectorsServices.ViewClient(),
                ListSubmission = Submission.ViewClient(),
                PublicSubmission = new PublicSubmission
                {
                    CitizenNationalId = citizen?.CitizenNationalId,
                },
                FeedbackAndSuggestion = new FeedbackAndSuggestion
                {
                    CitizenNationalId = citizen?.CitizenNationalId,

                },
                ListCitizen = Citizen.ViewClient(),
                ListFormField = FormField.ViewClient(),
            };

            return View(data);
        }
        public IActionResult ManageSector(int id,string name)
        {
            var currentUserId = UserManager.GetUserId(User);
            var admin = context.Admins.FirstOrDefault(a => a.IdentityUserId == currentUserId);

            if (admin == null || admin.AssignedSectorId != id)
            {
                return Forbid(); // Unauthorized sector access
            }
            var data = new HomeModel
            {
                ListSectors = Sectors.ViewClient(),
                ListSectorsServices = SectorsServices.ViewClient().Where(s => s.SectorsId == id).ToList(),
                ListSubmission = Submission.ViewClient().Where(s=>s.SectorsId==id).ToList(),
                ListSubmissionData = SubmissionData.ViewClient().Where(s => s.SubmissionId == 26).ToList(),
                ListFormField = FormField.ViewClient(),
            };
            ViewBag.CurrentSectorId = id;
            ViewBag.CurrentStatus = "مقبول";
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
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    model.PublicSubmission.CreateId = userId;
                    model.PublicSubmission.EditId = userId;
                    var name = User.FindFirst(ClaimTypes.Name)?.Value;


                    PublicSubmission.Add(model.PublicSubmission);
                    context.SaveChanges(); 

                    var newSubmission = new Submission
                    {
                        SubmissionName = name,
                        SubmissionStatus = "مقبول",
                        CreateId = userId,
                        EditId = userId
                    };

                    Submission.Add(newSubmission);
                    context.SaveChanges();

                    TempData["FormSuccess"] = "تم إرسال البلاغ بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
                if (model?.FeedbackAndSuggestion != null)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    model.FeedbackAndSuggestion.CreateId = userId;
                    model.FeedbackAndSuggestion.EditId = userId;

                    FeedbackAndSuggestion.Add(model.FeedbackAndSuggestion);
                    context.SaveChanges(); 

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
        [HttpPost]
        public IActionResult ChangeStatus(int submissionId, string status, string returnUrl)
        {
            var admin = context.Submission.FirstOrDefault(a => a.SubmissionId == submissionId);

            if (admin != null)
            {
                admin.SubmissionStatus = status;
                context.SaveChanges();

            }
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl); 

            return RedirectToAction("ManageSector", "Home"); 
        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login", "AdminAccount");
        }
    }
}
