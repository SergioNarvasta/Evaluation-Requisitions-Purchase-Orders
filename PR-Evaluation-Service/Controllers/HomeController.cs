using HDProjectWeb.Models;
using HDProjectWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HDProjectWeb.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServicioEstandar servicioEstandar;

        public HomeController(ILogger<HomeController> logger, IServicioEstandar servicioEstandar)
        {
            _logger = logger;
            this.servicioEstandar = servicioEstandar;
        }
        public IActionResult Index()
        {
            ViewBag.periodo = servicioEstandar.ObtenerPeriodo();
            string cia = servicioEstandar.Compañia();
            string nomcia = servicioEstandar.ObtenerCompañia(cia);
            
            ViewBag.nomcia = nomcia ;


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult NoEncontrado()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}