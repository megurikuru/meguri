using System.ComponentModel.DataAnnotations;

namespace meguri.Models {

    public class WorkType {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Work>? Works { get; set; }
    }

}
