using Emdad.Models.Repositories;
using Emdad.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Emdad.Areas.Admin.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Emdad.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]

    public class AdminController : Controller
    {
        public EmdadContext context { get; }
        public IRepository<Sectors> Sectors { get; }
        public IRepository<PublicSubmission> PublicSubmission { get; }
        public IRepository<FeedbackAndSuggestion> FeedbackAndSuggestion { get; }
        public AdminController(EmdadContext _context,
             IRepository<Sectors> _Sectors,
             IRepository<PublicSubmission> _PublicSubmission,
             IRepository<FeedbackAndSuggestion> _FeedbackAndSuggestion
            )
        {
            context = _context;
            Sectors = _Sectors;
            PublicSubmission = _PublicSubmission;
            FeedbackAndSuggestion = _FeedbackAndSuggestion;
        }
        // GET: AdminController
        public ActionResult AdminManage()
        {
            var admins = context.Admins.Include(a => a.Sector).ToList();
            var sectors = Sectors.ViewClient();

            var model = new AdminManageViewModel
            {
                Admins = admins,
                AllSectors = sectors,
                ListPublicSubmission = PublicSubmission.ViewClient(),
                ListFeedbackAndSuggestion = FeedbackAndSuggestion.ViewClient()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeSector(int adminId, int sectorId)
        {
            var admin = context.Admins.FirstOrDefault(a => a.AdminId == adminId);

            if (admin != null)
            {
                admin.AssignedSectorId = sectorId;
                admin.LastActiveAt = DateTime.Now;
                context.SaveChanges();
                
            }
            return RedirectToAction("AdminManage");
        }
        [HttpPost]
        public IActionResult AssignSector(int submissionId, int sectorId)
        {
            var admin = context.PublicSubmissions.FirstOrDefault(a => a.PublicSubmissionId == submissionId);

            if (admin != null)
            {
                admin.AssignedSectorId = sectorId;
                context.SaveChanges();

            }
            return RedirectToAction("AdminManage");
        }

        [HttpPost]
        public IActionResult ToggleStatus(int adminId, string toggle)
        {
            var admin = context.Admins.FirstOrDefault(a => a.AdminId == adminId);
            if (admin != null)
            {
                if (toggle == "active") admin.IsActive = !admin.IsActive;
                if (toggle == "stop") admin.IsStopped = !admin.IsStopped;
                context.SaveChanges();
            }
            return RedirectToAction("AdminManage");
        }

       
        

      

        
    }
}
