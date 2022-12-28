using Microsoft.AspNetCore.Mvc;

namespace HDProjectWeb.Models.Helps
{
    [ViewComponent(Name = "AyudaUsuario")]
    public class UsuarioViewComponent : ViewComponent
    {
        private readonly IUsuarioService _usuarioService; 
        public UsuarioViewComponent(IUsuarioService usuarioService)
        {
          _usuarioService= usuarioService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Data = await _usuarioService.ListaAyudaUsuario();
            return View(Data);
        }
    }
}
