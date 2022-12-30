using Dapper;
using System.Data.SqlClient;
using System.Security.Claims;

namespace HDProjectWeb.Services
{
    public interface IServicioUsuario
    {
        string ObtenerCodUsuario();
        Task<int> ObtenerEpkUsuario(string CodUser);
        Task<string> ObtenerNombreUsuario(string CodUser);
        Task<int> RegistraUsuario_S10();
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
        public  async Task<int> ObtenerEpkUsuario(string CodUser)
        {
            using var connection = new SqlConnection(connectionString);
            int cant = await connection.QuerySingleAsync<int>(@"SELECT COUNT(*) FROM SYS_TABLA_USUARIOS_S10 A 
                                     LEFT JOIN AspNetUsers B ON A.S10_USUARIO = B.UserName 
                                     WHERE B.UserName = @CodUser ", new { CodUser });
            if(cant == 0)
            {
                return await RegistraUsuario_S10();
            }else
                return await connection.QuerySingleAsync<int>(@"SELECT S10_CODEPK  FROM SYS_TABLA_USUARIOS_S10 A 
                                     LEFT JOIN AspNetUsers B ON A.S10_USUARIO = B.UserName 
                                     WHERE B.UserName = @CodUser ", new { CodUser });
        }
        public async Task<int> RegistraUsuario_S10()
        {
            string usu, nom, nomcor, psd; int nivusu = 1;
            usu = ObtenerCodUsuario(); nom = ObtenerCodUsuario(); nomcor= ObtenerCodUsuario(); psd = "asd123";
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleAsync<int>(@"INSERT INTO SYS_TABLA_USUARIOS_S10(S10_USUARIO,S10_NOMUSU,S10_NOMCOR,S10_NIVUSU,S10_PASSWO) VALUES(@usu,@nom,@nomcor,@nivusu,@psd);
                          SELECT SCOPE_IDENTITY()" ,new { usu,nom,nomcor, psd,nivusu });
        }
        public async Task<string> ObtenerNombreUsuario(string CodUser)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleAsync<string>(@"SELECT TOP 1 S10_NOMUSU FROM SYS_TABLA_USUARIOS_S10 
            WHERE S10_USUARIO = @coduser", new { CodUser });
        }
    }
}
