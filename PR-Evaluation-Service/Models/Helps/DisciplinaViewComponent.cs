using Microsoft.AspNetCore.Mvc;

namespace HDProjectWeb.Models.Helps
{
    [ViewComponent(Name = "AyudaDisciplina")]
    public class DisciplinaViewComponent : ViewComponent
    {
        private readonly IDisciplinaService _disciplinaService;
        public DisciplinaViewComponent(IDisciplinaService disciplinaService)
        {
          _disciplinaService= disciplinaService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Data = await _disciplinaService.ListaAyudaDisciplina();
            return View(Data);
        }
    }
}
