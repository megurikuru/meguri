using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Meguri.Models {
    [Index(nameof(Birthday))]
    public class User: IdentityUser{
        [Required(ErrorMessage = "RequiredError")]
        [EmailAddress(ErrorMessage = "InvalidEmailError")]
        [Display(Name = "Email")]
        public override string Email { get; set; }

        public DateOnly Birthday { get; set; }
        public bool AllowSexual  { get; set; } = false;
        public bool AllowViolence  { get; set; } = false;

        public ICollection<Work> Works { get; set; }
    }
}
