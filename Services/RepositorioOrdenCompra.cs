using Dapper;
using HDProjectWeb.Models;
using ProjectWeb_DRA.Models;
using System.Data.SqlClient;

namespace ProjectWeb_DRA.Services
{
    public interface IRepositorioOrdenCompra
    {
        Task<int> AprobarOC(string cia, string suc, string occ, string usu);
        Task<int> ContarRegistrosOCC(string periodo);
        Task<int> DevuelveOC(string cia, string suc, string occ, string usu);
        Task<IEnumerable<OrdenCompra>> Obtener(string periodo, PaginacionViewModel paginacion);
        Task<OrdenCompra> ObtenerporCodigoOCC(string Occ_numero);
        Task<int> RechazaOC(string cia, string suc, string occ, string usu);
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
            return await connection.QueryAsync<OrdenCompra>(@$" SELECT Convert(varbinary,A.occ_numero)as occ_encryp, A.occ_codepk, A.occ_numero,A.occ_feccre,A.occ_tcaocc,B.ccr_codccr,B.ccr_nomaux,A.occ_observ 
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
        public async Task<OrdenCompra> ObtenerporCodigoOCC(string Occ_numero)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<OrdenCompra>(@" SELECT A.occ_codepk, A.occ_numero,A.occ_feccre,A.occ_tcaocc,B.ccr_codccr,B.ccr_nomaux,A.occ_observ,A.occ_impigv,A.tco_codtco,C.tco_nombre,
                    A.occ_estado,iif(A.occ_estado=1,'APROBADO','PENDIENTE')as occ_destado,A.mon_codepk,D.mon_desmon,A.cpg_codepk,E.cpg_deslar,
	                A.occ_fecemi,A.occ_pordet,A.occ_impdet,A.imp_codepk,F.imp_desimp
               FROM OCOMPRA_OCC A
               LEFT JOIN CUEN_CORR_CCR   B ON A.cia_codcia=B.cia_codcia AND A.ccr_codepk=B.ccr_codepk
               LEFT JOIN TIPO_COMPRA_TCO C ON A.cia_codcia=C.cia_codcia AND A.tco_codtco=C.tco_codtco
               LEFT JOIN MONEDA_MON      D ON A.mon_codepk=D.mon_codepk
               LEFT JOIN COND_PAGO_CPG   E ON A.cia_codcia=E.cia_codcia AND A.cpg_codepk=E.cpg_codepk
               LEFT JOIN IMPUESTOS_IMP   F ON A.imp_codepk=F.imp_codepk
               WHERE A.occ_numero = @Occ_numero ", new { Occ_numero});
        }

        public async Task<int> AprobarOC(string cia, string suc, string occ, string usu)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleAsync<int>(@" PA_WEB_OC_Aprueba
                 @p_CodCia = @cia, @p_CodSuc = @suc, @p_NumOC =@occ, @p_CodUsr=@usu ", new { cia, suc, occ, usu });
        }
        public async Task<int> RechazaOC(string cia, string suc, string occ, string usu)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleAsync<int>(@" PA_HD_WEB_OC_Rechaza
                 @p_CodCia = @cia, @p_CodSuc = @suc, @p_NumOC =@occ, @p_CodUsr=@usu ", new { cia, suc, occ, usu });
        }
        public async Task<int> DevuelveOC(string cia, string suc, string occ, string usu)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleAsync<int>(@" PA_HD_WEB_OC_Devuelve
                 @p_CodCia = @cia, @p_CodSuc = @suc, @p_NumOC =@occ, @p_CodUsr=@usu ", new { cia, suc, occ, usu });
        }

    }
}
