using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Emdad.Models;
using Emdad.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Emdad.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class FormFieldsController : Controller
    {
        private readonly EmdadContext _context;
        public IRepository<FormField> FormFieldRepository { get; set; }

        public FormFieldsController(EmdadContext context, IRepository<FormField> _FormFieldRepository)
        {
            _context = context;
            FormFieldRepository = _FormFieldRepository;
        }

        // GET: Admin/FormFields
        public IActionResult Index()
        {

            return View(FormFieldRepository.View());

        }

        // GET: Admin/FormFields/Details/5
        public ActionResult Details(int id)
        {
            var data = _context.FormFields
                    .Include(f => f.SectorsServices)
                    .FirstOrDefault(f => f.FormFieldId == id);

            if (data == null)
                return NotFound();

            return View(data);
        }

        // GET: Admin/FormFields/Create
        public ActionResult Create()
        {
            ViewData["SectorsServicesId"] = new SelectList(_context.SectorsServices, "SectorsServicesId", "SectorsServicesId");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("FormFieldId,SectorsServicesId,FormFieldLabel,FormFieldName,FormFieldType,FormFieldIsRequired,IsActive,IsDelete,CreateId,CreateDate,EditId,EditDate")] FormField formField)
        {

            formField.CreateId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            formField.EditId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            FormFieldRepository.Add(formField);
            ViewData["SectorsServicesId"] = new SelectList(_context.SectorsServices, "SectorsServicesId", "SectorsServicesId", formField.SectorsServicesId);

            return RedirectToAction(nameof(Index));

           
        }

        // GET: Admin/FormFields/Edit/5
        public ActionResult Edit(int id)
        {
            var data = FormFieldRepository.Find(id);
            ViewData["SectorsServicesId"] = new SelectList(
            _context.SectorsServices,
            "SectorsServicesId",
            "SectorsServicesId", // Or replace with a display name field like "SectorName"
            data.SectorsServicesId // selected value
        );

            return View(data);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("FormFieldId,SectorsServicesId,FormFieldLabel,FormFieldName,FormFieldType,FormFieldIsRequired,IsActive,IsDelete,CreateId,CreateDate,EditId,EditDate")] FormField formField)
        {
            try
            {
                formField.EditId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewData["SectorsServicesId"] = new SelectList(_context.SectorsServices, "SectorsServicesId", "SectorsServicesId", formField.SectorsServicesId);

                FormFieldRepository.Update(id, formField);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/FormFields/Delete/5
        public IActionResult Delete(int id)
        {
            var formField = FormFieldRepository.Find(id);
            if (formField == null)
                return NotFound();

            ViewData["SectorsServicesId"] = new SelectList(
                _context.SectorsServices,
                "SectorsServicesId",
                "SectorsServicesId",
                formField.SectorsServicesId
            );

            return View(formField); // shows confirmation page
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var formField = _context.FormFields.Find(id);
            if (formField == null)
                return NotFound();

            formField.EditId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            FormFieldRepository.Delete(id, formField);


            return RedirectToAction(nameof(Index));
        }

        private bool FormFieldExists(int id)
        {
            return _context.FormFields.Any(e => e.FormFieldId == id);
        }
    }
}
