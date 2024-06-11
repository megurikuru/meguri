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
using Microsoft.AspNetCore.Http;

namespace meguri.Controllers {
    public class UploadFilesController : Controller {
        private readonly ApplicationDbContext _context;

        public UploadFilesController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: UploadFiles
        public async Task<IActionResult> Index() {
            return View(await _context.UploadFile.ToListAsync());
        }

        // GET: UploadFiles/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var uploadFile = await _context.UploadFile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uploadFile == null) {
                return NotFound();
            }

            return View(uploadFile);
        }

        // GET: UploadFiles/Create
        public IActionResult Create() {
            return View();
        }

        // POST: UploadFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, string name, IFormFile fileContent) {
            UploadFile uploadFile = new UploadFile();
            if (ModelState.IsValid) {

                int myId = id;
                string myName = name;
                byte[] myFileContent;

                using (var memoryStream = new MemoryStream()) {
                    fileContent.CopyTo(memoryStream);
                    myFileContent = memoryStream.ToArray();
                }

                uploadFile.Id = myId;
                uploadFile.Name = myName;
                uploadFile.FileContent = myFileContent;

                _context.Add(uploadFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uploadFile);
        }

        // GET: UploadFiles/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var uploadFile = await _context.UploadFile.FindAsync(id);
            if (uploadFile == null) {
                return NotFound();
            }
            return View(uploadFile);
        }

        // POST: UploadFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int Id, string name, IFormFile fileContent) {
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FileContent")] UploadFile uploadFile) {

            UploadFile uploadFile = new UploadFile();
            if (id != Id) {
                return NotFound();
            }

            int myId = id;
            string myName = name;
            byte[] myFileContent;

            using (var memoryStream = new MemoryStream()) {
                fileContent.CopyTo(memoryStream);
                myFileContent = memoryStream.ToArray();
            }

            uploadFile.Id = myId;
            uploadFile.Name = myName;
            uploadFile.FileContent = myFileContent;

            if (ModelState.IsValid) {
                try {
                    _context.Update(uploadFile);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!UploadFileExists(uploadFile.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(uploadFile);
        }

        // GET: UploadFiles/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var uploadFile = await _context.UploadFile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uploadFile == null) {
                return NotFound();
            }

            return View(uploadFile);
        }

        // POST: UploadFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var uploadFile = await _context.UploadFile.FindAsync(id);
            if (uploadFile != null) {
                _context.UploadFile.Remove(uploadFile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UploadFileExists(int id) {
            return _context.UploadFile.Any(e => e.Id == id);
        }
    }
}
