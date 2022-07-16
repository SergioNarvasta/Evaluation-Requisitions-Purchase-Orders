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
    public partial class Registrar_cliente : Form
    {   private int action = ABC_Acciones.NO_ACTION;
        public Registrar_cliente()
        {
            InitializeComponent();
        }

        public Cliente getObjetoCliente()
        {
            Cliente oCliente = new Cliente();
            oCliente.codiClie = txtCodClie.Text;
            oCliente.nombClie = txtNomClie.Text;
            oCliente.dniClie = txtDniCli.Text;
            oCliente.telfClie = txtTelefCli.Text;
            oCliente.placClie = txtNumAutcli.Text;
            oCliente.modeClie = txtModAutcli.Text;
            oCliente.coloClie = txtColAutcli.Text;
            return oCliente;
        }

        public void setObjetoCliente(Cliente oCliente)
        {
            txtCodClie.Text = oCliente.codiClie + "";
            txtNomClie.Text = oCliente.nombClie + "";
            txtDniCli.Text = oCliente.dniClie + "";
            txtTelefCli.Text = oCliente.telfClie + "";
            txtNumAutcli.Text = oCliente.placClie + "";
            txtModAutcli.Text = oCliente.modeClie + "";
            txtColAutcli.Text = oCliente.coloClie + "";
        }

        public void actualizarDataGridViewAlumno()
        {
            limpiarDataGridViewCliente();
            ClienteDAO oClientDAO = new ClienteDAO();
            List<Cliente> oListCliente = oClientDAO.obtenerDatosEnList();
            for (int posicion = 0; posicion < oListCliente.Count; posicion = posicion + 1)
            {
                if (oListCliente[posicion].codiClie != 0)
                {
                    dataGridViewCliente.Rows.Add(oListCliente[posicion].codiClie, oListCliente[posicion], oListCliente[posicion].nombClie, oListCliente[posicion].dniClie, oListCliente[posicion].telfClie, oListCliente[posicion].placClie);
                }
            }
        }
        private void habilitarBotones(Boolean cBuscar, Boolean cNuevo, Boolean cRegistrar, Boolean cActualizar, Boolean cEliminar, Boolean cSalir)
        {
            btnBusCliReg.Enabled = cBuscar;
            btnNueRegCli.Enabled = cNuevo;
            btnRegCli.Enabled = cRegistrar;
            btnActCli.Enabled = cActualizar;
            btnEliCli.Enabled = cEliminar;
            btnSalir.Enabled = cSalir;
        }

        private void habilitarCajasDeTexto(Boolean editable)
        {
            txtNomClie.Enabled = editable;
            txtDniCli.Enabled = editable;
            txtTelefCli.Enabled = editable;
            txtNumAutcli.Enabled = editable;
            txtModAutcli.Enabled = editable;
            txtColAutcli.Enabled = editable;
        }

        private void limpiarCajasDeTexto()
        {
            txtCodClie.Text = "";
            txtNomClie.Text = "";
            txtDniCli.Text = "";
            txtTelefCli.Text = "";
            txtNumAutcli.Text = "";
            txtModAutcli.Text = "";
            txtColAutcli.Text = "";
        }
        private void btnBusCliReg_Click(object sender, EventArgs e)
        {
            if ((txtDniCli.Text.Length) > 0)
            {
                Cliente oCliente = new Cliente();
                ClienteDAO oClienteDAO = new ClienteDAO();
                int busqueda = int.Parse(txtDniCli.Text);
                busqueda = oClienteDAO.buscarRegistro(busqueda);
                if (busqueda == -99)
                {
                    txtCodClie.Enabled = false;
                    habilitarCajasDeTexto(true);
                    habilitarBotones(false, false, true, false, false, false);
                }
                else
                {
                    MessageBox.Show("El registro ya existe ... !!!");
                    action = ABC_Acciones.NO_ACTION;
                    controladorDeEventosBotonesABCM();
                }
            }
            else
            {
                MessageBox.Show("Ingrese el dni del cliente... !!!");
                txtCodClie.Focus();
            }
        }

        private void btnNueRegCli_Click(object sender, EventArgs e)
        {
            dataGridViewCliente.ClearSelection();
            habilitarBotones(true, false, false, false, false,  false);
            action = ABC_Acciones.ACTION_INSERTAR;
            limpiarCajasDeTexto();
            txtCodClie.Enabled = true;
            txtCodClie.Focus();
        }

        private void btnRegCli_Click(object sender, EventArgs e)
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
                        btnNueRegCli.Focus();
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
                        if (oClienteDAO.modificarRegistro(int.Parse(txtDniCli.Text), getObjetoCliente()))
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

        private void btnActCli_Click(object sender, EventArgs e)
        {
            if (dataGridViewCliente.RowCount >= 1)
            {
                int filaSeleccionada = dataGridViewCliente.CurrentRow.Index;
                if (filaSeleccionada != -1 && (txtDniCli.Text.Length) > 0)
                {
                    txtNomClie.Focus();
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

        private void btnEliCli_Click(object sender, EventArgs e)
        {
            if (dataGridViewCliente.RowCount >= 1)
            {
                int filaSeleccionada = dataGridViewCliente.CurrentRow.Index;
                if (filaSeleccionada != -1 && (txtDniCli.Text.Length) > 0)
                {
                    if (MessageBox.Show("Está seguro de eliminar los datos?", "Confirme la eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ClienteDAO oClienteDAO = new ClienteDAO();
                        Cliente oCliente = new Cliente();
                        int busqueda = int.Parse(txtDniCli.Text);
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

        private void btnSalir_Click(object sender, EventArgs e)
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

        private void limpiarDataGridViewCliente()
        {
            dataGridViewCliente.Rows.Clear();
        }

        private void controladorDeEventosBotonesABCM()
        {
            if (action == ABC_Acciones.NO_ACTION)
            {
                txtDniCli.Enabled = false;
                habilitarCajasDeTexto(false);
                habilitarBotones(false, true, false, true, true, true);
                btnNueRegCli.Focus();
                limpiarCajasDeTexto();
                dataGridViewCliente.ClearSelection();
            }
            else
            {
                if (action == ABC_Acciones.ACTION_INSERTAR)
                {
                    limpiarCajasDeTexto();
                    txtDniCli.Enabled = true;
                    habilitarBotones(true, false, true, false, true,  false);
                    ClienteDAO oClienteDAO = new ClienteDAO();
                    Cliente oCliente = new Cliente();
                    int busqueda = 0;
                    busqueda = int.Parse(txtDniCli.Text);
                    oCliente = oClienteDAO.consultarRegistro(busqueda);
                    if (oCliente == null)
                    {
                        btnBusCliReg.Enabled = false;
                        txtDniCli.Enabled = false;
                        habilitarCajasDeTexto(true);
                        habilitarBotones(true, false, true, false, true,  false);
                    }
                    else
                    {
                        btnBusCliReg.Enabled = true;
                        MessageBox.Show("El cliente existe ...!!!");
                    }
                }
                else
                {
                    if (action == ABC_Acciones.ACTION_MODIFICAR)
                    {
                        habilitarCajasDeTexto(true);
                        habilitarBotones(false, false, true, false, false, false);
                        txtDniCli.Enabled = false;
                    }
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
    }
}
