using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SUBDCORE.Models;
using SUBDCORE.Repository;
namespace SUBDCORE.Controllers
{
    public class UnitOfMeasuresController : Controller
    {
        private readonly ICrudable<UnitOfMeasure> unitsrepo;

        public UnitOfMeasuresController()
        {
            unitsrepo = new UnitOfMeasurerepository();
        }

        // GET: UnitOfMeasures
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => unitsrepo.GetList()));
        }
        // GET: UnitOfMeasures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnitOfMeasures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUnitOfmeasure,Names")] UnitOfMeasure unitOfMeasure)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => unitsrepo.Create(unitOfMeasure));
                return RedirectToAction(nameof(Index));
            }
            return View(unitOfMeasure);
        }

        // GET: UnitOfMeasures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitOfMeasure = await Task.Run(() => unitsrepo.GetList().FirstOrDefault(p => p.IdUnitOfmeasure== id));
            if (unitOfMeasure == null)
            {
                return NotFound();
            }
            return View(unitOfMeasure);
        }

        // POST: UnitOfMeasures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUnitOfmeasure,Names")] UnitOfMeasure unitOfMeasure)
        {
            if (id != unitOfMeasure.IdUnitOfmeasure)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => unitsrepo.Update(unitOfMeasure));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitOfMeasureExists(unitOfMeasure.IdUnitOfmeasure))
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
            return View(unitOfMeasure);
        }

        // GET: UnitOfMeasures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitOfMeasure = await Task.Run(() => unitsrepo.GetList().FirstOrDefault(m => m.IdUnitOfmeasure == id));
            if (unitOfMeasure == null)
            {
                return NotFound();
            }

            return View(unitOfMeasure);
        }

        // POST: UnitOfMeasures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Task.Run(() => unitsrepo.Delete(id));
            return RedirectToAction(nameof(Index));
        }

        private bool UnitOfMeasureExists(int id)
        {
            return unitsrepo.GetList().Any(p => p.IdUnitOfmeasure == id);
        }
    }
}
