using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Meguri.Data;
using Meguri.Models;
using MimeKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Meguri.Authorization;


namespace Meguri.Controllers {
    public class WorksController : Controller {
        private readonly ApplicationDbContext _context;

        // 認可
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<IdentityUser> UserManager { get; }

        public WorksController(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
        ) {
            _context = context;
            UserManager = userManager;
            AuthorizationService = authorizationService;
        }

        // GET: Works
        public async Task<IActionResult> Index() {
            IList<Work> works = await _context.Work
                .Include(w => w.Category)
                .AsNoTracking()
                .ToListAsync();

            // return View(await _context.Work.ToListAsync());
            return View(works);
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
            int id, string name, string? text, int parentId, string workType,
            int categoryId, string? tag1, string? tag2, string? tag3,
            IFormFile? fileContent, bool sexual, bool violence,
            DateTime created, DateTime updated
        ) {
            Work work = new Work();
            if (ModelState.IsValid) {
                work.Id = id;
                work.Name = name;
                work.Text = text;
                work.ParentId = parentId;
                work.WorkType = workType;
                work.CategoryId = categoryId;
                work.Tag1 = tag1;
                work.Tag2 = tag2;
                work.Tag3 = tag3;
                work.FileContent = null;
                if (fileContent != null) {
                    byte[] byteArray;
                    using (var memoryStream = new MemoryStream()) {
                        fileContent.CopyTo(memoryStream);
                        byteArray = memoryStream.ToArray();
                    }
                    work.FileContent = byteArray;
                }
                work.Sexual = sexual;
                work.Created = DateTime.Now;
                work.Updated = DateTime.Now;

                var userId = UserManager.GetUserId(User);
                var userName = UserManager.GetUserName(User);
                work.UserId = userId;
                work.UserName = userName;

                // 認可
                var isAuthorized = await AuthorizationService.AuthorizeAsync(
                    User, work, WorkOperations.Create
                );
                if (!isAuthorized.Succeeded) {
                    return Forbid();
                }

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

            // 認可
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, work, WorkOperations.Update
            );
            if (!isAuthorized.Succeeded) {
                return Forbid();
            }

            return View(work);
        }

        // POST: Works/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, string name, string? text, int parentId, string workType,
            int categoryId, string? tag1, string? tag2, string? tag3,
            IFormFile? fileContent, bool sexual, bool violence, 
            DateTime created, DateTime updated,
            string userId
        ) {
            Work? work = _context.Work.Find(id);
            if (work == null) {
                return NotFound();
            }

            // 認可
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, work, WorkOperations.Update
            );
            if (!isAuthorized.Succeeded) {
                return Forbid();
            }

            work.Id = id;
            work.Name = name;
            work.Text = text;
            work.ParentId = parentId;
            work.WorkType = workType;
            work.CategoryId = categoryId;
            work.Tag1 = tag1;
            work.Tag2 = tag2;
            work.Tag3 = tag3;
            if (fileContent != null) {
                byte[] UpdateFileContent;
                using (var memoryStream = new MemoryStream()) {
                    fileContent.CopyTo(memoryStream);
                    UpdateFileContent = memoryStream.ToArray();
                }
                work.FileContent = UpdateFileContent;
            }
            work.Sexual = sexual;
            work.Created = created;
            work.Updated = DateTime.Now;
            work.UserId = userId;

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

            // 認可
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, work, WorkOperations.Delete
            );
            if (!isAuthorized.Succeeded) {
                return Forbid();
            }

            return View(work);
        }

        // POST: Works/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var work = await _context.Work.FindAsync(id);

            // 認可
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, work, WorkOperations.Delete
            );
            if (!isAuthorized.Succeeded) {
                return Forbid();
            }

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
