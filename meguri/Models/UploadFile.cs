namespace meguri.Models {
    public class UploadFile { 
        public int Id { get; set; }
        public string? Name { get; set; }
        public byte[]? FileContent { get; set; }
    }
}
