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
    public class FilePathsController : Controller {
        private readonly ApplicationDbContext _context;

        public FilePathsController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: FilePaths
        public async Task<IActionResult> Index() {
            var applicationDbContext = _context.FilePath.Include(f => f.Work);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FilePaths/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var filePath = await _context.FilePath
                .Include(f => f.Work)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filePath == null) {
                return NotFound();
            }

            return View(filePath);
        }

        // GET: FilePaths/Create
        public IActionResult Create() {
            ViewData["WorkId"] = new SelectList(_context.Work, "Id", "Id");
            return View();
        }

        // POST: FilePaths/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WorkId,Path")] FilePath filePath) {
            if (ModelState.IsValid) {
                _context.Add(filePath);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkId"] = new SelectList(_context.Work, "Id", "Id", filePath.WorkId);
            return View(filePath);
        }

        // GET: FilePaths/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var filePath = await _context.FilePath.FindAsync(id);
            if (filePath == null) {
                return NotFound();
            }
            ViewData["WorkId"] = new SelectList(_context.Work, "Id", "Id", filePath.WorkId);
            return View(filePath);
        }

        // POST: FilePaths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WorkId,Path")] FilePath filePath) {
            if (id != filePath.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(filePath);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!FilePathExists(filePath.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkId"] = new SelectList(_context.Work, "Id", "Id", filePath.WorkId);
            return View(filePath);
        }

        // GET: FilePaths/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var filePath = await _context.FilePath
                .Include(f => f.Work)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filePath == null) {
                return NotFound();
            }

            return View(filePath);
        }

        // POST: FilePaths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var filePath = await _context.FilePath.FindAsync(id);
            if (filePath != null) {
                _context.FilePath.Remove(filePath);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilePathExists(int id) {
            return _context.FilePath.Any(e => e.Id == id);
        }
    }
}
