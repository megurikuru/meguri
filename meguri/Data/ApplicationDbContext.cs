using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Meguri.Models;

namespace Meguri.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        public DbSet<Meguri.Models.Work> Work { get; set; } = default!;
        public DbSet<Meguri.Models.User> User{ get; set; } = default!;
        public DbSet<Meguri.Models.Category> Category{ get; set; } = default!;
        public DbSet<Meguri.Models.UploadFile> UploadFile { get; set; } = default!;
    }
}
