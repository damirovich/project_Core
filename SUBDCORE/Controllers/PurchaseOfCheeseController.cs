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
    public class PurchaseOfCheeseController : Controller
    {
        private readonly ICrudable<PurchaseOfCheese> purchaserepo;
        private readonly ICrudable<RawMaterials> rawMaterialsrepo;
        private readonly ICrudable<Employees> employeesrepo;
        public PurchaseOfCheeseController()
        {
            purchaserepo = new PurchaseOfCheeserepository();
            rawMaterialsrepo = new RawMaterialsrepository();
            employeesrepo = new Employeerepository();
        }

        // GET: PurchaseOfCheese
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => purchaserepo.GetList()));
        }

        // GET: PurchaseOfCheese/Create
        public IActionResult Create()
        {
            ViewBag.message = "";
            ViewData["Employees"] = new SelectList(employeesrepo.GetList(), "IdEmployees", "FullName");
            ViewData["RawMaterials"] = new SelectList(rawMaterialsrepo.GetList(), "IdRawMaterials", "Names");
            return View();
        }

        // POST: PurchaseOfCheese/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPurchaseOfcheese,RawMaterials,Quantity,Summ,Date,Employees")] PurchaseOfCheese purchaseOfCheese)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => purchaserepo.Create(purchaseOfCheese));
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ViewBag.message = "Недостаточно суммы в бюджете для покупки сырья";
                    ViewData["Employees"] = new SelectList(employeesrepo.GetList(), "IdEmployees", "FullName", purchaseOfCheese.Employees);
                    ViewData["RawMaterials"] = new SelectList(rawMaterialsrepo.GetList(), "IdRawMaterials", "Names", purchaseOfCheese.RawMaterials);
                    return View(purchaseOfCheese);
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: PurchaseOfCheese/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOfCheese = await Task.Run(() => purchaserepo.GetList().FirstOrDefault(p => p.IdPurchaseOfcheese == id));
            if (purchaseOfCheese == null)
            {
                return NotFound();
            }
            ViewData["Employees"] = new SelectList(employeesrepo.GetList(), "IdEmployees", "FullName", purchaseOfCheese.Employees);
            ViewData["RawMaterials"] = new SelectList(rawMaterialsrepo.GetList(), "IdRawMaterials", "Names", purchaseOfCheese.RawMaterials);
            return View(purchaseOfCheese);
        }

        // POST: PurchaseOfCheese/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPurchaseOfcheese,RawMaterials,Quantity,Summ,Date,Employees")] PurchaseOfCheese purchaseOfCheese)
        {
            if (id != purchaseOfCheese.IdPurchaseOfcheese)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => purchaserepo.Update(purchaseOfCheese));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOfCheeseExists(purchaseOfCheese.IdPurchaseOfcheese))
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
            ViewData["Employees"] = new SelectList(employeesrepo.GetList(), "IdEmployees", "FullName", purchaseOfCheese.Employees);
            ViewData["RawMaterials"] = new SelectList(rawMaterialsrepo.GetList(), "IdRawMaterials", "Names", purchaseOfCheese.RawMaterials);
            return View(purchaseOfCheese);
        }

        // GET: PurchaseOfCheese/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOfCheese = await Task.Run(() => purchaserepo.GetList().FirstOrDefault(p => p.IdPurchaseOfcheese == id));

            if (purchaseOfCheese == null)
            {
                return NotFound();
            }

            return View(purchaseOfCheese);
        }

        // POST: PurchaseOfCheese/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Task.Run(() => purchaserepo.Delete(id));
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOfCheeseExists(int id)
        {
            return purchaserepo.GetList().Any(p => p.IdPurchaseOfcheese== id);
        }
    }
}
