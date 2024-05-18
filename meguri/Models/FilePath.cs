using System.ComponentModel.DataAnnotations;

namespace meguri.Models {
    public class FilePath {
        [Key]
        public int Id { get; set; }
        public int WorkId { get; set; }
        public string Path { get; set; } = string.Empty;

        public virtual Work? Work { get; set; }
    }
}
