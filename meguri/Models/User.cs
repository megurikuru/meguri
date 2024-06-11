using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace meguri.Models {

    [Index(nameof(Birthday))]
    public class User: IdentityUser{
        public DateOnly Birthday { get; set; }
        public bool AllowSexual  { get; set; } = false;
        public bool AllowViolence  { get; set; } = false;

        public virtual ICollection<Work>? Works { get; set; }
    }

}
