using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace meguri.Models {

    [Index(nameof(ParentId))]
    public class Category {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ParentId { get; set; } = 1;
        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Work>? Works { get; set; }
    }

}
