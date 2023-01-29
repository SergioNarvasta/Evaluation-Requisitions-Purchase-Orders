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
    public partial class RegistrarCliente : Form
    {
        private int action = ABC_Acciones.NO_ACTION;
        public RegistrarCliente()
        {
            InitializeComponent();
            controladorDeEventosBotonesABCM();
            actualizarDataGridViewAlumno();
            hora();
        }

        public Cliente getObjetoCliente()
        {
            Cliente oCliente = new Cliente();
            ///oCliente.codiClie = int.Parse( txtCodiClie.Text);
            oCliente.nombClie = txtNombClie.Text;
            oCliente.dniClie = int.Parse(txtDNI.Text);
            oCliente.telfClie = txtTeleClie.Text;
            oCliente.placClie = txtPlacaClie.Text;
            oCliente.modeClie = txtModeClie.Text;
            oCliente.coloClie = txtColoClie.Text;
            return oCliente;
        }

        public void setObjetoCliente(Cliente oCliente)
        {
           
            txtNombClie.Text = oCliente.nombClie + "";
            txtDNI.Text = oCliente.dniClie + "";
            txtTeleClie.Text = oCliente.telfClie + "";
            txtPlacaClie.Text = oCliente.placClie + "";
            txtModeClie.Text = oCliente.modeClie + "";
            txtColoClie.Text = oCliente.coloClie + "";
            txtHoraIngre.Text = oCliente.horaClie + "";
        }

        public void actualizarDataGridViewAlumno()
        {
            limpiarDataGridViewCliente();
            ClienteDAO oClientDAO = new ClienteDAO();
            List<Cliente> oListCliente = oClientDAO.obtenerDatosEnList();
            for (int posicion = 0; posicion < oListCliente.Count; posicion = posicion + 1)
            {
                if (oListCliente[posicion].dniClie != 0)
                {
                    dataGridViewCliente.Rows.Add(oListCliente[posicion].codiClie, oListCliente[posicion], oListCliente[posicion].nombClie, oListCliente[posicion].dniClie, oListCliente[posicion].telfClie, oListCliente[posicion].placClie, oListCliente[posicion].modeClie, oListCliente[posicion].coloClie,oListCliente[posicion].fechClie,oListCliente[posicion].horaClie);
                }
            }
        }

        private void habilitarBotones(Boolean cBuscar, Boolean cNuevo, Boolean cRegistrar, Boolean cActualizar, Boolean cEliminar, Boolean cSalir)
        {
            btnBuscar.Enabled = cBuscar;
            btnNuevo.Enabled = cNuevo;
            btnRegistrar.Enabled = cRegistrar;
            btnActualizar.Enabled = cActualizar;
            btnEliminar.Enabled = cEliminar;
            btnSalir.Enabled = cSalir;
        }
        private void hora()
        {
            DateTime hora = DateTime.Now;
            txtHoraIngre.Text = hora.ToShortTimeString();
         
        }
        private void habilitarCajasDeTexto(Boolean editable)
        {
           
            txtNombClie.Enabled = editable;
            txtTeleClie.Enabled = editable;
            txtPlacaClie.Enabled = editable;
            txtModeClie.Enabled = editable;
            txtColoClie.Enabled = editable;
        }
        private void limpiarCajasDeTexto()
        {
            
            txtNombClie.Text = "";
            txtDNI.Text = "";
            txtTeleClie.Text = "";
            txtPlacaClie.Text = "";
            txtModeClie.Text = "";
            txtColoClie.Text = "";
        }
        private void limpiarDataGridViewCliente()
        {
            dataGridViewCliente.Rows.Clear();
        }

        private void controladorDeEventosBotonesABCM()
        {
            if (action == ABC_Acciones.NO_ACTION)
            {
                txtDNI.Enabled = false;
                habilitarCajasDeTexto(false);
                habilitarBotones(false, true, false, true, true, true);
                btnNuevo.Focus();
                limpiarCajasDeTexto();
                dataGridViewCliente.ClearSelection();
            }
            else
            {
                if (action == ABC_Acciones.ACTION_INSERTAR)
                {
                    limpiarCajasDeTexto();
                    txtDNI.Enabled = true;
                    habilitarBotones(true, false, true, false, true, false);
                    ClienteDAO oClienteDAO = new ClienteDAO();
                    Cliente oCliente = new Cliente();
                    int busqueda = 0;
                    busqueda = int.Parse(txtDNI.Text);
                    oCliente = oClienteDAO.consultarRegistro(busqueda);
                    if (oCliente == null)
                    {
                        btnBuscar.Enabled = false;
                        txtDNI.Enabled = false;
                        habilitarCajasDeTexto(true);
                        habilitarBotones(true, false, true, false, true, false);
                    }
                    else
                    {
                        btnBuscar.Enabled = true;
                        MessageBox.Show("El cliente existe ...!!!");
                    }
                }
                else
                {
                    if (action == ABC_Acciones.ACTION_MODIFICAR)
                    {
                        habilitarCajasDeTexto(true);
                        habilitarBotones(false, false, true, false, false, false);
                        txtDNI.Enabled = false;
                    }
                }
            }
        }

        /*private void dataGridViewCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                int filaSeleccionada = dataGridViewCliente.CurrentRow.Index + 1;
                if (filaSeleccionada >= dataGridViewCliente.RowCount)
                {
                    filaSeleccionada = filaSeleccionada - 1;
                }
                else
                {
                    Cliente oClienteSeleccionado = (Cliente)dataGridViewCliente.Rows[filaSeleccionada].Cells[1].Value;
                    setObjetoCliente(oClienteSeleccionado);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                int filaSeleccionada = dataGridViewCliente.CurrentRow.Index - 1;
                if (filaSeleccionada <= -1)
                {
                    filaSeleccionada = filaSeleccionada + 1;
                }
                else
                {
                    Cliente oClienteSeleccionado = (Cliente)dataGridViewCliente.Rows[filaSeleccionada].Cells[1].Value;
                    setObjetoCliente(oClienteSeleccionado);
                }
            }
        }*/

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if ((txtDNI.Text.Length) > 0)
            {
                Cliente oCliente = new Cliente();
                ClienteDAO oClienteDAO = new ClienteDAO();
                int busqueda = int.Parse(txtDNI.Text);
                busqueda = oClienteDAO.buscarRegistro(busqueda);
                if (busqueda == -99)
                {
                    
                    habilitarCajasDeTexto(true);
                    habilitarBotones(false, false, true, false, false, false);
                
                }
                else
                {
                    MessageBox.Show("EL usuario ya existe");
                    action = ABC_Acciones.NO_ACTION;
                    controladorDeEventosBotonesABCM();
                }
            }
            else
            {
                MessageBox.Show("Ingrese el dni del cliente... !!!");
                txtDNI.Focus();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            dataGridViewCliente.ClearSelection();
            habilitarBotones(true, false, false, false, false, false);
            action = ABC_Acciones.ACTION_INSERTAR;
            limpiarCajasDeTexto();
            txtDNI.Enabled = true;
            txtDNI.Focus();
            hora();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {

            if (action == ABC_Acciones.ACTION_INSERTAR)
            {
                if (MessageBox.Show("¿Desea guardarlo?", "Confirme el guardado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ClienteDAO oClienteDAO = new ClienteDAO();
                    if (oClienteDAO.insertarRegistro(getObjetoCliente()))
                    {
                        action = ABC_Acciones.NO_ACTION;
                        controladorDeEventosBotonesABCM();
                        actualizarDataGridViewAlumno();
                        btnRegistrar.Focus();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo guardar el registro ... !!!");
                    }
                }
                else
                {
                    limpiarCajasDeTexto();
                    action = ABC_Acciones.NO_ACTION;
                    controladorDeEventosBotonesABCM();
                }
            }
            else
            {
                if (action == ABC_Acciones.ACTION_MODIFICAR)
                {
                    if (MessageBox.Show("Está seguro de guardar los datos?", "Confirme el guardado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ClienteDAO oClienteDAO = new ClienteDAO();
                        Cliente oCliente = new Cliente();
                        if (oClienteDAO.modificarRegistro(int.Parse(txtDNI.Text), getObjetoCliente()))
                        {
                            MessageBox.Show("Operación exitosa ... !!!");
                            action = ABC_Acciones.NO_ACTION;
                            controladorDeEventosBotonesABCM();
                            actualizarDataGridViewAlumno();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo guardar el registro ...!!!");
                        }
                    }
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            if (dataGridViewCliente.RowCount >= 1)
            {
                int filaSeleccionada = dataGridViewCliente.CurrentRow.Index;
                if (filaSeleccionada != -1 && (txtDNI.Text.Length) > 0)
                {
                    txtNombClie.Focus();
                    action = ABC_Acciones.ACTION_MODIFICAR;
                    controladorDeEventosBotonesABCM();
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado un registro ...!!!");
                }
            }
            else
            {
                MessageBox.Show("No existen registros ...!!!");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewCliente.RowCount >= 1)
            {
                int filaSeleccionada = dataGridViewCliente.CurrentRow.Index;
                if (filaSeleccionada != -1 && (txtDNI.Text.Length) > 0)
                {
                    if (MessageBox.Show("Está seguro de eliminar los datos?", "Confirme la eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ClienteDAO oClienteDAO = new ClienteDAO();
                        Cliente oCliente = new Cliente();
                        int busqueda = int.Parse(txtDNI.Text);
                        oCliente = oClienteDAO.consultarRegistro(busqueda);
                        if (oCliente != null)
                        {
                            oCliente = getObjetoCliente();
                            oCliente.dniClie = 0;//falta dnicliente
                            if (oClienteDAO.eliminarRegistro(busqueda))
                            {
                                MessageBox.Show("Se eliminó el registro ...!!!");
                                action = ABC_Acciones.NO_ACTION;
                                controladorDeEventosBotonesABCM();
                                actualizarDataGridViewAlumno();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo eliminar el registro ...!!!");
                                action = ABC_Acciones.NO_ACTION;
                                controladorDeEventosBotonesABCM();
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el registro ...!!!");
                            action = ABC_Acciones.NO_ACTION;
                            controladorDeEventosBotonesABCM();
                        }
                    }
                    else
                    {
                        action = ABC_Acciones.NO_ACTION;
                        controladorDeEventosBotonesABCM();
                    }
                }
                else
                {
                    MessageBox.Show("Se tiene que seleccionar un registro ...!!!");
                }
            }
            else
            {
                MessageBox.Show("No existen registros ...!!!");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            action = ABC_Acciones.NO_ACTION;
            controladorDeEventosBotonesABCM();
            btnNuevo.Focus();
            hora();
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dataGridViewCliente_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1 && dataGridViewCliente.RowCount >= 1)
            {
                int filaSeleccionada = dataGridViewCliente.CurrentRow.Index;
                if (filaSeleccionada != -1)
                {
                    Cliente oClienteSeleccionado = (Cliente)dataGridViewCliente.Rows[filaSeleccionada].Cells[1].Value;
                    setObjetoCliente(oClienteSeleccionado);
                }
            }
        }

        private void dataGridViewCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                int filaSeleccionada = dataGridViewCliente.CurrentRow.Index + 1;
                if (filaSeleccionada >= dataGridViewCliente.RowCount)
                {
                    filaSeleccionada = filaSeleccionada - 1;
                }
                else
                {
                    Cliente oClienteSeleccionado = (Cliente)dataGridViewCliente.Rows[filaSeleccionada].Cells[1].Value;
                    setObjetoCliente(oClienteSeleccionado);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                int filaSeleccionada = dataGridViewCliente.CurrentRow.Index - 1;
                if (filaSeleccionada <= -1)
                {
                    filaSeleccionada = filaSeleccionada + 1;
                }
                else
                {
                    Cliente oClienteSeleccionado = (Cliente)dataGridViewCliente.Rows[filaSeleccionada].Cells[1].Value;
                    setObjetoCliente(oClienteSeleccionado);
                }
            }
        }

        /* private void dataGridViewCliente_MouseClick_1(object sender, MouseEventArgs e)
         {
             if (e.Clicks == 1 && dataGridViewCliente.RowCount >= 1)
             {
                 int filaSeleccionada = dataGridViewCliente.CurrentRow.Index;
                 if (filaSeleccionada != -1)
                 {
                     Cliente oClienteSeleccionado = (Cliente)dataGridViewCliente.Rows[filaSeleccionada].Cells[1].Value;
                     setObjetoCliente(oClienteSeleccionado);
                 }
             }
         }*/
    }
}


