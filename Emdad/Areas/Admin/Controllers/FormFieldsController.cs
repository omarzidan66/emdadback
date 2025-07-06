using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Emdad.Models;
using Emdad.Models.Repositories;

namespace Emdad.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public async Task<IActionResult> Index()
        {
            var emdadContext = _context.FormFields.Include(f => f.SectorsServices);
            return View(await emdadContext.ToListAsync());
        }

        // GET: Admin/FormFields/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formField = await _context.FormFields
                .Include(f => f.SectorsServices)
                .FirstOrDefaultAsync(m => m.FormFieldId == id);
            if (formField == null)
            {
                return NotFound();
            }

            return View(formField);
        }

        // GET: Admin/FormFields/Create
        public IActionResult Create()
        {
            ViewData["SectorsServicesId"] = new SelectList(_context.SectorsServices, "SectorsServicesId", "SectorsServicesId");
            return View();
        }

        // POST: Admin/FormFields/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormFieldId,SectorsServicesId,FormFieldLabel,FormFieldName,FormFieldType,FormFieldIsRequired,IsActive,IsDelete,CreateId,CreateDate,EditId,EditDate")] FormField formField)
        {
            
                FormFieldRepository.Add(formField);
                //await FormFieldRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["SectorsServicesId"] = new SelectList(_context.SectorsServices, "SectorsServicesId", "SectorsServicesId", formField.SectorsServicesId);
            return View(formField);
        }

        // GET: Admin/FormFields/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formField = await _context.FormFields.FindAsync(id);
            if (formField == null)
            {
                return NotFound();
            }
            ViewData["SectorsServicesId"] = new SelectList(_context.SectorsServices, "SectorsServicesId", "SectorsServicesId", formField.SectorsServicesId);
            return View(formField);
        }

        // POST: Admin/FormFields/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormFieldId,SectorsServicesId,FormFieldLabel,FormFieldName,FormFieldType,FormFieldIsRequired,IsActive,IsDelete,CreateId,CreateDate,EditId,EditDate")] FormField formField)
        {
            if (id != formField.FormFieldId)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(formField);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormFieldExists(formField.FormFieldId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            //ViewData["SectorsServicesId"] = new SelectList(_context.SectorsServices, "SectorsServicesId", "SectorsServicesId", formField.SectorsServicesId);
            return View(formField);
        }

        // GET: Admin/FormFields/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formField = await _context.FormFields
                .Include(f => f.SectorsServices)
                .FirstOrDefaultAsync(m => m.FormFieldId == id);
            if (formField == null)
            {
                return NotFound();
            }

            return View(formField);
        }

        // POST: Admin/FormFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formField = await _context.FormFields.FindAsync(id);
            if (formField != null)
            {
                _context.FormFields.Remove(formField);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormFieldExists(int id)
        {
            return _context.FormFields.Any(e => e.FormFieldId == id);
        }
    }
}
