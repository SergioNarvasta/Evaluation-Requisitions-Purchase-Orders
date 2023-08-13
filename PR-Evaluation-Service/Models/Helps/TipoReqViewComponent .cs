using Microsoft.AspNetCore.Mvc;

namespace HDProjectWeb.Models.Helps
{
    [ViewComponent(Name = "ListaTipoReq")]

    //Lista para mostrar los TipoReq en Combo (View Crear)
    public class TipoReqViewComponent : ViewComponent
    {
        private readonly ITipoReqService _tipoReqService;
        public TipoReqViewComponent(ITipoReqService tipoReqService)
        {
             _tipoReqService= tipoReqService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _tipoReqService.ListaTipoReq();
            return View(data);
        }
    }
}
