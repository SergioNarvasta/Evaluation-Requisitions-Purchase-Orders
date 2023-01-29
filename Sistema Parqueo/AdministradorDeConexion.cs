using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Parqueo
{
    public class AdministradorDeConexion
    {
        static SqlConnection conexion;
        static public SqlConnection getConexion()
        {
             conexion = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbparqueo;Integrated Security=True");
            return conexion;
        }
        //Config Connection
    }
}
