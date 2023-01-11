using Microsoft.AspNetCore.Mvc;

namespace HDProjectWeb.Models.Helps
{
    [ViewComponent(Name = "ListaUNegocio")]

    //Lista para mostrar los TipoReq en Combo (View Crear)
    public class UNegocioViewComponent : ViewComponent
    {
        private readonly IUNegocioService _unegocioService;
        public UNegocioViewComponent(IUNegocioService unegocioService)
        {
           _unegocioService = unegocioService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _unegocioService.ListaUNegocio();
            return View(data);
        }
    }
}
