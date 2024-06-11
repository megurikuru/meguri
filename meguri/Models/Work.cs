using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace meguri.Models {

    [Index(nameof(ParentId))]
    [Index(nameof(Tag1))]
    [Index(nameof(Tag2))]
    [Index(nameof(Tag3))]
    [Index(nameof(Created))]
    [Index(nameof(Updated))]
    public class Work {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Text { get; set; }
        public int ParentId { get; set; }
        public string? WorkType { get; set; }
        public string? Category { get; set; }
        public string? Tag1 { get; set; }
        public string? Tag2 { get; set; }
        public string? Tag3 { get; set; }
        public byte[]? FileContent { get; set; }
        public bool Sexual  { get; set; } = false;
        public bool Violence  { get; set; } = false;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set;}

        public virtual User? User { get; set; }
    }

}
