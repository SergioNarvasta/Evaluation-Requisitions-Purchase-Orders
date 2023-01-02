using Microsoft.AspNetCore.Mvc;

namespace ProjectWeb_DRA.Models.DetallesOCompra
{
    [ViewComponent(Name = "DetalleCcoPrd")]
    public class DetallePrdViewComponent : ViewComponent
    {
        private readonly IDetallePrdService _detallePrdService;
        public DetallePrdViewComponent(IDetallePrdService detalleReqService)
        {
            _detallePrdService = detalleReqService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int Occ_codepk)
        {
            var products = await _detallePrdService.GetDetallePrd(Occ_codepk);
            return View(products);
        }
    }
}
