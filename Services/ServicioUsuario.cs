using Dapper;
using System.Data.SqlClient;
using System.Security.Claims;

namespace HDProjectWeb.Services
{
    public interface IServicioUsuario
    {
        string ObtenerCodUsuario();
        int ObtenerEpkUsuario(string CodUser);
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
        public int ObtenerEpkUsuario(string CodUser)
        {
            using var connection = new SqlConnection(connectionString);
            return connection.QuerySingle<int>(@"SELECT S10_CODEPK FROM SYS_TABLA_USUARIOS_S10 A 
                                     LEFT JOIN AspNetUsers B ON A.S10_USUARIO = B.UserName 
                                     WHERE B.UserName = @CodUser " ,new { CodUser });
        }
        public string ObtenerNombreUsuario(string CodUser)
        {
            using var connection = new SqlConnection(connectionString);
            return connection.QuerySingle<string>(@"SELECT TOP 1 S10_NOMUSU FROM SYS_TABLA_USUARIOS_S10 
            WHERE S10_USUARIO = @coduser", new { CodUser });
        }
    }
}
