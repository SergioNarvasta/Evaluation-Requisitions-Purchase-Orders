namespace ProjectWeb_DRA.Models
{
    public class FileClass
    {
        public int FileId { get; set; } = 0;
        public string Name { get; set; } = "";
        public string Path { get; set; } = "";
        public List<FileClass> Files { get; set; }= new List<FileClass>();  

    }
}
