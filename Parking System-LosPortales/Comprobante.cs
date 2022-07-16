using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Parqueo
{
    public class Comprobante
    {
        public int id_comp      { get; set; }
        public string fech_comp { get; set; }
        public string codi_clie { get; set; }
        public string nomb_clie { get; set; }
        public string hora_ingreso { get; set; }
        public string hora_salida { get; set; }
        public string tiempo_uso { get; set; }
        public string descuento { get; set; }
        public double mont_comp { get; set; }

       public Comprobante() { }

        public Comprobante(int idcomp,string feccomp,string codiclie,string nombclie,string horaingr,string horasali,string tiempouso,string descuentoCom, double monto)
        {
            id_comp = idcomp;
            fech_comp = feccomp;
            codi_clie = codiclie;
            nomb_clie = nombclie;
            hora_ingreso = horaingr;
            hora_salida = horasali;
            tiempo_uso = tiempouso;
            descuento = descuentoCom;
            mont_comp = monto;
        }

    }
   
}
