using System.ComponentModel.DataAnnotations;

namespace HDProjectWeb.Validaciones
{
    public class ValidacionPeriodo
    {
        [Required]
        [MaxLength(2, ErrorMessage = "Seleccione Mes")]
        public string Mes { get; set; }

        [Required]
        [MaxLength(2, ErrorMessage = "Seleccione Año")]
        public string Ano { get; set; }
    }
}
