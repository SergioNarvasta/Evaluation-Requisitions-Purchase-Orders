using Dapper;
using HDProjectWeb.Services;
using Microsoft.Data.SqlClient;

namespace HDProjectWeb.Services
{
    public interface IServicioPeriodo
    {
        Task ActualizaOrden(string orden);
        Task ActualizaPeriodo(string periodo);
        string Ano();
        string Compañia();
        string Mes();
        string NroReq();
        Task<string> ObtenerCompañia(string codcia);
        Task<string> ObtenerOrden();
        Task<string> ObtenerPeriodo();
        Task SetOrden();
        Task SetPeriodo();
        string Sucursal();
    }  
    public class ServicioPeriodo :IServicioPeriodo
    {
        private readonly string connectionString;
        private readonly IServicioUsuario servicioUsuario;
        public ServicioPeriodo(IConfiguration configuration,IServicioUsuario servicioUsuario)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            this.servicioUsuario = servicioUsuario;
        }
        public async Task SetPeriodo()
        {
            string codUser = servicioUsuario.ObtenerCodUsuario();
            int mes = DateTime.Now.Month;
            int ano = DateTime.Now.Year;
            string periodo = ano.ToString() + mes.ToString();
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE AspNetUsers SET ActivePeriod = @periodo 
                     WHERE Email = @CodUser", new { codUser,periodo });
        }
        public async Task<string> ObtenerPeriodo()
        {
            string codUser = servicioUsuario.ObtenerCodUsuario();
            using var connection = new SqlConnection(connectionString);
            var periodo = await connection.QuerySingleAsync<string>(@"SELECT ActivePeriod 
                         FROM AspNetUsers WHERE Email = @codUser", new {codUser });
            if (periodo is null)
            {
                await SetPeriodo();
                return periodo = await connection.QuerySingleAsync<string>(@"SELECT ActivePeriod 
                         FROM AspNetUsers WHERE Email = @CodUser", new { codUser });
            }else
            return periodo;
        }
        public async Task ActualizaPeriodo(string periodo)
        {
            string codUser = servicioUsuario.ObtenerCodUsuario();
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE AspNetUsers SET ActivePeriod = @periodo 
                     WHERE Email = @CodUser", new { codUser, periodo });
        }
        public async Task SetOrden()
        {
            string codUser = servicioUsuario.ObtenerCodUsuario();
            string orden = "1";
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE AspNetUsers SET ActiveOrder = @orden 
                     WHERE Email = @CodUser", new { codUser, orden });
        }
        public async Task<string> ObtenerOrden()
        {
            string codUser = servicioUsuario.ObtenerCodUsuario();
            using var connection = new SqlConnection(connectionString);
            var orden = await connection.QuerySingleAsync<string>(@"SELECT ActiveOrder 
                         FROM AspNetUsers WHERE Email = @codUser", new { codUser });
            if (orden is null)
            {
                await SetOrden();
                return orden = await connection.QuerySingleAsync<string>(@"SELECT ActiveOrder 
                         FROM AspNetUsers WHERE Email = @CodUser", new { codUser });
            }
            else
                return orden;
        }
        public async Task ActualizaOrden(string orden)
        {
            string codUser = servicioUsuario.ObtenerCodUsuario();
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE AspNetUsers SET ActiveOrder = @orden 
                     WHERE Email = @CodUser", new { codUser, orden });
        }
        public string Compañia()
        {
            string cia = "01";
            return cia; 
        }
        public async Task<string> ObtenerCompañia(string codcia)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleAsync<string>(@"SELECT CIA_NOMCIA 
                   FROM COMPANIA_CIA WHERE CIA_CODCIA = @codcia",new {codcia});
        }
        public string Sucursal()
        {
            string suc = "01";
            return suc;
        }
        public string Mes()
        {
            string mes =  DateTime.Now.Month.ToString();
            return mes;
        }
        public string Ano()
        {
            string ano = DateTime.Now.Year.ToString();
            return ano;
        }
        public string NroReq()
        {
            Random rnd = new Random();
            string _base = "RQ";
            string Random = rnd.Next().ToString();
            string NroReq = (_base+ Random).Remove(10,(_base + Random).Length-10);
            return NroReq;
        }
    }
}
