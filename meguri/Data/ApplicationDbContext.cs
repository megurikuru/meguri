using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using meguri.Models;

namespace meguri.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
        public DbSet<meguri.Models.Work> Work { get; set; } = default!;
        public DbSet<meguri.Models.UploadFile> UploadFile { get; set; } = default!;
    }
}
