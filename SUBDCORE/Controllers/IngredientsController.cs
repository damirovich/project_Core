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
    public class IngredientsController : Controller
    {
        private readonly ICrudable<Ingredients> ingredientsrepo;
        private readonly ICrudable<RawMaterials> rawMaterialrepi;
        private readonly ICrudable<FinishedProducts> finishedProductsrepo;

        public IngredientsController()
        {
            ingredientsrepo = new Ingredientsrepository();
            rawMaterialrepi = new RawMaterialsrepository();
            finishedProductsrepo = new FinProductrepository();
        }

        // GET: Ingredients
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => ingredientsrepo.GetList()));
        }

        // GET: Ingredients/Create
        public IActionResult Create()
        {
            ViewData["Manufacturing"] = new SelectList(finishedProductsrepo.GetList(), "IdFinishedProducts", "Names");
            ViewData["RawMaterials"] = new SelectList(rawMaterialrepi.GetList(), "IdRawMaterials", "Names");
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdIngredients,Manufacturing,RawMaterials,Quantity")] Ingredients ingredients)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => ingredientsrepo.Create(ingredients));
                return RedirectToAction(nameof(Index));
            }
            ViewData["Manufacturing"] = new SelectList(finishedProductsrepo.GetList(), "IdFinishedProducts", "Names", ingredients.Manufacturing);
            ViewData["RawMaterials"] = new SelectList(rawMaterialrepi.GetList(), "IdRawMaterials", "Names", ingredients.RawMaterials);
            return View(ingredients);
        }

        // GET: Ingredients/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await Task.Run(() => ingredientsrepo.GetList().FirstOrDefault(p => p.IdIngredients == id));
            if (ingredients == null)
            {
                return NotFound();
            }
            ViewData["Manufacturing"] = new SelectList(finishedProductsrepo.GetList(), "IdFinishedProducts", "Names", ingredients.Manufacturing);
            ViewData["RawMaterials"] = new SelectList(rawMaterialrepi.GetList(), "IdRawMaterials", "Names", ingredients.RawMaterials);
            return View(ingredients);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdIngredients,Manufacturing,RawMaterials,Quantity")] Ingredients ingredients)
        {
            if (id != ingredients.IdIngredients)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => ingredientsrepo.Update(ingredients));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientsExists(ingredients.IdIngredients))
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
            ViewData["Manufacturing"] = new SelectList(finishedProductsrepo.GetList(), "IdFinishedProducts", "Names", ingredients.Manufacturing);
            ViewData["RawMaterials"] = new SelectList(rawMaterialrepi.GetList(), "IdRawMaterials", "Names", ingredients.RawMaterials);
            return View(ingredients);
        }

        // GET: Ingredients/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await Task.Run(() => ingredientsrepo.GetList().FirstOrDefault(p => p.IdIngredients == id));
            if (ingredients == null)
            {
                return NotFound();
            }

            return View(ingredients);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            await Task.Run(() => ingredientsrepo.Delete(id));
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientsExists(short id)
        {
            return ingredientsrepo.GetList().Any(p => p.IdIngredients == id);
        }
    }
}
