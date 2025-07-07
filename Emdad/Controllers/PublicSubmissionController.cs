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

            return View(model); 
        }

       
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
