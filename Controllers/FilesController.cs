using Microsoft.AspNetCore.Mvc;

namespace ProjectWeb_DRA.Controllers
{
    public class FilesController : Controller
    {
        public IWebHostEnvironment _webHostEnvironment;

        public FilesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
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
