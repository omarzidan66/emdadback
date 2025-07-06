using Emdad.Models.Repositories;
using Emdad.Models;
using Emdad.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Emdad.Controllers
{
    public class FormFieldController : Controller
    {
        // GET: FormFieldController
        private readonly IRepository<FormField> _repository;
        private readonly EmdadContext _context;

        public FormFieldController(IRepository<FormField> repository, EmdadContext context)
        {
            _repository = repository;
            _context = context;
        }

        // GET: FormFieldController
        public IActionResult Index(string name, int id)
        {
            var model = new FormFieldModel
            {
                ServiceId = id,
                Fields = _context.FormFields
        .Where(f => f.SectorsServicesId == id)
        .ToList()
            };
            return View(model);
        }

        // GET: SFormFieldController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FormFieldController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SFormFieldController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormField collection)
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

        // GET: FormFieldController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FormFieldController/Edit/5
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

        // GET: FormFieldController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FormFieldController/Delete/5
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
