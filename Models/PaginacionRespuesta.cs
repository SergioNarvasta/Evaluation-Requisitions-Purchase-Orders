namespace HDProjectWeb.Models
{
    public class PaginacionRespuesta
    {
        public int Pagina { get; set; } = 1;
        public int RecordsporPagina { get; set; } = 5;
        public int CantidadRegistros { get; set; } = 0;
        //  266 / 5
        public int CantidadPaginas => (int)Math.Ceiling((double)CantidadRegistros / RecordsporPagina);
        public string BaseURL { get; set; }
    }

    public class PaginacionRespuesta<T>:PaginacionRespuesta
    {
        public IEnumerable<T> Elementos { get;set; }
    }
}
