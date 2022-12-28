using Dapper;
using HDProjectWeb.Services;
using System.Data.SqlClient;

namespace HDProjectWeb.Models.Helps
{
    public class CentroCosto
    {
        public string Cia_codcia { get; set; }
        public string Cco_codcco { get; set; }
        public string Cco_deslar { get; set; }
    }

    public interface ICentroCostoService
    {
        Task<IEnumerable<CentroCosto>> ListaAyudaCentroCosto();
    }
    public class CentroCostoService : ICentroCostoService
    {
        private readonly string connectionString;      

        public CentroCostoService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");         
        }
        public async Task<IEnumerable<CentroCosto>> ListaAyudaCentroCosto()
        {          
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<CentroCosto>(@"SELECT CIA_CODCIA,CCO_CODCCO,CCO_DESLAR FROM CENTRO_COSTO_CCO 
                                                             WHERE CIA_CODCIA =1 AND CCO_INDEST=1 AND CCO_INDCOS=0");
        }
    }
}
