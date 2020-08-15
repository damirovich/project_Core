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
    public class ManufacturesController : Controller
    {
        private readonly ICrudable<Manufacture> manfacturerepo;
        private readonly ICrudable<FinishedProducts> finishedProductsrepo;
        private readonly ICrudable<Employees> employeerepo;

        public ManufacturesController()
        {
            manfacturerepo = new Manufacturerepository();
            finishedProductsrepo = new FinProductrepository();
            employeerepo = new Employeerepository();
        }

        // GET: Manufactures
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => manfacturerepo.GetList()));
        }

        // GET: Manufactures/Create
        public IActionResult Create()
        {
            ViewBag.message = "";
            ViewData["Employees"] = new SelectList(employeerepo.GetList(), "IdEmployees", "FullName");
            ViewData["Production"] = new SelectList(finishedProductsrepo.GetList(), "IdFinishedProducts", "Names");
            return View();
        }

        // POST: Manufactures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdManufacture,Production,Quantity,Date,Employees")] Manufacture manufacture)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => manfacturerepo.Create(manufacture));
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ViewBag.message = "Не достаточно Сырья Для производство продукции";
                    ViewData["Employees"] = new SelectList(employeerepo.GetList(), "IdEmployees", "FullName", manufacture.Employees);
                    ViewData["Production"] = new SelectList(finishedProductsrepo.GetList(), "IdFinishedProducts", "Names", manufacture.Production);
                    return View(manufacture);
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Manufactures/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacture = await Task.Run(() => manfacturerepo.GetList().FirstOrDefault(p => p.IdManufacture == id));
            if (manufacture == null)
            {
                return NotFound();
            }
            ViewData["Employees"] = new SelectList(employeerepo.GetList(), "IdEmployees", "FullName", manufacture.Employees);
            ViewData["Production"] = new SelectList(finishedProductsrepo.GetList(), "IdFinishedProducts", "Names", manufacture.Production);
            return View(manufacture);
        }

        // POST: Manufactures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdManufacture,Production,Quantity,Date,Employees")] Manufacture manufacture)
        {
            if (id != manufacture.IdManufacture)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => manfacturerepo.Update(manufacture));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManufactureExists(manufacture.IdManufacture))
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
            ViewData["Employees"] = new SelectList(employeerepo.GetList(), "IdEmployees", "FullName", manufacture.Employees);
            ViewData["Production"] = new SelectList(finishedProductsrepo.GetList(), "IdFinishedProducts", "Names", manufacture.Production);
            return View(manufacture);
        }

        // GET: Manufactures/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacture = await Task.Run(() => manfacturerepo.GetList().FirstOrDefault(p => p.IdManufacture == id));

            if (manufacture == null)
            {
                return NotFound();
            }

            return View(manufacture);
        }

        // POST: Manufactures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            await Task.Run(() => manfacturerepo.Delete(id));
            return RedirectToAction(nameof(Index));
        }

        private bool ManufactureExists(short id)
        {
            return manfacturerepo.GetList().Any(p => p.IdManufacture == id);
        }
    }
}
