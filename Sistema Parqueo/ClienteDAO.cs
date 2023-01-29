using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Parqueo
{
    public class ClienteDAO
    {
        SqlConnection ObjSqlConnection;

        public Boolean insertarRegistro(Cliente ObjCliente)
        {
            try
            {
                ObjSqlConnection = AdministradorDeConexion.getConexion();
                ObjSqlConnection.Open();
                String fechaingreso= DateTime.Now.ToString("dd/MM/yyyy");
                String HoraIngreso = DateTime.Now.ToString("hh:mm:ss");
                //DateTime HoraIngreso = DateTime.Today;
              // HoraIngreso = ObjCliente.fechClie;
                String sentencia = "INSERT INTO Cliente(nombClie,dniClie,telfClie,placClie,modeClie,coloClie,fechClie,horaClie) values(' " +
                                //ObjCliente.codiClie + "','" +
                                ObjCliente.nombClie + "','" +
                                ObjCliente.dniClie + "','" +
                                ObjCliente.telfClie + "','" +
                                ObjCliente.placClie + "','" +
                                ObjCliente.modeClie + "','" +
                                ObjCliente.coloClie + "','" +
                                 fechaingreso + "','" +
                                 HoraIngreso + "') ";

                SqlCommand ObjSqlComand = new SqlCommand(sentencia, ObjSqlConnection);
                ObjSqlComand.ExecuteNonQuery();
                ObjSqlConnection.Close();
                return true;
            }
            catch (System.Exception e)
            {
                ObjSqlConnection.Close();
                MessageBox.Show("Error ...!!!" + e.Message);
                return false;
            }
        }
        public Cliente consultarRegistro(int dni)
        {
            try
            {
                Cliente ObjCliente;
                ObjSqlConnection = AdministradorDeConexion.getConexion();
                ObjSqlConnection.Open();
                String sentencia = "SELECT * FROM Cliente WHERE dniClie =" + dni;
                SqlCommand ObjSqlCommand = new SqlCommand(sentencia, ObjSqlConnection);
                SqlDataReader ObjSqlDataReader = ObjSqlCommand.ExecuteReader();
                if (ObjSqlDataReader.Read())
                {
                    ObjCliente = new Cliente();
                    ObjCliente.codiClie = (int)ObjSqlDataReader["codiClie"];
                    ObjCliente.nombClie = (String)ObjSqlDataReader["nombClie"];
                    ObjCliente.dniClie =      (int)ObjSqlDataReader["dniClie"];
                    ObjCliente.telfClie = (String)ObjSqlDataReader["telfClie"];
                    ObjCliente.placClie = (String)ObjSqlDataReader["placClie"];
                    ObjCliente.modeClie = (String)ObjSqlDataReader["modeClie"];
                    ObjCliente.coloClie = (String)ObjSqlDataReader["coloClie"];
                    ObjCliente.fechClie = (String)ObjSqlDataReader["fechClie"];
                    ObjCliente.horaClie = (String)ObjSqlDataReader["horaClie"];
                    ObjSqlDataReader.Close();
                    return ObjCliente;
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show("Error ...!!!" + e.Message);
            }
            return null;
        }
        /////////////////////////////////////
          public Boolean modificarRegistro(int busqueda, Cliente ObjCliente)
          {
              try
              {
                  SqlConnection ObjSqlConnection = AdministradorDeConexion.getConexion();
                  ObjSqlConnection.Open();
                  string sentencia = "UPDATE Cliente SET nombClie ='" + ObjCliente.nombClie + "'," +
                                                         "dniClie ='" + ObjCliente.dniClie + "'," +
                                                         "telfClie ='" + ObjCliente.telfClie + "'," +
                                                         "placClie ='" + ObjCliente.placClie + "'," +
                                                         "modeClie ='" + ObjCliente.modeClie + "'," +
                                                         "coloClie = '" + ObjCliente.coloClie  +"'"+
                                                          //"fechClie = " + ObjCliente.fechClie+
                                                         //"horaClie = "+ObjCliente.horaClie+
                                                         " WHERE dniClie =" + busqueda;
                  SqlCommand ObjSqlCommand = new SqlCommand(sentencia, ObjSqlConnection);
                  ObjSqlCommand.ExecuteNonQuery();
                  ObjSqlConnection.Close();
                  return true;

              }
              catch (System.Exception e)
              {
                  ObjSqlConnection.Close();
                  MessageBox.Show("Error ...!!!" + e.Message);
                  return false;
              }
          }

       /* public Boolean modificarRegistro(int busqueda, Cliente oCliente)
        {
            try
            {
                oSqlConnection = AdministradorDeConexion.getConexion();
                oSqlConnection.Open();
                SqlCommand oSqlCommand = new SqlCommand("dbo.ModificarCliente", oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add(new SqlParameter("@codiClie", oCliente.codiClie));
                oSqlCommand.Parameters.Add(new SqlParameter("@nombClie", oCliente.nombClie));
                oSqlCommand.Parameters.Add(new SqlParameter("@dniClie", oCliente.dniClie));
                oSqlCommand.Parameters.Add(new SqlParameter("@telfClie", oCliente.telfClie));
                oSqlCommand.Parameters.Add(new SqlParameter("@placClie", oCliente.placClie));
                oSqlCommand.Parameters.Add(new SqlParameter("@modeClie", oCliente.modeClie));
                oSqlCommand.Parameters.Add(new SqlParameter("@coloClie", oCliente.coloClie));
                oSqlCommand.Parameters.Add(new SqlParameter("@fechClie", oCliente.fechClie));
                oSqlCommand.Parameters.Add(new SqlParameter("@horaClie", oCliente.horaClie));
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
        }*/
        ////////////////////////////

        public int buscarRegistro(int busqueda)
        {
            Cliente ObjCLiente = new Cliente();

            try
            {
                SqlConnection oSqlConnection = AdministradorDeConexion.getConexion();
                oSqlConnection.Open();

                String sentencia = "SELECT * FROM Cliente WHERE dniClie=" + busqueda;
                SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);

                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();

                if (oSqlDataReader.Read())
                {
                   ObjCLiente.dniClie = (int)oSqlDataReader["dniClie"];
                    oSqlDataReader.Close();
                    oSqlConnection.Close();
                 return ObjCLiente.dniClie;
                }
                else
                {
                    return -99;
                }

            }
            catch (System.Exception e)
            {
                ObjSqlConnection.Close();
                MessageBox.Show("Error ...!!!" + e.Message);
                return -99;
            }
        }
        public Boolean eliminarRegistro(int busqueda)
        {
            try
            {
                SqlConnection ObjSqlConnection = AdministradorDeConexion.getConexion();
                ObjSqlConnection.Open();
                string sentencia = "DELETE FROM Cliente WHERE dniClie =" + busqueda;
                SqlCommand ObjSqlCommand = new SqlCommand(sentencia, ObjSqlConnection);
                ObjSqlCommand.ExecuteNonQuery();
                ObjSqlConnection.Close();
                return true;
            }
            catch (System.Exception e)
            {
                ObjSqlConnection.Close();
                MessageBox.Show("Error ...!!!" + e.Message);
                return false;
            }
        }
        public List<Cliente> obtenerDatosEnList()
        {
            List<Cliente> ObjListCliente = new List<Cliente>();
            try
            {
                ObjSqlConnection = AdministradorDeConexion.getConexion();
                ObjSqlConnection.Open();
                String sentencia = "SELECT * FROM Cliente";
                SqlCommand ObjSqlCommand = new SqlCommand(sentencia, ObjSqlConnection);
                SqlDataReader ObjSqlDataReader = ObjSqlCommand.ExecuteReader();
                Cliente ObjCliente;
                while (ObjSqlDataReader.Read())
                {
                    ObjCliente = new Cliente();

                    ObjCliente.codiClie = (int)ObjSqlDataReader["codiClie"];
                    ObjCliente.nombClie = (String)ObjSqlDataReader["nombClie"];
                    ObjCliente.dniClie = (int)ObjSqlDataReader["dniClie"];
                    ObjCliente.telfClie = (String)ObjSqlDataReader["telfClie"];
                    ObjCliente.placClie = (String)ObjSqlDataReader["placClie"];
                    ObjCliente.modeClie = (String)ObjSqlDataReader["modeClie"];
                    ObjCliente.coloClie = (String)ObjSqlDataReader["coloClie"];
                    ObjCliente.fechClie = (String)ObjSqlDataReader["fechClie"];
                    ObjCliente.horaClie = (String)ObjSqlDataReader["horaClie"];
                    ObjListCliente.Add(ObjCliente);
                }
                ObjSqlDataReader.Close();
                ObjSqlConnection.Close();
                return ObjListCliente;
            }
            catch (System.Exception e)
            {
                ObjSqlConnection.Close();
                MessageBox.Show("Error ...!!!" + e.Message);
                return null;
            }
        }
    }
}
