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
    public class SectorsController : Controller
    {
        private readonly EmdadContext _context;

        public SectorsController(EmdadContext context)
        {
            _context = context;
        }

        // GET: Admin/Sectors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sectors.ToListAsync());
        }

        // GET: Admin/Sectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectors = await _context.Sectors
                .FirstOrDefaultAsync(m => m.SectorsId == id);
            if (sectors == null)
            {
                return NotFound();
            }

            return View(sectors);
        }

        // GET: Admin/Sectors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectorsId,SectorsName,SectorsLink,SectorIcon,SectorDesc,IsActive,IsDelete,CreateId,CreateDate,EditId,EditDate")] Sectors sectors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sectors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sectors);
        }

        // GET: Admin/Sectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectors = await _context.Sectors.FindAsync(id);
            if (sectors == null)
            {
                return NotFound();
            }
            return View(sectors);
        }

        // POST: Admin/Sectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SectorsId,SectorsName,SectorsLink,SectorIcon,SectorDesc,IsActive,IsDelete,CreateId,CreateDate,EditId,EditDate")] Sectors sectors)
        {
            if (id != sectors.SectorsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sectors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectorsExists(sectors.SectorsId))
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
            return View(sectors);
        }

        // GET: Admin/Sectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectors = await _context.Sectors
                .FirstOrDefaultAsync(m => m.SectorsId == id);
            if (sectors == null)
            {
                return NotFound();
            }

            return View(sectors);
        }

        // POST: Admin/Sectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sectors = await _context.Sectors.FindAsync(id);
            if (sectors != null)
            {
                _context.Sectors.Remove(sectors);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectorsExists(int id)
        {
            return _context.Sectors.Any(e => e.SectorsId == id);
        }
    }
}
