using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Emdad.Models;
using NuGet.Protocol.Core.Types;
using Emdad.Models.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Emdad.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class SectorsController : Controller
    {
        private readonly EmdadContext _context;
        public IRepository<Sectors> Sectors { get; set; }

        public SectorsController(EmdadContext context,
            IRepository<Sectors> _Sectors)
        {
            _context = context;
            Sectors = _Sectors;
        }

        // GET: Admin/Sectors
        public IActionResult Index()
        {
            return View(Sectors.View());
        }

        public ActionResult Update_Active(int id)
        {
            Sectors.Active(id);

            return RedirectToAction(nameof(Index));
        }
        // GET: Admin/Sectors/Details/5
        public  ActionResult Details(int id)
        {
            var sectors = Sectors.Find(id);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("SectorsId,SectorsName,SectorsLink,SectorIcon,SectorDesc,IsActive,IsDelete,CreateId,CreateDate,EditId,EditDate")] Sectors sectors)
        {
            sectors.CreateId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            sectors.EditId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Sectors.Add(sectors);
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Sectors/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Sectors.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("SectorsId,SectorsName,SectorsLink,SectorIcon,SectorDesc,IsActive,IsDelete,CreateId,CreateDate,EditId,EditDate")] Sectors sectors)
        {
            try
            {
                sectors.EditId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Sectors.Update(id, sectors);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Sectors/Delete/5
        public async Task<IActionResult> Delete(int id, Sectors sectors)
        {
            try
            {
                sectors.EditId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Sectors.Delete(id, sectors);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       

        private bool SectorsExists(int id)
        {
            return _context.Sectors.Any(e => e.SectorsId == id);
        }
    }
}
