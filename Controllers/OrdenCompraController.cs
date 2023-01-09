
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
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Evaluacion(int epk)
        {
            var OCompra = await repositorioOrdenCompra.ObtenerporEpk(epk);
            if (OCompra is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            //var OCompra = await repositorioOrdenCompra.ObtenerporCodigoOCC(Occ_numero);
            string cia, suc;
            cia = servicioEstandar.Compañia();
            suc = servicioEstandar.Sucursal();
            ViewBag.cia = cia;
            ViewBag.suc = suc;
            ViewBag.usu = servicioUsuario.ObtenerCodUsuario();
            ViewBag.epk = OCompra.Occ_codepk;
            ViewBag.num = OCompra.Occ_numero;
            ViewBag.url = Url.Action();
            //ViewBag.sit = OCompra.
            return View(OCompra);
        }
        [HttpPost]
        public async Task<IActionResult> Aprobar(string cia,string suc,string epk,string usu,string num)
        {
            int cia_codcia, suc_codsuc, occ_codepk, uap_codepk;
            cia_codcia = int.Parse(cia);
            suc_codsuc = int.Parse(suc);
            occ_codepk = int.Parse(epk);
            uap_codepk = await servicioUsuario.ObtenerEpkUsuario(usu);
            var result = await repositorioOrdenCompra.Aprobar(cia_codcia, suc_codsuc, occ_codepk, uap_codepk);
            string message = "se aprobo con exito la Orden de Compra";
            if (result == 0)
            {
                message = "ocurrio un error al intentar aprobar la Orden de Compra";
            } 
            ViewBag.message = message;
            ViewBag.usu = usu;
            ViewBag.num = num;
            ViewBag.result = result;
            return View("ResultAprob");
        }
        [HttpPost]
        public async Task<IActionResult> Rechazar(string cia, string suc, string epk, string usu, string num)
        {
            int cia_codcia, suc_codsuc, occ_codepk, uap_codepk;
            cia_codcia = int.Parse(cia);
            suc_codsuc = int.Parse(suc);
            occ_codepk = int.Parse(epk);
            uap_codepk = await servicioUsuario.ObtenerEpkUsuario(usu);
            var result = await repositorioOrdenCompra.Rechaza(cia_codcia, suc_codsuc, occ_codepk, uap_codepk);
            string message = "Se rechazo con exito la Orden de Compra";
            if (result == 0)
            {
                message = "ocurrio un error al intentar rechazar la Orden de Compra";
            }
            ViewBag.message = message;
            ViewBag.usu = usu;
            ViewBag.num = num;
            return View("ResultAprob");
        }
        [HttpPost]
        public async Task<IActionResult> Devolver(string cia, string suc, string epk, string usu, string num)
        {
            int cia_codcia, suc_codsuc, occ_codepk, uap_codepk;
            cia_codcia = int.Parse(cia);
            suc_codsuc = int.Parse(suc);
            occ_codepk = int.Parse(epk);
            uap_codepk = await servicioUsuario.ObtenerEpkUsuario(usu);
            var result = await repositorioOrdenCompra.Devuelve(cia_codcia, suc_codsuc, occ_codepk, uap_codepk);
            string message = "Se devolvio con exito la Orden de Compra";
            if (result == 0)
            {
                message = "ocurrio un error al intentar devolver la Orden de Compra";
            }
            ViewBag.message = message;
            ViewBag.usu = usu;
            ViewBag.num = num;
            return View("ResultAprob");
        }

        public static string Encriptar(string _cadenaAencriptar)
        {
            string result;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

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
