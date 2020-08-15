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
    public class PositionsController : Controller
    {
        private readonly ICrudable<Position> positionrepo;

        public PositionsController()
        {
            positionrepo = new Positionrepository();
        }

        // GET: Positions
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => positionrepo.GetList()));
        }

        // GET: Positions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPosition,Position1")] Position position)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => positionrepo.Create(position));
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        // GET: Positions/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await Task.Run(() => positionrepo.GetList().FirstOrDefault(p => p.IdPosition == id));
            if (position == null)
            {
                return NotFound();
            }
            return View(position);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("IdPosition,Position1")] Position position)
        {
            if (id != position.IdPosition)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => positionrepo.Update(position));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionExists(position.IdPosition))
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
            return View(position);
        }

        // GET: Positions/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await Task.Run(() => positionrepo.GetList().FirstOrDefault(p => p.IdPosition == id));
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            await Task.Run(() => positionrepo.Delete(id));
            return RedirectToAction(nameof(Index));
        }

        private bool PositionExists(byte id)
        {
            return positionrepo.GetList().Any(p => p.IdPosition == id);
        }
    }
}
