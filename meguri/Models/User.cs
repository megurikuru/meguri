using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace meguri.Models {

    [Index(nameof(Birthday))]
    public class User: IdentityUser{
        public DateOnly Birthday { get; set; }
        public bool Sexual  { get; set; } = false;
        public bool Violence  { get; set; } = false;

        public ICollection<Work>? Works { get; set; }
    }

}
