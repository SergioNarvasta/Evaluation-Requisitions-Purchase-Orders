using Dapper;
using System.Data.SqlClient;

namespace HDProjectWeb.Models.Helps
{
    //Lista para mostrar los TipoReq en Combo (View Crear)
    public class TipoReq
    {
        public string Codigo { get; set; }
        public string Descri { get; set; }
    }

    public interface ITipoReqService
    {
        Task<IEnumerable<TipoReq>> ListaTipoReq();
    }
    public class TipoReqService : ITipoReqService
    {
        private readonly string connectionString;      

        public TipoReqService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");         
        }
        public async Task<IEnumerable<TipoReq>> ListaTipoReq()
        {          
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoReq>(@"SELECT tre_codepk as codigo, 
                        tre_deslar as descri 
                        FROM REQ_TIPO_REQUISICION_TRE WHERE cia_codcia=1 and tre_estado=1");
        }
    }
}
