using Dapper;
using System.Data.SqlClient;

namespace HDProjectWeb.Models.Helps
{
    public class Usuario
    {
        public string Codigo { get; set; }
        public string Descri { get; set; }
    }
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> ListaAyudaUsuario();
    }
    public class UsuarioService : IUsuarioService
    {
        private readonly string connectionString;

        public UsuarioService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<Usuario>> ListaAyudaUsuario()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Usuario>(@"Select uap_coduap as codigo, uap_descor as descri 
                          From REQ_USERS_APROBADORES_UAP
                          Where cia_codcia=1 ORDER BY UAP_DESLAR");
        }
    }

}
