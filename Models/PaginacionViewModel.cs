namespace HDProjectWeb.Models
{
    public class PaginacionViewModel
    {
        public int Pagina { get; set; } = 1;
        private int recordsPorPagina = 6;
        private readonly int cantidadMaxima = 80;

        public int RecordsPorPagina 
        {
            get
            {
                return recordsPorPagina;
            }
            set
            {
               recordsPorPagina = (value>cantidadMaxima)? cantidadMaxima:value;
            }
        }
        public int RecordsASaltar => recordsPorPagina*(Pagina - 1);
    }
}
