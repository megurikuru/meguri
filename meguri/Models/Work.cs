using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace meguri.Models {

    [Index(nameof(ParentId))]
    [Index(nameof(Created))]
    [Index(nameof(Updated))]
    public class Work {
        public int Id { get; set; }
        public int ParentId { get; set; } = 1;
        public int WorkTypeId { get; set; } = 1;
        public string? Name { get; set; }
        public string? Text { get; set; }
        public bool Sexual  { get; set; } = false;
        public bool Violence  { get; set; } = false;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set;}

        public ICollection<User>? Users { get; set; }
        public ICollection<FilePath>? FielPaths { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Tag>? Tags { get; set; }
    }

}
