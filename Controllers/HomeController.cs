using HDProjectWeb.Models;
using HDProjectWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HDProjectWeb.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServicioPeriodo servicioPeriodo;

        public HomeController(ILogger<HomeController> logger, IServicioPeriodo servicioPeriodo)
        {
            _logger = logger;
            this.servicioPeriodo = servicioPeriodo;
        }
        public IActionResult Index()
        {
            ViewBag.periodo = servicioPeriodo.ObtenerPeriodo();
            string cia = servicioPeriodo.Compañia();
            ViewBag.compañia = servicioPeriodo.ObtenerCompañia(cia);
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