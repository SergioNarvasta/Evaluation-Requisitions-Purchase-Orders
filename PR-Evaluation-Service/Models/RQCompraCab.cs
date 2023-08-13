
using System.ComponentModel.DataAnnotations;

namespace HDProjectWeb.Models
{
    public class RQCompraCab
    {
     //Model for View Index
     [Key]
     public string Rco_numero { get; set; } 
     public DateTime Rco_fec_registro { get; set; } = DateTime.Now;
     public string Usuario_origen { get; set; }
     public string User_solicita { get; set; } 
     public string Rco_motivo { get; set; } 
     public string U_negocio { get; set; } 
     public string Centro_costo { get; set; } 
     public string Disciplina { get; set; } 
     public string Rco_situacion_aprobado { get; set; } 
     public string Rco_prioridad { get; set; } 
     public string Rco_justificacion { get; set; } 
     public string Rco_reembolso { get; set; } 
     public string Rco_presupuesto { get; set; } 
     public string Rco_categorizado { get; set; } 
     public string Ccc_numeroccc { get; set; } 
     public string Scc_cotizacion { get; set; } 
     public string Occ_numeroocc { get; set; } 
     public string Occ_dessituacionocc { get; set; } 
     public string OCC_ProveedorOCC { get; set; } 
    }
  
}
