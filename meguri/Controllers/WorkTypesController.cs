using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using meguri.Data;
using meguri.Models;

namespace meguri.Controllers {
    public class WorkTypesController : Controller {
        private readonly ApplicationDbContext _context;

        public WorkTypesController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: WorkTypes
        public async Task<IActionResult> Index() {
            return View(await _context.WorkType.ToListAsync());
        }

        // GET: WorkTypes/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var workType = await _context.WorkType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workType == null) {
                return NotFound();
            }

            return View(workType);
        }

        // GET: WorkTypes/Create
        public IActionResult Create() {
            return View();
        }

        // POST: WorkTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] WorkType workType) {
            if (ModelState.IsValid) {
                _context.Add(workType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workType);
        }

        // GET: WorkTypes/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var workType = await _context.WorkType.FindAsync(id);
            if (workType == null) {
                return NotFound();
            }
            return View(workType);
        }

        // POST: WorkTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] WorkType workType) {
            if (id != workType.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(workType);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!WorkTypeExists(workType.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(workType);
        }

        // GET: WorkTypes/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var workType = await _context.WorkType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workType == null) {
                return NotFound();
            }

            return View(workType);
        }

        // POST: WorkTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var workType = await _context.WorkType.FindAsync(id);
            if (workType != null) {
                _context.WorkType.Remove(workType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkTypeExists(int id) {
            return _context.WorkType.Any(e => e.Id == id);
        }
    }
}
