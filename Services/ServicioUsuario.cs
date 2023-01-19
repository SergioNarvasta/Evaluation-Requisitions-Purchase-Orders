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
            int epk = await connection.QuerySingleAsync<int>(@"SELECT COUNT(*) FROM REQ_USERS_APROBADORES_UAP A                                
                                     WHERE A.uap_codemp = @CodUser OR A.uap_deslar= @CodUser OR A.uap_nombre =@CodUser ",
                                     new { CodUser });
            if(epk == 0)
            {
                return -1;
            }else
                return await connection.QuerySingleAsync<int>(@"SELECT A.UAP_CODEPK FROM REQ_USERS_APROBADORES_UAP A                                
                                     WHERE A.uap_codemp = @CodUser OR A.uap_deslar= @CodUser OR A.uap_nombre =@CodUser ",
                                     new { CodUser });
        }
        public async Task<int> RegistraUsuario_S10()
        {
            string nom; int cia = 1; int suc=1;
            nom = ObtenerCodUsuario(); 
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleAsync<int>(@"INSERT INTO REQ_USERS_APROBADORES_UAP(cia_codcia,suc_codsuc,uap_deslar,uap_nombre)VALUES(@cia,@suc,@nom,@nom);
                          SELECT SCOPE_IDENTITY()", new { cia,suc,nom });
        }
        public async Task<string> ObtenerNombreUsuario(string CodUser)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleAsync<string>(@"SELECT A.UAP_DESLAR FROM REQ_USERS_APROBADORES_UAP A                                
                                     WHERE A.uap_codemp = @CodUser OR A.uap_deslar= @CodUser OR A.uap_nombre =@CodUser ", new { CodUser });
        }
    }
}
