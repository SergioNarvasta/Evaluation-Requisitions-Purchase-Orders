using Dapper;
using HDProjectWeb.Models;
using ProjectWeb_DRA.Models;
using System.Data.SqlClient;

namespace ProjectWeb_DRA.Services
{
    public interface IRepositorioOrdenCompra
    {
        Task<int> ContarRegistrosOCC(string periodo);
        Task<IEnumerable<OrdenCompra>> Obtener(string periodo, PaginacionViewModel paginacion);
    }
    public class RepositorioOrdenCompra :IRepositorioOrdenCompra
    {
        private readonly string connectionString;
        public RepositorioOrdenCompra(IConfiguration configuration) 
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<OrdenCompra>> Obtener(string periodo, PaginacionViewModel paginacion)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<OrdenCompra>(@$" SELECT A.occ_numero,A.occ_feccre,A.occ_tcaocc,B.ccr_codccr,B.ccr_nomaux,A.occ_observ 
                FROM OCOMPRA_OCC A
                LEFT JOIN CUEN_CORR_CCR B ON A.cia_codcia=B.cia_codcia AND A.ccr_codepk=B.ccr_codepk
                WHERE Cast(YEAR(A.occ_feccre) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.occ_feccre)as char(2))),len(ltrim(Cast(MONTH(A.occ_feccre)as char(2)))),2) =@periodo
                AND A.cia_codcia = 1 AND A.suc_codsuc = 1 
                ORDER BY A.occ_feccre DESC 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo });
            
        }
        public async Task<int> ContarRegistrosOCC(string periodo)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.ExecuteScalarAsync<int>(@"SELECT COUNT(*) 
                FROM OCOMPRA_OCC A
                LEFT JOIN CUEN_CORR_CCR B ON A.cia_codcia=B.cia_codcia AND A.ccr_codepk=B.ccr_codepk
                WHERE Cast(YEAR(A.occ_feccre) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.occ_feccre)as char(2))),len(ltrim(Cast(MONTH(A.occ_feccre)as char(2)))),2) =@periodo
                AND A.cia_codcia = 1 AND A.suc_codsuc = 1 ",
                new { periodo });
        }
    }
}
