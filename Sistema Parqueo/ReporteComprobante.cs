using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Parqueo
{
    public partial class ReporteComprobante : Form
    {
        public ReporteComprobante()

        {

            InitializeComponent();

        }
        private void ReporteComprobante_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetImpresionTemp.ImpresionTemp' Puede moverla o quitarla según sea necesario.
            this.ImpresionTempTableAdapter.Fill(this.DataSetImpresionTemp.ImpresionTemp);

            this.reportViewer1.RefreshReport();

        }
        private void ReporteComprobante_FormClosed(object sender, FormClosedEventArgs e)
        {
            ComprobanteDAO objComp = new ComprobanteDAO();
         
            objComp.BorrarDatosTemp();
        }
    }
}
