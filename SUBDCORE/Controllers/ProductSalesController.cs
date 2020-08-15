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
    public class ProductSalesController : Controller
    {
        private readonly ICrudable<ProductSales> productSalesrepo;
        private readonly ICrudable<FinishedProducts> finishedProductsrepo;
        private readonly ICrudable<Employees> employeesrepo;

        public ProductSalesController()
        {
            productSalesrepo = new ProductSalesrepository();
            finishedProductsrepo = new FinProductrepository();
            employeesrepo = new Employeerepository();
        }

        // GET: ProductSales
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => productSalesrepo.GetList()));
        }

        // GET: ProductSales/Create
        public IActionResult Create()
        {
            ViewBag.message = "";
            ViewData["Employees"] = new SelectList(employeesrepo.GetList(), "IdEmployees", "FullName");
            ViewData["FinishedProduct"] = new SelectList(finishedProductsrepo.GetList(), "IdFinishedProducts", "Names");
            return View();
        }

        // POST: ProductSales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProductSales,FinishedProduct,Quantity,Summ,Date,Employees")] ProductSales productSales)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => productSalesrepo.Create(productSales));
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ViewBag.message = "Недостаточно колличество продукции для продажи";
                    ViewData["Employees"] = new SelectList(employeesrepo.GetList(), "IdEmployees", "FullName", productSales.Employees);
                    ViewData["FinishedProduct"] = new SelectList(finishedProductsrepo.GetList(), "IdFinishedProducts", "Names", productSales.FinishedProduct);
                    return View(productSales);
                }

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductSales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSales = await Task.Run(() => productSalesrepo.GetList().FirstOrDefault(p => p.IdProductSales == id));
            if (productSales == null)
            {
                return NotFound();
            }
            ViewData["Employees"] = new SelectList(employeesrepo.GetList(), "IdEmployees", "FullName", productSales.Employees);
            ViewData["FinishedProduct"] = new SelectList(finishedProductsrepo.GetList(), "IdFinishedProducts", "Names", productSales.FinishedProduct);
            return View(productSales);
        }

        // POST: ProductSales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProductSales,FinishedProduct,Quantity,Summ,Date,Employees")] ProductSales productSales)
        {
            if (id != productSales.IdProductSales)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => productSalesrepo.Update(productSales));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSalesExists(productSales.IdProductSales))
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
            ViewData["Employees"] = new SelectList(employeesrepo.GetList(), "IdEmployees", "FullName", productSales.Employees);
            ViewData["FinishedProduct"] = new SelectList(finishedProductsrepo.GetList(), "IdFinishedProducts", "Names", productSales.FinishedProduct);
            return View(productSales);
        }

        // GET: ProductSales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSales = await Task.Run(() => productSalesrepo.GetList().FirstOrDefault(p => p.IdProductSales== id));
            if (productSales == null)
            {
                return NotFound();
            }

            return View(productSales);
        }

        // POST: ProductSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Task.Run(() => productSalesrepo.Delete(id));
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSalesExists(int id)
        {
            return productSalesrepo.GetList().Any(p => p.IdProductSales == id);
        }
    }
}
