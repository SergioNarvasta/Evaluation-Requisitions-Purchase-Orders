using Dapper;
using System.Data.SqlClient;

namespace HDProjectWeb.Models.Helps
{
    //Lista para mostrar los UNegocio en Combo (View Crear)
    public class UNegocio
    {
        public string Codigo { get; set; }
        public string Descri { get; set; }
    }

    public interface IUNegocioService
    {
        Task<IEnumerable<UNegocio>> ListaUNegocio();
    }
    public class UNegocioService : IUNegocioService
    {
        private readonly string connectionString;      

        public UNegocioService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");         
        }
        public async Task<IEnumerable<UNegocio>> ListaUNegocio()
        {          
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<UNegocio>(@"SELECT ung_codepk as codigo,ung_deslar as descri 
                     FROM UNID_NEGOCIO_UNG WHERE cia_codcia=1 and ung_estado=1");
        }
    }
}
