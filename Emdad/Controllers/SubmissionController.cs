using Emdad.Models.Repositories;
using Emdad.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Emdad.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly IRepository<Submission> _repository;
        private readonly EmdadContext _context;

        public SubmissionController(IRepository<Submission> repository, EmdadContext context)
        {
            _repository = repository;
            _context = context;
        }
        // GET: SubmissionController
        [HttpPost]
        public ActionResult Index(int id, IFormCollection form)
        {
            // 1. Get the user's username (from cookie / identity)
           

            // 3. Create the Submission
            var submission = new Submission
            {
                SectorsServicesId = id,
                SubmissionStatus = "Pending"
            };

            _context.Submission.Add(submission);
            _context.SaveChanges();

            // 4. Get form fields for this service
            var formFields = _context.FormFields
                .Where(f => f.SectorsServicesId == id)
                .ToList();

            // 5. Save each input from form
            foreach (var field in formFields)
            {
                var value = form[field.FormFieldName];

                var submissionData = new SubmissionData
                {
                    
                    SubmissionId = submission.SubmissionId,
                    FormFieldId = field.FormFieldId,
                    SubmissionDataValue = value
                };

                _context.SubmissionData.Add(submissionData);
            }

            _context.SaveChanges();

            // 6. Redirect to Home
            return RedirectToAction("Index", "Home");
        }



        // GET: SubmissionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SubmissionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubmissionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Submission collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    collection.CreateId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    collection.EditId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                    _repository.Add(collection);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Required values are missing.");
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: SubmissionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubmissionController/Edit/5
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

        // GET: SubmissionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubmissionController/Delete/5
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
