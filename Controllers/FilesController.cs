using Microsoft.AspNetCore.Mvc;
using ProjectWeb_DRA.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ProjectWeb_DRA.Controllers
{
    public class FilesController : Controller
    {
        public IWebHostEnvironment _webHostEnvironment;

        public FilesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index(string filename="") {
            FileClass fileObj = new FileClass();
            fileObj.Name= filename;
            string path = $"{_webHostEnvironment.WebRootPath}\\files\\";
            int nId = 1;
            foreach (string pdfPath in Directory.EnumerateFiles(path, "*.pdf"))
            {
                fileObj.Files.Add(new FileClass()
                {
                    FileId= nId++,
                    Name= Path.GetFileName(pdfPath) ,
                    Path= pdfPath
                });  
            }
            return View(fileObj);
        }
        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            string filename = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(filename))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
                return View();
        }
        public IActionResult Upload(IFormFile file)
        {
            try 
            {
                if (file.Length == 0)
                    return BadRequest();
                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Files");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fullpath = Path.Combine(path, file.FileName);
                using (var stream = new FileStream(fullpath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok(file);
                
            }catch(Exception) 
            {
                return BadRequest();
            }
            

        }
    }
}
