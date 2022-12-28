using Microsoft.AspNetCore.Mvc;

namespace HDProjectWeb.Models.Detalles
{
    [ViewComponent(Name = "Adjuntos")]
    public class AdjuntosViewComponent : ViewComponent
    {
        private readonly IDetalleReqService _detalleReqService;
        
    }
}
