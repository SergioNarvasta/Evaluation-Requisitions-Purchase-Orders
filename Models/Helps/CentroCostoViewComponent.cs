using Microsoft.AspNetCore.Mvc;

namespace HDProjectWeb.Models.Helps
{
    [ViewComponent(Name = "AyudaCentroCosto")]
    public class CentroCostoViewComponent : ViewComponent
    {
        private readonly ICentroCostoService _centroCostoService;
        public CentroCostoViewComponent(ICentroCostoService centroCostoService)
        {
            _centroCostoService = centroCostoService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _centroCostoService.ListaAyudaCentroCosto();
            return View(data);
        }
    }
}
