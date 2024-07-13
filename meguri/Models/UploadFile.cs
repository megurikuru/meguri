using System.ComponentModel.DataAnnotations.Schema;

namespace Meguri.Models {
    [Table("UploadFiles")]
    public class UploadFile { 
        public int Id { get; set; }
        public string? Name { get; set; }
        public byte[]? FileContent { get; set; }
    }
}
