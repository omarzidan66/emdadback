using Emdad.Models.Repositories;
using Emdad.Models;
using Emdad.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Emdad.Controllers
{
    public class PublicSubmissionController : Controller
    {
        private readonly IRepository<PublicSubmission> _repository;
        private readonly IRepository<Submission> _subRepository;
        private readonly EmdadContext _context;

        public PublicSubmissionController(IRepository<PublicSubmission> repository, EmdadContext context, IRepository<Submission> subRepository)
        {
            _repository = repository;
            _context = context;
            _subRepository = subRepository;
        }

        // GET: PublicSubmissionController/Create
        public ActionResult Create()
        {
            var model = new HomeModel
            {
                PublicSubmission = new PublicSubmission()
            };

            return View(model); // Only called if you're using a standalone Create view (not required in your case)
        }

        // POST: Save PublicSubmission and generate corresponding Submission
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Prefix = "PublicSubmission")] PublicSubmission collection)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            // Audit info
        //            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //            collection.CreateId = userId;
        //            collection.EditId = userId;

        //            // Save PublicSubmission
        //            _repository.Add(collection);
        //            _context.SaveChanges(); // Ensure PublicSubmissionId is generated

        //            // ✅ Validate FK existence
        //            var fallbackSector = _context.SectorsServices.FirstOrDefault(s => s.SectorsServicesName == "بلاغ من الجمهور");
        //            if (fallbackSector == null)
        //            {
        //                // Insert it if missing
        //                var newSector = new SectorsServices
        //                {
        //                    SectorsServicesName = "بلاغ من الجمهور"
        //                };
        //                _context.SectorsServices.Add(newSector);
        //                _context.SaveChanges();
        //                fallbackSector = newSector;
        //            }

        //            // Create Submission record
        //            var submission = new Submission
        //            {
        //                SectorsServicesId = fallbackSector.SectorsServicesId,
        //                SectorsServicesName = fallbackSector.SectorsServicesName,
        //                SubmissionStatus = "pending",
        //                CreateId = collection.CreateId,
        //                EditId = collection.EditId
        //            };

        //            _subRepository.Add(submission);
        //            _context.SaveChanges();

        //            TempData["FormSuccess"] = "تم استلام البلاغ وتم تحويله إلى الإدارة المختصة.";
        //            return RedirectToAction("Index", "Home");
        //        }

        //        TempData["FormError"] = "يرجى تعبئة كافة الحقول المطلوبة.";
        //        return RedirectToAction("Index", "Home");
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["FormError"] = "حدث خطأ غير متوقع أثناء حفظ البلاغ.";
        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        // Placeholder methods — you can delete if unused
        public ActionResult Details(int id) => View();
        public ActionResult Edit(int id) => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection) => RedirectToAction("Index", "Home");

        public ActionResult Delete(int id) => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) => RedirectToAction("Index", "Home");
    }
}
