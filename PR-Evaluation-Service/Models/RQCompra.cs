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
        public string Rco_numero { get; set; }            /***View****/

        [Required(ErrorMessage = "Campo obligatorio")]// Requerido
        
        public int Tin_codtin { get; set; }       /***Vacio****/

        [Required(ErrorMessage = "**")]// Requerido
        [MaxLength(200, ErrorMessage = "El campo no debe de tener mas de 200 caracteres")]
        [Display(Name = "Motivo")]
        public string Rco_motivo { get; set; }    /***View****/

        [MaxLength(200, ErrorMessage = "El campo no debe de tener mas de 200 caracteres")]
        public string Rco_glorco { get; set; } //Resumen
        [Required(ErrorMessage = "**")]
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

        public int Uap_codepk { get; set; } //User solicita
        public string Uap_deslar { get; set; }
        public string Uap_coduap { get; set; }

        [Required(ErrorMessage = "**")]
        public int Occ_codepk { get; set; }
        public string Occ_numero { get; set; }
        public string Dis_nomlar { get; set; }
        public string Cco_codcco { get; set; }
        public string Cco_descco { get; set; }   
        public DateTime Rco_fec_registro { get; set; }  
        public short Rco_indcie { get; set; } = 0;

        [MaxLength(200, ErrorMessage = "El campo no debe de tener mas de 200 caracteres")]
        public string Rco_obscie { get; set; } = "";

        //@rcd_corite,@prd_codepk,@rcd_desprd,@rcd_glorcd,@rcd_canate,@ccr_codepk,@ume_codepk
        public string DPrd_item { get; set; }
        public string DPrd_descri { get; set; }
        public string DPrd_codigo { get; set; }
        public string DPrd_glosa { get; set; }
        public string DPrd_unidad { get; set; }
        public string DPrd_cantidad { get; set; }
        public string DPrd_codprov { get; set; }

        public string DFi_item1 { get; set; }
        public string DFi_nom1 { get; set; }
        public string DFi_cod1 { get; set; }
        public string DFi_fil1 { get; set; }
        public string DFi_item2 { get; set; }
        public string DFi_nom2 { get; set; }
        public string DFi_cod2 { get; set; }
        public string DFi_fil2 { get; set; }

        //Atributos para Req Compra Detalle (RCD)
        public List<DetalleReq> ListaDetalles { get; set; }
    }
}
