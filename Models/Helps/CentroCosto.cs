using Dapper;
using HDProjectWeb.Services;
using System.Data.SqlClient;

namespace HDProjectWeb.Models.Helps
{
    public class CentroCosto
    {
        public string Epk { get; set; }
        public string Codigo { get; set; }
        public string Descri { get; set; }
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
            return await connection.QueryAsync<CentroCosto>(@"SELECT pry_codepk as Epk,pry_codpry as Codigo,pry_deslar as Descri  
                          FROM PROYECTOS_PRY 
                          WHERE CIA_CODCIA =1 AND pry_estado=1");
        }
    }
}
