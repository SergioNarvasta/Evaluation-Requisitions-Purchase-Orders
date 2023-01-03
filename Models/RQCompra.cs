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
        
        public int Rco_codepk{ get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(10, ErrorMessage = "El campo no debe de tener mas de 10 caracteres")]
        public string Rco_numrco { get; set; }            /***View****/

        [Required(ErrorMessage = "Campo obligatorio")]// Requerido
        
        public int Tin_codtin { get; set; }       /***Vacio****/

        [Required(ErrorMessage = "Campo obligatorio")]// Requerido
        [MaxLength(200, ErrorMessage = "El campo no debe de tener mas de 200 caracteres")]
        [Display(Name = "Motivo")]
        public string Rco_motivo { get; set; }    /***View****/

        [MaxLength(200, ErrorMessage = "El campo no debe de tener mas de 200 caracteres")]
        public string Rco_glorco { get; set; } //Resumen

        //[Required(ErrorMessage = "Campo obligatorio")]// Requerido
        
        public int Cco_codepk { get; set; }    /***View****/

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracter")]
        public string Rco_sitrco { get; set; }    /***View****/

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(30, ErrorMessage = "El campo no debe de tener mas de 30 caracteres")]
        public string Rco_codusu { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        
        public int Ung_codepk { get; set; }      /***View****/

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido (Categorizado) **********Revisar 
        public Boolean Rco_indval { get; set; }        /***Boolean para CheckBox****/

        [Required(ErrorMessage = "Campo obligatorio")]
        public int Rco_indest { get; set; }      /***View****/

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
        
        public int Tre_codepk { get; set; }        /***View****/

        [Required(ErrorMessage = "Campo obligatorio")] // Requerido
        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracteres")]
        public string Rco_estado { get; set; }

        public int Dis_codepk { get; set; } //Disciplina

        public int S10_codepk { get; set; } //User solicita

        public int Occ_codepk { get; set; }
        public string Occ_numero { get; set; }
        public string Dis_nomlar { get; set; }

        public string Cco_codcco { get; set; }
        public string Cco_descco { get; set; }
        public string S10_nomusu { get; set; }
        public string S10_codusu { get; set; }

        //Atributos para Req Compra Detalle (RCD)
        public List<DetalleReq> ListaDetalles { get; set; }


        public DateTime Rco_fec_registro { get; set; }

       // [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        public short Rco_indcie { get; set; } = 0;

        [MaxLength(200, ErrorMessage = "El campo no debe de tener mas de 200 caracteres")]
        public string Rco_obscie { get; set; } = "";

    }
}
