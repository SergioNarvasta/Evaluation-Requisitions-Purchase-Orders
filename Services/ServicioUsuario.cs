using Dapper;
using System.Data.SqlClient;
using System.Security.Claims;

namespace HDProjectWeb.Services
{
    public interface IServicioUsuario
    {
        string ObtenerCodAuxUsuario(string CodUser);
        string ObtenerCodUsuario();
        string ObtenerNombreUsuario(string Codaux);
    }
    public class ServicioUsuario : IServicioUsuario
    {
        private readonly HttpContext httpContext;
        private readonly string connectionString;

        public ServicioUsuario(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            httpContext = httpContextAccessor.HttpContext;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public string ObtenerCodUsuario()
        {
            if(httpContext.User.Identity.IsAuthenticated)
            {
                var idClaim = httpContext.User
                             .Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault();
                var Cod = idClaim.Value.ToString();
                return Cod;
            }
            else
            {
                throw new ApplicationException("El usuario no esta autenticado");
            }
        }
        public string ObtenerCodAuxUsuario( string CodUser)
        {
            using var connection = new SqlConnection(connectionString);
            return connection.QuerySingle<string>(@"SELECT AUX_CODAUX FROM SYS_TABLA_USUARIOS_S10 
                          WHERE S10_USUARIO = @CodUser", new { CodUser });
        }
        public string ObtenerNombreUsuario(string Codaux)
        {
            using var connection = new SqlConnection(connectionString);
            return connection.QuerySingle<string>(@"SELECT TOP 1 S10_NOMUSU FROM SYS_TABLA_USUARIOS_S10 
            WHERE AUX_CODAUX=@codaux", new { Codaux });
        }
    }
}
