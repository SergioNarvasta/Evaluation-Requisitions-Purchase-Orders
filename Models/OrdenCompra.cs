﻿namespace ProjectWeb_DRA.Models
{
    public class OrdenCompra
    {
        public string Occ_numero { get; set; }
        public DateTime Occ_feccre { get; set; }
        public double Occ_tcaocc { get; set; }
        public string Ccr_codccr { get; set; }
        public string Ccr_nomaux { get; set; }
        public string Occ_observ { get; set; }
        public double Occ_impigv   { get; set; }
        public int  Tco_codtco   { get; set; }
        public string  Tco_nombre   { get; set; }
        public string Occ_estado   { get; set; }
        public string  Occ_destado  { get; set; }
        public int  Mon_codepk   { get; set; }
        public string Mon_desmon   { get; set; }     
        public int  Cpg_codepk   { get; set; } //Condicion Pago
        public string  Cpg_deslar   { get; set; }
        public  DateTime Occ_fecemi   { get; set; }
        public double  Occ_pordet   { get; set; }
        public double Occ_impdet   { get; set; }
        public int  Imp_codepk   { get; set; }
        public string  Imp_desimp   { get; set; }

    }
}