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
    public class FinishedProductsController : Controller
    {

        private readonly ICrudable<FinishedProducts> finishedProductsrepo;
        private readonly ICrudable<UnitOfMeasure> unitsrepo;
        public FinishedProductsController(ProzivContext context)
        {
            finishedProductsrepo = new FinProductrepository();
            unitsrepo = new UnitOfMeasurerepository();
        }

        // GET: FinishedProducts
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => finishedProductsrepo.GetList()));
        }
        // GET: FinishedProducts/Create
        public IActionResult Create()
        {
            ViewData["UnitOfMeasure"] = new SelectList(unitsrepo.GetList(), "IdUnitOfmeasure", "Names");
            return View();
        }

        // POST: FinishedProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFinishedProducts,Names,UnitOfMeasure,Summ,Quantity")] FinishedProducts finishedProducts)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => finishedProductsrepo.Create(finishedProducts));
                return RedirectToAction(nameof(Index));
            }
            ViewData["UnitOfMeasure"] = new SelectList(unitsrepo.GetList(), "IdUnitOfmeasure", "Names", finishedProducts.UnitOfMeasure);
            return View(finishedProducts);
        }

        // GET: FinishedProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finishedProducts = await Task.Run(() => finishedProductsrepo.GetList().FirstOrDefault(p => p.IdFinishedProducts == id));
            if (finishedProducts == null)
            {
                return NotFound();
            }
            ViewData["UnitOfMeasure"] = new SelectList(unitsrepo.GetList(), "IdUnitOfmeasure", "Names", finishedProducts.UnitOfMeasure);
            return View(finishedProducts);
        }

        // POST: FinishedProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFinishedProducts,Names,UnitOfMeasure,Summ,Quantity")] FinishedProducts finishedProducts)
        {
            if (id != finishedProducts.IdFinishedProducts)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => finishedProductsrepo.Update(finishedProducts));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinishedProductsExists(finishedProducts.IdFinishedProducts))
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
            ViewData["UnitOfMeasure"] = new SelectList(unitsrepo.GetList(), "IdUnitOfmeasure", "Names", finishedProducts.UnitOfMeasure);
            return View(finishedProducts);
        }

        // GET: FinishedProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finishedProducts = await Task.Run(() => finishedProductsrepo.GetList().FirstOrDefault(p => p.IdFinishedProducts == id));

            if (finishedProducts == null)
            {
                return NotFound();
            }

            return View(finishedProducts);
        }

        // POST: FinishedProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Task.Run(() => finishedProductsrepo.Delete(id));
            return RedirectToAction(nameof(Index));
        }

        private bool FinishedProductsExists(int id)
        {
            return finishedProductsrepo.GetList().Any(p => p.IdFinishedProducts== id);
        }
    }
}
