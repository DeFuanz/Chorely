using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Chorely.Models;
using Chorley.Data;
using Microsoft.AspNetCore.Authorization;

namespace Chorely.Controllers
{
    [Authorize(Roles = "IdentityUser, Worker")]
    public class ChoreController : Controller
    {
        private readonly ChorleyContext _context;

        public ChoreController(ChorleyContext context)
        {
            _context = context;
        }

        // GET: Chore
        public async Task<IActionResult> Index()
        {
              return _context.Chore != null ? 
                          View(await _context.Chore.ToListAsync()) :
                          Problem("Entity set 'ChorleyContext.Chore'  is null.");
        }

        // GET: Chore/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Chore == null)
            {
                return NotFound();
            }

            var chore = await _context.Chore
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chore == null)
            {
                return NotFound();
            }

            return View(chore);
        }

        // GET: Chore/Create
        [Authorize(Roles = "IdentityUser")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chore/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,AssignedDate,Completed,Value,Notes")] Chore chore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chore);
        }

        // GET: Chore/Edit/5
        [Authorize(Roles = "IdentityUser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Chore == null)
            {
                return NotFound();
            }

            var chore = await _context.Chore.FindAsync(id);
            if (chore == null)
            {
                return NotFound();
            }
            return View(chore);
        }

        // POST: Chore/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,AssignedDate,Completed,Value,Notes")] Chore chore)
        {
            if (id != chore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChoreExists(chore.Id))
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
            return View(chore);
        }

        // GET: Chore/Delete/5
        [Authorize(Roles = "IdentityUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Chore == null)
            {
                return NotFound();
            }

            var chore = await _context.Chore
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chore == null)
            {
                return NotFound();
            }

            return View(chore);
        }

        // POST: Chore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Chore == null)
            {
                return Problem("Entity set 'ChorleyContext.Chore'  is null.");
            }
            var chore = await _context.Chore.FindAsync(id);
            if (chore != null)
            {
                _context.Chore.Remove(chore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChoreExists(int id)
        {
          return (_context.Chore?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
