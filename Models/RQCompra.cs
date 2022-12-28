using HDProjectWeb.Models.Detalles;
using System.ComponentModel.DataAnnotations;

namespace HDProjectWeb.Models
{
    public class RQCompra
    {
        //Model for register
        [Required(ErrorMessage = "Campo obligatorio")]// Requerido
        [MaxLength(2, ErrorMessage = "El campo no debe de tener mas de 2 caracteres")]
        public string Cia_codcia { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]// Requerido
        [MaxLength(2, ErrorMessage = "El campo no debe de tener mas de 2 caracteres")]
        public string Suc_codsuc { get; set; }

        //[Required(ErrorMessage = "Campo obligatorio")]// Requerido
        [MaxLength(10, ErrorMessage = "El campo no debe de tener mas de 10 caracteres")]
        public string Rco_numrco { get; set; }         /***View****/
        public DateTime Rco_fec_registro { get; set; }  /***View****/

        [Required(ErrorMessage = "Campo obligatorio")]// Requerido
        [MaxLength(2, ErrorMessage = "El campo no debe de tener mas de 2 caracteres")]
        public string Tin_codtin { get; set; }       /***Vacio****/

        //[Required(ErrorMessage = "Campo obligatorio")]// Requerido
        [MaxLength(6, ErrorMessage = "El campo no debe de tener mas de 6 caracteres")]
        public string Cco_codcco { get; set; }    /***View****/

        [Required(ErrorMessage = "Campo obligatorio")]// Requerido
        [MaxLength(200, ErrorMessage = "El campo no debe de tener mas de 200 caracteres")]
        [Display(Name = "Motivo")]
        public string Rco_motivo { get; set; }    /***View****/

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(2, ErrorMessage = "El campo no debe de tener mas de 2 caracteres")]
        public string Adi_codadi { get; set; } = " ";

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracter")]
        public string Rco_sitrco { get; set; }    /***View****/

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracter")]
        public string Rco_sitlog { get; set; } = " ";

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(4, ErrorMessage = "El campo no debe de tener mas de 4 caracteres")]
        public string Ano_codano { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(2, ErrorMessage = "El campo no debe de tener mas de 2 caracteres")]
        public string Mes_codmes { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracter")]
        public string Rco_indest { get; set; }      /***View****/

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(30, ErrorMessage = "El campo no debe de tener mas de 30 caracteres")]
        public string Rco_usucre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] 
        [MaxLength(30, ErrorMessage = "El campo no debe de tener mas de 30 caracteres")]
        [Display( Name = "Solicitante")]
        public string S10_usuario { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(30, ErrorMessage = "El campo no debe de tener mas de 30 caracteres")]
        public string Rco_codusu { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(2, ErrorMessage = "El campo no debe de tener mas de 2 caracteres")]
        public string Ung_codung { get; set; }      /***View****/

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        public short Rco_indcie { get; set; } = 0;

        [MaxLength(200, ErrorMessage = "El campo no debe de tener mas de 200 caracteres")]
        public string Rco_obscie { get; set; } = "";

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido (Categorizado)
        public Boolean Rco_indval { get; set; }        /***Boolean para CheckBox****/

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracteres")]
        public string Rco_rembls { get; set; }    /***View****/

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracteres")]
        public string Rco_presup { get; set; }    /***View****/


        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracteres")]
        public string Rco_priori { get; set; }      /***View****/

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracteres")]

        public string Rco_tiprco { get; set; }        /***View****/
        public string Rco_glorco { get; set; } //Resumen
        public string Dis_coddis { get; set; } //Disciplina
        public string Dis_deslar { get; set; }
        public Boolean Ccc_numero { get; set; }
        public string Occ_numero { get; set; }
        public string Cco_deslar { get; set; }
        public string S10_nomusu { get; set; }

        //Atributos para Req Compra Detalle (RCD)
        public List<DetalleReq> ListaDetalles { get; set; }

    }
}
