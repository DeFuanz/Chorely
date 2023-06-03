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
using Microsoft.AspNetCore.Identity;

namespace Chorely.Controllers
{
    [Authorize(Roles = "Administrator, Assignee")]
    public class ChoreController : Controller
    {
        private readonly ChorleyContext _context;
        private readonly UserManager<ChorelyUser> _userManager;

        public ChoreController(ChorleyContext context, UserManager<ChorelyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Chore
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var chores = _context.Chore.Where(i => i.CreatedById == userId).ToList();

            decimal balance = chores.Where(c => c.Completed).Sum(c => c.Value);

            ViewBag.Balance = balance;

            return View(chores);
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create()
        {
            var userId = _userManager.GetUserId(User);
            var assignees = await  _userManager.Users.Where(i => i.AssignedAdministratorId == userId).ToListAsync();

            List<SelectListItem> assigneeSelectList = assignees.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.UserName,
                    Value = a.Id,
                    Selected = false
                };
            });

            ViewBag.AssigneeList = assigneeSelectList;

            return View();
        }

        // POST: Chore/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,AssignedDate,Completed,Value,Notes,AssignedToId")] Chore chore)
        {
            if (ModelState.IsValid)
            {
                chore.CreatedById = _userManager.GetUserId(User);
                _context.Add(chore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chore);
        }

        // GET: Chore/Edit/5
        [Authorize(Roles = "Administrator")]
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

            var userId = _userManager.GetUserId(User);
            var assignees = await  _userManager.Users.Where(i => i.AssignedAdministratorId == userId).ToListAsync();

            List<SelectListItem> assigneeSelectList = assignees.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.UserName,
                    Value = a.Id,
                    Selected = false
                };
            });

            ViewBag.AssigneeList = assigneeSelectList;

            return View(chore);
        }

        // POST: Chore/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,AssignedDate,Completed,Value,Notes,AssignedToId")] Chore chore)
        {
            if (id != chore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    chore.CreatedById = _userManager.GetUserId(User);
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
        [Authorize(Roles = "Administrator")]
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
