using HDProjectWeb.Models.Helps;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ProjectWeb_DRA.Models
{
    public class Files
    {
        public int FileId { get; set; } = 0;
        public string Name { get; set; } = "";
        public string Path { get; set; } = "";
        public List<Files> nFiles { get; set; } = new List<Files>();
    }
    public interface IFilesService
    {
        FileClass Index(string filename = "");
    }
    public class FilesService : IFilesService 
    {
        public IWebHostEnvironment _webHostEnvironment;

        public FilesService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public FileClass Index(string filename = "")
        {
            FileClass fileObj = new FileClass();
            fileObj.Name = filename;
            string path = $"{_webHostEnvironment.WebRootPath}\\files\\";
            int nId = 1;
            foreach (string pdfPath in Directory.EnumerateFiles(path, "*.pdf"))
            {
                fileObj.Files.Add(new FileClass()
                {
                    FileId = nId++,
                    Name = Path.GetFileName(pdfPath),
                    Path = pdfPath
                });
            }
            return fileObj;
        }
    }
}
