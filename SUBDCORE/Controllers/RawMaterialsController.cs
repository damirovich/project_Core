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
    public class RawMaterialsController : Controller
    {
        private readonly ICrudable<RawMaterials> rawMaterialsrepo;
        private readonly ICrudable<UnitOfMeasure> unitsrepo;

        public RawMaterialsController()
        {
            rawMaterialsrepo = new RawMaterialsrepository();
            unitsrepo = new UnitOfMeasurerepository();
        }

        // GET: RawMaterials
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => rawMaterialsrepo.GetList()));
        }
        // GET: RawMaterials/Create
        public IActionResult Create()
        {
            ViewData["UnitOfMeasure"] = new SelectList(unitsrepo.GetList(), "IdUnitOfmeasure", "Names");
            return View();
        }

        // POST: RawMaterials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRawMaterials,Names,UnitOfMeasure,Summ,Quantity")] RawMaterials rawMaterials)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => rawMaterialsrepo.Create(rawMaterials));
                return RedirectToAction(nameof(Index));
            }
            ViewData["UnitOfMeasure"] = new SelectList(unitsrepo.GetList(), "IdUnitOfmeasure", "Names", rawMaterials.UnitOfMeasure);
            return View(rawMaterials);
        }

        // GET: RawMaterials/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rawMaterials = await Task.Run(() => rawMaterialsrepo.GetList().FirstOrDefault(p => p.IdRawMaterials == id));
            if (rawMaterials == null)
            {
                return NotFound();
            }
            ViewData["UnitOfMeasure"] = new SelectList(unitsrepo.GetList(), "IdUnitOfmeasure", "Names", rawMaterials.UnitOfMeasure);
            return View(rawMaterials);
        }

        // POST: RawMaterials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdRawMaterials,Names,UnitOfMeasure,Summ,Quantity")] RawMaterials rawMaterials)
        {
            if (id != rawMaterials.IdRawMaterials)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => rawMaterialsrepo.Update(rawMaterials));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RawMaterialsExists(rawMaterials.IdRawMaterials))
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
            ViewData["UnitOfMeasure"] = new SelectList(unitsrepo.GetList(), "IdUnitOfmeasure", "Names", rawMaterials.UnitOfMeasure);
            return View(rawMaterials);
        }

        // GET: RawMaterials/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rawMaterials = await Task.Run(() => rawMaterialsrepo.GetList().FirstOrDefault(p => p.IdRawMaterials == id));
            if (rawMaterials == null)
            {
                return NotFound();
            }

            return View(rawMaterials);
        }

        // POST: RawMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            await Task.Run(() => rawMaterialsrepo.Delete(id));
            return RedirectToAction(nameof(Index));
        }

        private bool RawMaterialsExists(short id)
        {
            return rawMaterialsrepo.GetList().Any(p => p.IdRawMaterials == id);
        }
    }
}
