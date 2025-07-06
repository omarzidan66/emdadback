using Emdad.Models;
using Emdad.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Emdad.Controllers
{
    public class SectorsServicesController : Controller
    {
        private readonly IRepository<SectorsServices> _repository;
        private readonly EmdadContext _context;

        public SectorsServicesController(IRepository<SectorsServices> repository, EmdadContext context)
        {
            _repository = repository;
            _context = context;
        }

        // GET: SectorsServicesController
        public IActionResult Index( int id)
        {
            var sector = _context.Sectors
       .FirstOrDefault(s => s.SectorsId == id);

            

            ViewBag.SectorName = sector.SectorsName;
            var services = _context.SectorsServices
    .Include(s => s.Sectors) // <- this is key
    .Where(s => s.SectorsId == id)
    .ToList();

            return View(services);
        }

        // GET: SectorsServicesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SectorsServicesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SectorsServicesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SectorsServices collection)
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

        // GET: SectorsServicesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SectorsServicesController/Edit/5
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

        // GET: SectorsServicesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SectorsServicesController/Delete/5
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
