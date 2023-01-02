using HDProjectWeb.Models;
using HDProjectWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectWeb_DRA.Models;
using ProjectWeb_DRA.Services;
using System.Security.Cryptography;
using System.Text;

namespace ProjectWeb_DRA.Controllers
{
    public class OrdenCompraController : Controller
    {
        private readonly IServicioEstandar servicioEstandar;
        private readonly IServicioUsuario servicioUsuario;
        private readonly IRepositorioOrdenCompra repositorioOrdenCompra;

        public OrdenCompraController(IServicioEstandar servicioEstandar, IRepositorioOrdenCompra repositorioOrdenCompra, IServicioUsuario servicioUsuario)
        {
            this.servicioEstandar       = servicioEstandar;
            this.repositorioOrdenCompra = repositorioOrdenCompra;
            this.servicioUsuario        = servicioUsuario;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(PaginacionViewModel paginacionViewModel)
        {
            
            string periodo = await servicioEstandar.ObtenerPeriodo();
            ViewBag.periodo = periodo.Remove(4, 2) + "-" + periodo.Remove(0, 4);
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
                BaseURL = Url.Action(),      
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
        [HttpGet]
        public async Task<IActionResult> AprobacionOC(string Occ_numero)
        {

            var OCompra = await repositorioOrdenCompra.ObtenerporCodigoOCC(Occ_numero);
            string cia, suc;
            cia = servicioEstandar.Compañia();
            suc = servicioEstandar.Sucursal();
            ViewBag.cia= cia;
            ViewBag.suc= suc;
            ViewBag.usu = servicioUsuario.ObtenerCodUsuario();
            ViewBag.epk = OCompra.Occ_codepk;
            if (OCompra is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            return View(OCompra);
        }
        public static string Encriptar(string _cadenaAencriptar)
        {
            string result;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public static string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

    }
}
