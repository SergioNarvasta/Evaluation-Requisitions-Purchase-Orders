using HDProjectWeb.Models;
using HDProjectWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectWeb_DRA.Models;
using ProjectWeb_DRA.Services;

namespace ProjectWeb_DRA.Controllers
{
    public class OrdenCompraController : Controller
    {
        private readonly IServicioEstandar servicioEstandar;
        private     readonly IRepositorioOrdenCompra repositorioOrdenCompra;

        public OrdenCompraController(IServicioEstandar servicioEstandar, IRepositorioOrdenCompra repositorioOrdenCompra) 
        {
            this.servicioEstandar = servicioEstandar;
            this.repositorioOrdenCompra = repositorioOrdenCompra;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(PaginacionViewModel paginacionViewModel)
        {
            string periodo = await servicioEstandar.ObtenerPeriodo();
            ViewBag.periodo = periodo.Remove(4, 2) + "-" + periodo.Remove(0, 4);
            var OCompra = await repositorioOrdenCompra.Obtener(periodo,paginacionViewModel);
            var totalRegistros = await repositorioOrdenCompra.ContarRegistrosOCC(periodo);
            if (totalRegistros == 0)
            {
                ViewBag.registros = "0";
            }
            var respuesta = new PaginacionRespuesta<OrdenCompra>
            {
                Elementos = OCompra,
                Pagina = paginacionViewModel.Pagina,
                RecordsporPagina = paginacionViewModel.RecordsPorPagina,
                CantidadRegistros = totalRegistros,
                BaseURL = Url.Action()
            };
            return View(respuesta);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string periodo)
        {
            if (periodo is not null)
            {
                await servicioEstandar.ActualizaPeriodo(periodo);
            }
            periodo = await servicioEstandar.ObtenerPeriodo();
            ViewBag.periodo = periodo.Remove(4, 2) + "-" + periodo.Remove(0, 4);
            PaginacionViewModel paginacionViewModel = new();
            var OCompra = await repositorioOrdenCompra.Obtener(periodo, paginacionViewModel);
            var totalRegistros = await repositorioOrdenCompra.ContarRegistrosOCC(periodo);
            if (totalRegistros == 0)
            {
                ViewBag.registros = "0";
            }
            var respuesta = new PaginacionRespuesta<OrdenCompra>
            {
                Elementos = OCompra,
                Pagina = paginacionViewModel.Pagina,
                RecordsporPagina = paginacionViewModel.RecordsPorPagina,
                CantidadRegistros = totalRegistros,
                BaseURL = Url.Action()
            };
            return View(respuesta);
        }
    }
}
