using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Emdad.Models;

namespace Emdad.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SectorsServicesController : Controller
    {
        private readonly EmdadContext _context;

        public SectorsServicesController(EmdadContext context)
        {
            _context = context;
        }

        // GET: Admin/SectorsServices
        public async Task<IActionResult> Index()
        {
            var emdadContext = _context.SectorsServices.Include(s => s.Sectors);
            return View(await emdadContext.ToListAsync());
        }

        // GET: Admin/SectorsServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectorsServices = await _context.SectorsServices
                .Include(s => s.Sectors)
                .FirstOrDefaultAsync(m => m.SectorsServicesId == id);
            if (sectorsServices == null)
            {
                return NotFound();
            }

            return View(sectorsServices);
        }

        // GET: Admin/SectorsServices/Create
        public IActionResult Create()
        {
            ViewData["SectorsId"] = new SelectList(_context.Sectors, "SectorsId", "SectorsId");
            return View();
        }

        // POST: Admin/SectorsServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectorsServicesId,SectorsServicesName,SectorsServicesType,SectorsServicesLink,SectorsId,SectorsName,IsActive,IsDelete,CreateId,CreateDate,EditId,EditDate")] SectorsServices sectorsServices)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sectorsServices);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SectorsId"] = new SelectList(_context.Sectors, "SectorsId", "SectorsId", sectorsServices.SectorsId);
            return View(sectorsServices);
        }

        // GET: Admin/SectorsServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectorsServices = await _context.SectorsServices.FindAsync(id);
            if (sectorsServices == null)
            {
                return NotFound();
            }
            ViewData["SectorsId"] = new SelectList(_context.Sectors, "SectorsId", "SectorsId", sectorsServices.SectorsId);
            return View(sectorsServices);
        }

        // POST: Admin/SectorsServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SectorsServicesId,SectorsServicesName,SectorsServicesType,SectorsServicesLink,SectorsId,SectorsName,IsActive,IsDelete,CreateId,CreateDate,EditId,EditDate")] SectorsServices sectorsServices)
        {
            if (id != sectorsServices.SectorsServicesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sectorsServices);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectorsServicesExists(sectorsServices.SectorsServicesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SectorsId"] = new SelectList(_context.Sectors, "SectorsId", "SectorsId", sectorsServices.SectorsId);
            return View(sectorsServices);
        }

        // GET: Admin/SectorsServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectorsServices = await _context.SectorsServices
                .Include(s => s.Sectors)
                .FirstOrDefaultAsync(m => m.SectorsServicesId == id);
            if (sectorsServices == null)
            {
                return NotFound();
            }

            return View(sectorsServices);
        }

        // POST: Admin/SectorsServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sectorsServices = await _context.SectorsServices.FindAsync(id);
            if (sectorsServices != null)
            {
                _context.SectorsServices.Remove(sectorsServices);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectorsServicesExists(int id)
        {
            return _context.SectorsServices.Any(e => e.SectorsServicesId == id);
        }
    }
}
