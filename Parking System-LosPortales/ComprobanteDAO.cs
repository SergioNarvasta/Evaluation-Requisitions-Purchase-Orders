using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Parqueo
{
    public class ComprobanteDAO
    {
        SqlConnection oSqlConnection;
        public Boolean insertarRegistro(Comprobante objComprobante)
        {
            try
            {
                String fechasalida = DateTime.Now.ToString("dd/MM/yyyy");
                oSqlConnection = AdministradorDeConexion.getConexion();
                oSqlConnection.Open();

                String sentencia = "INSERT INTO Comprobante(  fech_comp , codi_clie , nomb_clie ,hora_ingreso,hora_salida,tiempo_uso,descuento, mont_comp) values('" +
                                    //objComprobante.id_comp + "','" +
                                    fechasalida + "','" +
                                    objComprobante.codi_clie + "','" +
                                    objComprobante.nomb_clie + "','" +
                                    objComprobante.hora_ingreso + "','" +
                                    objComprobante.hora_salida + "','" +
                                    objComprobante.tiempo_uso + "','" +
                                    objComprobante.descuento + "','" +
                                    objComprobante.mont_comp + "')";

                SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch (System.Exception e)
            {
                oSqlConnection.Close();
                MessageBox.Show("Error ...!!!" + e.Message);
                return false;
            }
        }
        public void BorrarDatosTemp()
        {
            oSqlConnection = AdministradorDeConexion.getConexion();
            oSqlConnection.Open();
            string comande = "DELETE FROM ImpresionTemp";
            SqlCommand oSqlCommand = new SqlCommand(comande, oSqlConnection);
            oSqlCommand.ExecuteNonQuery();
            oSqlConnection.Close();

        }

        public Comprobante consultarRegistro(int busqueda)
        {
            try
            {
                Comprobante objComprobante;
                SqlConnection oSqlConnection = AdministradorDeConexion.getConexion();
                oSqlConnection.Open();
                String sentencia = "SELECT * FROM Comprobante WHERE codi_clie =" + busqueda;
                SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                if (oSqlDataReader.Read())
                {
                    objComprobante = new Comprobante();
                    objComprobante.id_comp = (int)oSqlDataReader["id_comp"];
                    objComprobante.fech_comp = (string)oSqlDataReader["fech_comp"];
                    objComprobante.codi_clie = (String)oSqlDataReader["codi_clie"];
                    objComprobante.nomb_clie = (String)oSqlDataReader["nomb_clie"];
                    objComprobante.mont_comp = (double)oSqlDataReader["mont_comp"];

                    oSqlDataReader.Close();
                    return objComprobante;
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception e)
            {
                oSqlConnection.Close();
                MessageBox.Show("Error ...!!!" + e.Message);
                return null;
            }
        }

        public Boolean modificarRegistro(int busqueda, Comprobante objComprobante)
        {
            try
            {
                SqlConnection oSqlConnection = AdministradorDeConexion.getConexion();
                oSqlConnection.Open();
                string sentencia = "UPDATE Comprobante SET  fech_comp    = '" + objComprobante.fech_comp + "', " +
                                                                 "codi_clie =  " + objComprobante.codi_clie + ", " +
                                                                 "nomb_clie =  " + objComprobante.nomb_clie + ", " +
                                                                 "mont_comp  =  " + objComprobante.mont_comp +

                                                                 " WHERE id_comp   =  " + busqueda;
                SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch (System.Exception e)
            {
                oSqlConnection.Close();
                MessageBox.Show("Error ...!!!" + e.Message);
                return false;
            }
        }
        public Boolean eliminarRegistro(int busqueda)
        {
            try
            {
                SqlConnection oSqlConnection = AdministradorDeConexion.getConexion();
                oSqlConnection.Open();
                string sentencia = "DELETE FROM  WHERE Comprobante id_comp =" + busqueda;
                SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch (System.Exception e)
            {
                oSqlConnection.Close();
                MessageBox.Show("Error ...!!!" + e.Message);
                return false;
            }
        }
        /*public int buscarRegistro(int busqueda)
        {
            Comprobante objComprobante = new Comprobante();
            try
            {
                SqlConnection oSqlConnection = AdministradorDeConexion.getConexion();
                oSqlConnection.Open();

                String sentencia = "SELECT * FROM Comprobante WHERE id_comp=" + busqueda;
                SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);

                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();

                if (oSqlDataReader.Read())
                {
                    objComprobante.id_comp = (Int64)oSqlDataReader["id_comp"];
                    oSqlDataReader.Close();
                    oSqlConnection.Close();
                    return objComprobante.id_comp;
                }
                return -99;
            }
            catch (System.Exception e)
            {
                //oSqlConnection.Close();
                MessageBox.Show("Error ...!!!" + e.Message);
                return -99;
            }

        }*/

       
    }
}
