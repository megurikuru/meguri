using System.ComponentModel.DataAnnotations;

namespace meguri.Models {

    public class Tag {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Discripution { get; set; }

        public ICollection<Work>? Works { get; set; }
    }

}
