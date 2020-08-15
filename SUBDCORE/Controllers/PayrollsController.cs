using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SUBDCORE.Models;

namespace SUBDCORE.Controllers
{
    public class PayrollsController : Controller
    {
        private readonly ProzivContext _context;

        public PayrollsController(ProzivContext context)
        {
            _context = context;
        }

        // GET: Payrolls
        public async Task<IActionResult> Index()
        {
            var prozivContext = _context.Payroll.Include(p => p.IdEmployeeNavigation).Include(p => p.MonthNavigation).Include(p => p.YearsNavigation);
            return View(await prozivContext.ToListAsync());
        }
        // GET: Payrolls/Create
        public IActionResult Create()
        {
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployees", "FullName");
            ViewData["Month"] = new SelectList(_context.Month, "IdMonth", "MonthName");
            ViewData["Years"] = new SelectList(_context.Years, "IdYears", "YearsName");
            return View();
        }

        // POST: Payrolls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Payroll collection)
        {
            ProzivContext DB = new ProzivContext();
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployees", "IdEmployees", collection.IdEmployee);
            ViewData["Month"] = new SelectList(_context.Month, "IdMonth", "IdMonth", collection.Month);
            ViewData["Years"] = new SelectList(_context.Years, "IdYears", "IdYears", collection.Years);
            string connectionString = @"Data Source=DESKTOP-1BOJ113\ACER;Initial Catalog=Proziv;Integrated Security=True";
            string sqlExpression = "SP_Payroll";
            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {

                connection.Open();
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                System.Data.SqlClient.SqlParameter empParameter = new System.Data.SqlClient.SqlParameter
                {
                    ParameterName = "@EmployeeID",
                    Value = collection.IdEmployee
                };
                command.Parameters.Add(empParameter);
                System.Data.SqlClient.SqlParameter yearParameter = new System.Data.SqlClient.SqlParameter
                {
                    ParameterName = "@year",
                    Value = collection.Years
                };
                command.Parameters.Add(yearParameter);
                System.Data.SqlClient.SqlParameter monthParameter = new System.Data.SqlClient.SqlParameter
                {
                    ParameterName = "@month",
                    Value = collection.Month
                };
                command.Parameters.Add(monthParameter);
                System.Data.SqlClient.SqlParameter salaryParametr = new System.Data.SqlClient.SqlParameter
                {
                    ParameterName = "@salary",
                    Value = collection.SumSalary
                };
                command.Parameters.Add(salaryParametr);
                var result = command.ExecuteNonQuery();
                //  return NotFound("Не достаточно суммы бюджета для выдачи зарплаты");

            }

            List<Payroll> list = DB.Payroll.ToList();
            Payroll payroll = list.Last();
            return RedirectToAction(nameof(Index));

        }

        // GET: Payrolls/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payroll = await _context.Payroll.FindAsync(id);
            if (payroll == null)
            {
                return NotFound();
            }
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployees", "FullName", payroll.IdEmployee);
            ViewData["Month"] = new SelectList(_context.Month, "IdMonth", "MonthName", payroll.Month);
            ViewData["Years"] = new SelectList(_context.Years, "IdYears", "YearsName", payroll.Years);
            return View(payroll);
        }

        // POST: Payrolls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdSalary,IdEmployee,Years,Month,Purchased,Sales,Manufacture,WorkPerformed,SumSalary,DataPayroll,ProsentZp,SummaSalary,WagePrem,InitalProsent,InitalSalary")] Payroll payroll)
        {
            if (id != payroll.IdSalary)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payroll);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayrollExists(payroll.IdSalary))
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
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployees", "FullName", payroll.IdEmployee);
            ViewData["Month"] = new SelectList(_context.Month, "IdMonth", "MonthName", payroll.Month);
            ViewData["Years"] = new SelectList(_context.Years, "IdYears", "YearsName", payroll.Years);
            return View(payroll);
        }

        // GET: Payrolls/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payroll = await _context.Payroll
                .Include(p => p.IdEmployeeNavigation)
                .Include(p => p.MonthNavigation)
                .Include(p => p.YearsNavigation)
                .FirstOrDefaultAsync(m => m.IdSalary == id);
            if (payroll == null)
            {
                return NotFound();
            }

            return View(payroll);
        }

        // POST: Payrolls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var payroll = await _context.Payroll.FindAsync(id);
            _context.Payroll.Remove(payroll);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayrollExists(short id)
        {
            return _context.Payroll.Any(e => e.IdSalary == id);
        }
    }
}
