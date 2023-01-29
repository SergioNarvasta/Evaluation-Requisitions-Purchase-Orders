using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Parqueo
{
   public class Cliente
    {
        public int codiClie { get; set; }
        public String nombClie { get; set; }
        public int dniClie { get; set; }
        public String telfClie { get; set; }
        public String placClie { get; set; }
        public String modeClie { get; set; }
        public String coloClie { get; set; }
        public String fechClie { get; set; }
        public String horaClie { get; set; }
        public Cliente()
        { 
        }

        public Cliente(int codi, String nomb, int dni, String telf, String plac, String mode, String colo,String fini,String hocli)
        {
            codiClie = codi;
            nombClie = nomb;
            dniClie = dni;
            telfClie = telf;
            placClie = plac;
            modeClie = mode;
            coloClie = colo;
            fechClie = fini;
            horaClie = hocli;

        }
    }
}
