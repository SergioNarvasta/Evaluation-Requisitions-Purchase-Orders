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
            return await connection.QueryAsync<Usuario>(@"SELECT DISTINCT AUX_CODAUX as Codigo,S10_NOMUSU as Descri 
                                                   FROM SYS_TABLA_USUARIOS_S10 WHERE S10_INDEST=1 AND LEN(AUX_CODAUX)>1 ORDER BY S10_NOMUSU");
        }
    }

}
