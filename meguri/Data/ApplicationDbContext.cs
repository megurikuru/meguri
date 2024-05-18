using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using meguri.Models;

namespace meguri.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
        public DbSet<meguri.Models.Work> Work { get; set; } = default!;
        public DbSet<meguri.Models.Category> Category { get; set; } = default!;
        public DbSet<meguri.Models.FilePath> FilePath { get; set; } = default!;
        public DbSet<meguri.Models.Tag> Tag { get; set; } = default!;
        public DbSet<meguri.Models.WorkType> WorkType { get; set; } = default!;
    }
}
