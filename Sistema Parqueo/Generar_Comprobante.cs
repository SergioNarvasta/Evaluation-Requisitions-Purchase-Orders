using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Parqueo
{
    public partial class Generar_Comprobante : Form
    {
        readonly SqlConnection conex = AdministradorDeConexion.getConexion();
        //SqlConnection conex = new SqlConnection("Data Source=(local);Initial Catalog=dbparqueo;Integrated Security=True");
        public Generar_Comprobante()
        {
            InitializeComponent();
            
            horaSalida();

            habilitarBotones(true,true, true, true);
           
        }

        private void horaSalida()
        {
            //DateTime a = DateTime.Now;
            String HoraIngreso = DateTime.Now.ToString("HH:mm:ss");
            txtHoraSali.Text = HoraIngreso;
            txtCodiClie.Focus();
        }
        
        private void operacionHora()
        {
            //double hoIngre = Convert.ToInt16(txtHoraIngreso);
            //double hoSali = Convert.ToDouble(txtHoraSali);

            TimeSpan tiemI = TimeSpan.Parse(txtHoraIngreso.Text);
            TimeSpan tiemS = TimeSpan.Parse(txtHoraSali.Text);
            TimeSpan resta = tiemS - tiemI;
            txtTiempoUso.Text = resta.ToString();

        }

        public Comprobante getObjetoComprobante()
        {
            Comprobante oComprobante = new Comprobante();
  
            oComprobante.codi_clie = txtDniClie.Text;
            oComprobante.nomb_clie = txtNombClie.Text;
            oComprobante.hora_ingreso = txtHoraIngreso.Text;
            oComprobante.hora_salida = txtHoraSali.Text;
            oComprobante.tiempo_uso = txtTiempoUso.Text;
            oComprobante.descuento = cbxDescuentos.SelectedItem.ToString();
            oComprobante.mont_comp = double.Parse(txtMonto.Text);
            return oComprobante;
        }

        private void habilitarBotones(Boolean cBuscar, Boolean cCancelar, Boolean cGenerar,Boolean cAnterior )
        {
            btnBusCliComp.Enabled = cBuscar;
            btnCancelar.Enabled = cCancelar;
            btnGenerar.Enabled = cGenerar;
            btnAnterior.Enabled = cAnterior;
            
        }

        private void habilitarCajasDeTexto(Boolean editable)
        {

            txtCodiClie.Enabled = editable;
            txtNombClie.Enabled = editable;
            txtDniClie.Enabled = editable;
           
        }

        private void limpiarCajasDeTexto()
        {
            txtCodiClie.Text = "";
            txtNombClie.Text = "";
            txtDniClie.Text = "";
            txtHoraIngreso.Text = "";
            txtHoraSali.Text = "";
            txtTiempoUso.Text = "";
            txtMonto.Text = "";
        }

        private void btnBusCliComp_Click(object sender, EventArgs e)
        {
            mostrarDatos();
    
        }
        private void insertarAlBD()
        {
            
                if (MessageBox.Show("¿Desea guardarlo?", "Confirme el guardado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ComprobanteDAO oComprobanteDAO = new ComprobanteDAO();
                    if (oComprobanteDAO.insertarRegistro(getObjetoComprobante()))
                    {

                        CopiarDatos();
                       btnGenerar.Focus();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo guardar el registro ... !!!");
                    }
                }
               
            
        }
        private void mostrarDatos()
        { 
            string comSql = "SELECT* FROM Cliente where dniClie='" + txtDniClie.Text + "'";
            SqlCommand comando = new SqlCommand(comSql,conex);
            conex.Open();

            SqlDataReader leer = comando.ExecuteReader();
            if (leer.Read() == true)
            {
                txtNombClie.Text = leer["nombClie"].ToString();
                txtCodiClie.Text = leer["codiClie"].ToString();
                txtHoraIngreso.Text = leer["horaClie"].ToString();
                habilitarBotones(false,true,true,true);
                conex.Close();
                MessageBox.Show("Usuario cargado con exito.");
            }
            else
            {
                MessageBox.Show("Usuario no registrado");
                limpiarCajasDeTexto();
            }
        }
        private void CopiarDatos()
        {
            string com1Sql = "INSERT INTO ImpresionTemp (id_comp1,fech_comp1,codi_clie1,nomb_clie1,hora_ingreso1,hora_salida1,tiempo_uso1,descuento1,mont_comp1 ) SELECT * FROM Comprobante  " ;
            SqlCommand comando = new SqlCommand(com1Sql, conex);
            conex.Open();

            SqlDataReader leer = comando.ExecuteReader();
            conex.Close();

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            ReporteComprobante orport = new ReporteComprobante();
            orport.Show();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarCajasDeTexto();
        }

        private void cbxDescuentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelecItem = cbxDescuentos.Text;
            
            string cant = this.txtTiempoUso.Text;
            string Uso = cant.Substring(0, 2);
            double resultado = 0;
            
            switch (SelecItem)
            {
                case "Descuento Plaza Vea":
                    if (Uso == "00")
                    {
                        resultado = resultado + 0;
                        txtMonto.Text = resultado.ToString();
                    }
                    else
                    {
                        if (Uso == "02")

                        {
                            resultado = resultado + 0;
                            txtMonto.Text = resultado.ToString();
                        }
                        else
                        {
                            if (Uso == "03")
                            {
                                resultado = resultado + 2;
                                txtMonto.Text = resultado.ToString();
                            }
                            else
                            {
                                if (Uso == "04")
                                {
                                    resultado = resultado + 2;
                                    txtMonto.Text = resultado.ToString();
                                }
                                else
                                {
                                    if (Uso == "05")
                                    {
                                        resultado = resultado + 12;
                                        txtMonto.Text = resultado.ToString();
                                    }
                                    else
                                    {
                                        if (Uso == "08")
                                        {
                                            resultado = resultado + 12;
                                            txtMonto.Text = resultado.ToString();
                                        }
                                        else
                                        {
                                            if (Uso == "09")
                                            {
                                                resultado = resultado + 12;
                                                txtMonto.Text = resultado.ToString();
                                            }
                                            else
                                            {
                                                if (Uso == "10")
                                                {
                                                    resultado = resultado + 12;
                                                    txtMonto.Text = resultado.ToString();
                                                }
                                                else
                                                {
                                                    if (Uso == "11")
                                                    {
                                                        resultado = resultado + 12;
                                                        txtMonto.Text = resultado.ToString();
                                                    }
                                                    else
                                                    {
                                                        if (Uso == "12")
                                                        {
                                                            resultado = resultado + 12;
                                                            txtMonto.Text = resultado.ToString();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "Sin Descuento":
                    if (Uso == "01")
                    {
                        resultado = resultado + 2;
                        txtMonto.Text = resultado.ToString();
                    }
                    else {
                        if (Uso == "02")
                            
                        {
                            resultado = resultado + 2;
                            txtMonto.Text = resultado.ToString();
                        }
                        else {
                            if (Uso == "03")
                            {
                                resultado = resultado + 2;
                                txtMonto.Text = resultado.ToString();
                            }
                            else {
                                if (Uso == "04")
                                {
                                    resultado = resultado + 2;
                                    txtMonto.Text = resultado.ToString();
                                }
                                else
                                {
                                    if (Uso == "05")
                                    {
                                        resultado = resultado + 12;
                                        txtMonto.Text = resultado.ToString();
                                    }
                                    else {
                                        if (Uso == "08")
                                        {
                                            resultado = resultado + 12;
                                            txtMonto.Text = resultado.ToString();
                                        }
                                        else
                                        {
                                            if (Uso == "09")
                                            {
                                                resultado = resultado + 12;
                                                txtMonto.Text = resultado.ToString();
                                            }
                                            else {
                                                if (Uso == "10")
                                                {
                                                    resultado = resultado + 12;
                                                    txtMonto.Text = resultado.ToString();
                                                }
                                                else {
                                                    if (Uso == "11")
                                                    {
                                                        resultado = resultado + 12;
                                                        txtMonto.Text = resultado.ToString();
                                                    }
                                                    else {
                                                        if (Uso == "12")
                                                        {
                                                            resultado = resultado + 12;
                                                            txtMonto.Text = resultado.ToString();
                                                        }
                                                        else 
                                                           resultado = resultado + 12;
                                                           txtMonto.Text = resultado.ToString();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;
                case "Descuento UVK ":
                    if (Uso == "01")
                    {
                        resultado = resultado + 0;
                        txtMonto.Text = resultado.ToString();
                    }
                    else
                    {
                        if (Uso == "02")

                        {
                            resultado = resultado + 0;
                            txtMonto.Text = resultado.ToString();
                        }
                        else
                        {
                            if (Uso == "03")
                            {
                                resultado = resultado + 0;
                                txtMonto.Text = resultado.ToString();
                            }
                            else
                            {
                                if (Uso == "04")
                                {
                                    resultado = resultado + 2;
                                    txtMonto.Text = resultado.ToString();
                                }
                                else
                                {
                                    if (Uso == "05")
                                    {
                                        resultado = resultado + 12;
                                        txtMonto.Text = resultado.ToString();
                                    }
                                    else
                                    {
                                        if (Uso == "08")
                                        {
                                            resultado = resultado + 12;
                                            txtMonto.Text = resultado.ToString();
                                        }
                                        else
                                        {
                                            if (Uso == "09")
                                            {
                                                resultado = resultado + 12;
                                                txtMonto.Text = resultado.ToString();
                                            }
                                            else
                                            {
                                                if (Uso == "10")
                                                {
                                                    resultado = resultado + 12;
                                                    txtMonto.Text = resultado.ToString();
                                                }
                                                else
                                                {
                                                    if (Uso == "11")
                                                    {
                                                        resultado = resultado + 12;
                                                        txtMonto.Text = resultado.ToString();
                                                    }
                                                    else
                                                    {
                                                        if (Uso == "12")
                                                        {
                                                            resultado = resultado + 12;
                                                            txtMonto.Text = resultado.ToString();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txtHoraIngreso_TextChanged(object sender, EventArgs e)
        {
            operacionHora();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            insertarAlBD();
            habilitarCajasDeTexto(false);
            btnConfirmar.Enabled = false;
            MessageBox.Show("Listo para imprimir");
        }
    }
}
