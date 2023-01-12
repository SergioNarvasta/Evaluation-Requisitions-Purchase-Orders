using Microsoft.AspNetCore.Mvc;
using ProjectWeb_DRA.Models;
using System.IO;

namespace ProjectWeb_DRA.Controllers
{
    public class Files1Controller : Controller
    {
        public IWebHostEnvironment _webHostEnvironment;

        public Files1Controller(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task <IActionResult> Index(string filename="") {
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
            return View("_Files",fileObj);
        }
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            string filename = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(filename))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
                return View("_Files");
        }
        public IActionResult PDFViewerNewTab(string filename)
        {
            string path = _webHostEnvironment.WebRootPath + "\\files\\" + filename;
            return File(System.IO.File.ReadAllBytes(path), "application/pdf");
        }
        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            Byte[] byteArray = System.IO.File.ReadAllBytes(file.Name);
            string FileString64 = Convert.ToBase64String(byteArray);


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
