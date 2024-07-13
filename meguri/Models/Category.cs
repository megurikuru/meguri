using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meguri.Models {

    [Table("Categories")]
    public class Category {
        public int Id { get; set; }
        public string? Name{ get; set; }

        public ICollection<Work>? Works { get; set; }
    }

}
