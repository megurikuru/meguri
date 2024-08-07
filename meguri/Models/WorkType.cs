using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meguri.Models {

    [Table("WorkTypes")]
    public class WorkType {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Work>? Works { get; set; }
    }

}
