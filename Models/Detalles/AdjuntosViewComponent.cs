using Microsoft.AspNetCore.Mvc;

namespace HDProjectWeb.Models.Detalles
{
    [ViewComponent(Name = "DetalleAdjuntos")]
    public class AdjuntosViewComponent : ViewComponent
    {
        private readonly IAdjuntosService _adjuntosService;
        public AdjuntosViewComponent(IAdjuntosService adjuntosService)
        {
            _adjuntosService= adjuntosService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string Rco_numero)
        {
            var products = await _adjuntosService.GetAdjuntos(Rco_numero);
            return View(products);
        }
    }
}
