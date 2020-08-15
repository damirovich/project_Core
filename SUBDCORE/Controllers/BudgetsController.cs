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
    public class BudgetsController : Controller
    {
      //  private readonly ProzivContext _context;
        private readonly Budgetrepository budgetrepository;


        public BudgetsController()
        {
           // _context = context;
            budgetrepository = new Budgetrepository();
        }

        // GET: Budgets
        public async Task<IActionResult> Index()
        {
            return View(budgetrepository.GetList());
        }

        // GET: Budgets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await Task.Run(() => budgetrepository.GetList().FirstOrDefault(p => p.IdBudget == id));
            if (budget == null)
            {
                return NotFound();
            }
            return View(budget);
        }

        // POST: Budgets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBudget,BudgetSum,Prosent,ProsentSalary")] Budget budget)
        {
            if (id != budget.IdBudget)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => budgetrepository.Update(budget));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetExists(budget.IdBudget))
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
            return View(budget);
        }
        private bool BudgetExists(int id)
        {
             return budgetrepository.GetList().Any(p => p.IdBudget == id);
        }
    }
}
