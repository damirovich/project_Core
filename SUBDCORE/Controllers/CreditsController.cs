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
    public class CreditsController : Controller
    {
        private readonly ICrudable<Credits> creditsrepo;
        public CreditsController(ProzivContext context)
        {
            creditsrepo = new Creditsrepository();
        }

        // GET: Credits
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => creditsrepo.GetList()));
        }

        // GET: Credits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Credits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCredits,Name,CreaditAmount,InterestRate,CreditTerm,DateOfIssue,MonthlyPayment,Fine")] Credits credits)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => creditsrepo.Create(credits));
                return RedirectToAction(nameof(Index));
            }
            return View(credits);
        }

        // GET: Credits/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credits = await Task.Run(() => creditsrepo.GetList().FirstOrDefault(p => p.IdCredits == id));
            if (credits == null)
            {
                return NotFound();
            }
            return View(credits);
        }

        // POST: Credits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdCredits,Name,CreaditAmount,InterestRate,CreditTerm,DateOfIssue,MonthlyPayment,Fine")] Credits credits)
        {
            if (id != credits.IdCredits)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => creditsrepo.Update(credits));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditsExists(credits.IdCredits))
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
            return View(credits);
        }

        // GET: Credits/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credits = await Task.Run(() => creditsrepo.GetList().FirstOrDefault(p => p.IdCredits == id));

            if (credits == null)
            {
                return NotFound();
            }

            return View(credits);
        }

        // POST: Credits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            await Task.Run(() => creditsrepo.Delete(id));
            return RedirectToAction(nameof(Index));
        }

        private bool CreditsExists(short id)
        {
            return creditsrepo.GetList().Any(p => p.IdCredits == id);
        }
    }
}
