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
    public class LoanRepaymentsController : Controller
    { 
        private readonly ICrudable<LoanRepayment> loanRepaymentRepo;
        private readonly ICrudable<Credits> creditsRepsitory;
        private readonly MonthRepository monthRepository;
      //  private readonly YearsRepository yearsRepository;

        public LoanRepaymentsController()
        {
            loanRepaymentRepo = new LoanRepaymentRepository();
            creditsRepsitory = new Creditsrepository();
            monthRepository = new MonthRepository();
          //  yearsRepository = new YearsRepository();
        }
        
        // GET: LoanRepayments
        public IActionResult Index()
        {
            ViewData["CreaditId"] = new SelectList(creditsRepsitory.GetList(), "IdCredits", "Name");
            return View();
        }
        public async Task<IActionResult> View([Bind("CreaditId")] int creditId)
        {
            return View(await Task.Run(() => loanRepaymentRepo.GetList().Where(p => p.CreaditId == creditId)));
        }
       
        // GET: LoanRepayments/Create
        public IActionResult Create()
        {
            ViewData["CreaditId"] = new SelectList(creditsRepsitory.GetList(), "IdCredits", "Name");
            ViewData["MonthForPay"] = new SelectList(monthRepository.getMonths(), "IdMonth", "MonthName");
           // ViewData["YearForPay"] = new SelectList(yearsRepository.getYears(), "IdYears", "YearsName");
            return View();
        }

        // POST: LoanRepayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLoanRepayment,CreaditId,MonthForPay,YearForPay,DateOfPay,MonthlyPayment,ExpiredDays,Fine,TotalPayment")] LoanRepayment loanRepayment)
        {
            if (ModelState.IsValid)
            {
                if (loanRepayment.GetPermission() == 1)
                {
                    await Task.Run(() => loanRepaymentRepo.Create(loanRepayment));
                    return RedirectToAction(nameof(Index));
                }
                else if (loanRepayment.GetPermission() == 0)   return NotFound("Credit fully paid");
                else if (loanRepayment.GetPermission() == 2)   return NotFound("Amount of budget is not enough");
                else if (loanRepayment.GetPermission() == 3)    return NotFound("Incorrect date for pay");
            }
            ViewData["CreaditId"] = new SelectList(creditsRepsitory.GetList(), "IdCredits", "Name", loanRepayment.CreaditId);
            ViewData["MonthForPay"] = new SelectList(monthRepository.getMonths(), "IdMonth", "MonthName", loanRepayment.MonthForPay);
          //  ViewData["YearForPay"] = new SelectList(yearsRepository.getYears(), "IdYears", "YearsName", loanRepayment.YearForPay);
            return View(loanRepayment);
        }


        // GET: LoanRepayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanRepayment = await Task.Run(()=>loanRepaymentRepo.GetList().FirstOrDefault(p=>p.IdLoanRepayment==id));
            if (loanRepayment == null)
            {
                return NotFound();
            }
            ViewData["CreaditId"] = new SelectList(creditsRepsitory.GetList(), "IdCredits", "Name", loanRepayment.CreaditId);
            ViewData["MonthForPay"] = new SelectList(monthRepository.getMonths(), "IdMonth", "MonthName", loanRepayment.MonthForPay);
           // ViewData["YearForPay"] = new SelectList(yearsRepository.getYears(), "IdYears", "YearsName", loanRepayment.YearForPay);
            return View(loanRepayment);
        }

        // POST: LoanRepayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLoanRepayment,CreaditId,MonthForPay,YearForPay,DateOfPay,MonthlyPayment,ExpiredDays,Fine,TotalPayment")] LoanRepayment loanRepayment)
        {
            if (id != loanRepayment.IdLoanRepayment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => loanRepaymentRepo.Update(loanRepayment));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanRepaymentExists(loanRepayment.IdLoanRepayment))
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
            ViewData["CreaditId"] = new SelectList(creditsRepsitory.GetList(), "IdCredits", "Name", loanRepayment.CreaditId);
            ViewData["MonthForPay"] = new SelectList(monthRepository.getMonths(), "IdMonth", "MonthName", loanRepayment.MonthForPay);
           // ViewData["YearForPay"] = new SelectList(yearsRepository.getYears(), "IdYears", "YearsName", loanRepayment.YearForPay);
            return View(loanRepayment);
        }

        private bool LoanRepaymentExists(int idLoanRepayment)
        {
            throw new NotImplementedException();
        }

        // GET: LoanRepayments/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanRepayment = await Task.Run(() =>loanRepaymentRepo.GetList().FirstOrDefault(p=>p.IdLoanRepayment==id));
            if (loanRepayment == null)
            {
                return NotFound();
            }

            return View(loanRepayment);
        }

        // POST: LoanRepayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            await Task.Run(() => loanRepaymentRepo.Delete(id));
            return RedirectToAction(nameof(Index));
        }

        private bool LoanRepaymentExists(short id)
        {
            //return _context.LoanRepayment.Any(e => e.IdLoanRepayment == id);
            return loanRepaymentRepo.GetList().Any(p => p.IdLoanRepayment == id);
        }
    }
}
