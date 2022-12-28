using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace HDProjectWeb.Models
{
    public class RQCompra_Complete
    {
        //Class Purchase requirement Requerimiento_Compra_RCO

        [Required(ErrorMessage = "Campo obligatorio")]//Campo Requerido
        [MaxLength(2, ErrorMessage = "El campo no debe de tener mas de 2 caracteres")]
        public string Cia_codcia { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]//Campo Requerido
        [MaxLength(2, ErrorMessage = "El campo no debe de tener mas de 2 caracteres")]
        public string Suc_codsuc { get; set; }

        [Key]
        [Required(ErrorMessage = "Campo obligatorio")]//Campo Requerido
        [MaxLength(10, ErrorMessage = "El campo no debe de tener mas de 10 caracteres")]
        public string Rco_numrco { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]//Campo Requerido
        [MaxLength(2, ErrorMessage = "El campo no debe de tener mas de 2 caracteres")]
        public string Tin_codtin { get; set; }

        public DateTime Rco_fecreg { get; set; }

        public DateTime Rco_fecsug { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        [MaxLength(2, ErrorMessage = "El campo no debe de tener mas de 2 caracteres")]
        public string Adi_codadi { get; set; }

        [MaxLength(20, ErrorMessage = "El campo no debe de tener mas de 20 caracteres")]
        public string S10_usuario { get; set; }

        [MaxLength(200, ErrorMessage = "El campo no debe de tener mas de 200 caracteres")]
        public string Rco_motivo { get; set; }

        [MaxLength(200, ErrorMessage = "El campo no debe de tener mas de 200 caracteres")]
        public string Rco_glorco { get; set; }

        [MaxLength(6, ErrorMessage = "El campo no debe de tener mas de 6 caracteres")]
        public string Cco_codcco { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracter")]
        public string Rco_sitrco { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracter")]
        public string Rco_sitlog { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        [MaxLength(4, ErrorMessage = "El campo no debe de tener mas de 4 caracteres")]
        public string Ano_codano { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        [MaxLength(2, ErrorMessage = "El campo no debe de tener mas de 2 caracteres")]
        public string Mes_codmes { get; set; }

        [MaxLength(30, ErrorMessage = "El campo no debe de tener mas de 30 caracteres")]
        public string Usu_codapr { get; set; }

        public DateTime Rco_fecapr { get; set; }

        [MaxLength(200, ErrorMessage = "El campo no debe de tener mas de 200 caracteres")]
        public string Rco_gloapr { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracter")]
        public string Rco_indest { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        public DateTime Rco_feccre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        [MaxLength(30, ErrorMessage = "El campo no debe de tener mas de 30 caracteres")]
        public string Rco_usucre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        public DateTime Rco_fecact { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        [MaxLength(30, ErrorMessage = "El campo no debe de tener mas de 30 caracteres")]
        public string Rco_codusu { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        [MaxLength(2, ErrorMessage = "El campo no debe de tener mas de 2 caracteres")]
        public string Ung_codung { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        public short Rco_indcie { get; set; }

        [MaxLength(200, ErrorMessage = "El campo no debe de tener mas de 200 caracteres")]
        public string Rco_obscie { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        public short Rco_indval { get; set; }

        [MaxLength(20, ErrorMessage = "El campo no debe de tener mas de 20 caracteres")]
        public string Rco_numpcn { get; set; }

        [MaxLength(2, ErrorMessage = "El campo no debe de tener mas de 2 caracteres")]
        public string Dis_coddis { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracteres")]
        public string Rco_rembls { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracteres")]
        public string Rco_presup { get; set; }

        [MaxLength(9, ErrorMessage = "El campo no debe de tener mas de 9 caracteres")]
        public string Rco_9digit { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] //Campo Requerido
        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracteres")]
        public string Rco_priori { get; set; }

        [MaxLength(11, ErrorMessage = "El campo no debe de tener mas de 11 caracteres")]
        public string Rco_codalt { get; set; }

        [MaxLength(100, ErrorMessage = "El campo no debe de tener mas de 100 caracteres")]
        public string Rco_obspri { get; set; }

        [MaxLength(100, ErrorMessage = "El campo no debe de tener mas de 100 caracteres")]
        public string Rco_rutdoc { get; set; }

        public DateTime Rco_fecprg { get; set; }

        public short Rco_procur { get; set; }

        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracter")]
        public string Rco_tiprco { get; set; }

        [MaxLength(10, ErrorMessage = "El campo no debe de tener mas de 10 caracteres")]
        public string Ocm_corocm { get; set; }

        [MaxLength(2, ErrorMessage = "El campo no debe de tener mas de 2 caracteres")]
        public string Tmo_codtmo { get; set; }

        public DateTime Rco_fecent { get; set; }

        public double Rco_impser { get; set; }

        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracter")]
        public string Rco_flgcom { get; set; }

        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracter")]
        public string Rco_flgate { get; set; }

        [MaxLength(200, ErrorMessage = "El campo no debe de tener mas de 200 caracteres")]
        public string Rco_jusate { get; set; }

        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracter")]
        public string Rco_flgmig { get; set; }

        public double Rco_imppor { get; set; }

        [MaxLength(100, ErrorMessage = "El campo no debe de tener mas de 100 caracteres")]
        public string Rco_hito01 { get; set; }

        [MaxLength(3, ErrorMessage = "El campo no debe de tener mas de 3 caracteres")]
        public string Cpa_codcpa { get; set; }

        public double Rco_imprec { get; set; }

        public double Rco_impfac { get; set; }

        [MaxLength(10, ErrorMessage = "El campo no debe de tener mas de 10 caracteres")]
        public string Rco_numsol { get; set; }

        [MaxLength(2, ErrorMessage = "El campo no debe de tener mas de 2 caracteres")]
        public string Rco_monpre { get; set; }

        public double Rco_imppre { get; set; }

        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracter")]
        public string Rco_sitctb { get; set; }

        public double Rco_ajufac { get; set; }

        public double Rco_impcfm { get; set; }

        public DateTime Rco_feinre { get; set; }

        public DateTime Rco_fefire { get; set; }

        [MaxLength(1, ErrorMessage = "El campo no debe de tener mas de 1 caracter")]
        public string Rco_clfprv { get; set; }

        [MaxLength(100, ErrorMessage = "El campo no debe de tener mas de 100 caracteres")]
        public string Rco_gloclf { get; set; }

        public short Rco_ultcer { get; set; }

        public DateTime Rco_feaclf { get; set; }

        [MaxLength(30, ErrorMessage = "El campo no debe de tener mas de 30 caracteres")]
        public string Rco_usaclf { get; set; }


    }
}
