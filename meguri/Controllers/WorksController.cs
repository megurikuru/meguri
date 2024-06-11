using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using meguri.Data;
using meguri.Models;
using MimeKit;
using Microsoft.AspNetCore.Identity;

namespace meguri.Controllers {
    public class WorksController : Controller {
        private readonly ApplicationDbContext _context;

        public WorksController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: Works
        public async Task<IActionResult> Index() {
            return View(await _context.Work.ToListAsync());
        }

        // GET: Works/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var work = await _context.Work
                .FirstOrDefaultAsync(m => m.Id == id);
            if (work == null) {
                return NotFound();
            }

            return View(work);
        }

        // GET: Works/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Works/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            int Id,string Name,string? Text,int ParentId,string WorkType,
            string? Category,string? Tag1,string? Tag2,string? Tag3,
            IFormFile? FileContent,bool Sexual,bool Violence,DateTime Created,DateTime Updated
        ) {
            Work work = new Work();
            if (ModelState.IsValid) {
                work.Id = Id;
                work.Name = Name;
                work.Text = Text;
                work.ParentId = ParentId;
                work.WorkType = WorkType;
                work.Category = Category;
                work.Tag1 = Tag1;
                work.Tag2 = Tag2;
                work.Tag3 = Tag3;
                work.FileContent = null;
                if (FileContent != null) {
                    byte[] fileContent;
                    using (var memoryStream = new MemoryStream()) {
                        FileContent.CopyTo(memoryStream);
                        fileContent = memoryStream.ToArray();
                    }
                    work.FileContent = fileContent;
                }
                work.Sexual = Sexual;
                work.Created = DateTime.Now;
                work.Updated = DateTime.Now;    
                
                _context.Add(work);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(work);
        }

        // GET: Works/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var work = await _context.Work.FindAsync(id);
            if (work == null) {
                return NotFound();
            }
            return View(work);
        }

        // POST: Works/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Text,ParentId,WorkType,Category,Tag1,Tag2,Tag3,FileContent,Sexual,Violence,Created,Updated")] Work work) {
            if (id != work.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(work);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!WorkExists(work.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(work);
        }

        // GET: Works/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var work = await _context.Work
                .FirstOrDefaultAsync(m => m.Id == id);
            if (work == null) {
                return NotFound();
            }

            return View(work);
        }

        // POST: Works/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var work = await _context.Work.FindAsync(id);
            if (work != null) {
                _context.Work.Remove(work);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkExists(int id) {
            return _context.Work.Any(e => e.Id == id);
        }
    }
}
