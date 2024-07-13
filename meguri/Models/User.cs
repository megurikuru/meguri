using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Meguri.Models {
    [Index(nameof(Birthday))]
    public class User: IdentityUser{
        public DateOnly Birthday { get; set; }
        public bool AllowSexual  { get; set; } = false;
        public bool AllowViolence  { get; set; } = false;

        public ICollection<Work>? Works { get; set; }
    }
}
