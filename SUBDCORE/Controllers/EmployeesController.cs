using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
using SUBDCORE.Models;
using SUBDCORE.Repository;

namespace SUBDCORE.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ICrudable<Employees> employeesrepository;
        private readonly ICrudable<Position> postionrepository;


        public EmployeesController()
        {
            employeesrepository = new Employeerepository();
            postionrepository = new Positionrepository();
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => employeesrepository.GetList()));
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["Position"] = new SelectList(postionrepository.GetList(), "IdPosition", "Position1");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmployees,FullName,Position,Salary,Address,PhoneNumber")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => employeesrepository.Create(employees));
                return RedirectToAction(nameof(Index));
            }
            ViewData["Position"] = new SelectList(postionrepository.GetList(), "IdPosition", "Position1", employees.Position);
            return View(employees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await Task.Run(() => employeesrepository.GetList().FirstOrDefault(p => p.IdEmployees == id));
            if (employees == null)
            {
                return NotFound();
            }
            ViewData["Position"] = new SelectList(postionrepository.GetList(), "IdPosition", "Position1", employees.Position);
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdEmployees,FullName,Position,Salary,Address,PhoneNumber")] Employees employees)
        {
            if (id != employees.IdEmployees)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => employeesrepository.Update(employees));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.IdEmployees))
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
            ViewData["Position"] = new SelectList(postionrepository.GetList(), "IdPosition", "Position1", employees.Position);
            return View(employees);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await Task.Run(() => employeesrepository.GetList().FirstOrDefault(p => p.IdEmployees == id));
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            await Task.Run(() => employeesrepository.Delete(id));
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesExists(short id)
        {
            return employeesrepository.GetList().Any(p => p.IdEmployees == id);
        }
    }
}
