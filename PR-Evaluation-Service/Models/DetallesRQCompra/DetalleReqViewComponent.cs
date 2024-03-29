﻿using Microsoft.AspNetCore.Mvc;

namespace HDProjectWeb.Models.Detalles
{
    [ViewComponent(Name = "DetalleReq")]
    public class DetallePrdViewComponent : ViewComponent
    {
        private readonly IDetalleReqService _detalleReqService;
        public DetallePrdViewComponent(IDetalleReqService detalleReqService)
        {
            _detalleReqService = detalleReqService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string Rco_numero)
        {
            var products = await _detalleReqService.GetDetalleReq(Rco_numero);
            return View(products);
        }
    }
}
