using Dapper;
using HDProjectWeb.Models;
using HDProjectWeb.Models.Detalles;
using Microsoft.Data.SqlClient;

namespace HDProjectWeb.Services
{
    public interface IRepositorioRQCompra
    {
        //Interface Obtener para la clase diseñada de vista RQCompra
        Task<RQCompra> ObtenerporCodigo(string Rco_numero);
        Task Actualizar(RQCompra rQCompraEd);
        Task Crear(RQCompra rQCompra);
        Task<IEnumerable<RQCompraCab>> Obtener(string periodo, PaginacionViewModel paginacion, int EpkUser, string orden, string estado1, string estado2);
        Task<int> ContarRegistros(string periodo, int EpkUser, string estado1, string estado2);  
        Task<int> ContarRegistrosBusqueda(string periodo, int EpkUser, string busqueda, string estado1, string estado2);
        Task<IEnumerable<RQCompraCab>> BusquedaMultiple(string periodo, PaginacionViewModel paginacion, int EpkUser, string busqueda, string estado1, string estado2);
        Task<RQCompra> ObtenerporEpk(int Rco_codepk);
        Task<int> AprobarReq(int cia, int suc, int epk, int uap);
        Task<int> RechazaReq(int cia, int suc, int epk, int uap, string mot);
        Task<int> DevuelveReq(int cia, int suc, int epk, int uap);
        Task<string> ObtenerMaxRCO();
    }
    public class RepositorioRQCompra:IRepositorioRQCompra
    {
        private readonly string connectionString;
        private readonly IServicioEstandar servicioEstandar;

        public RepositorioRQCompra(IConfiguration configuration,IServicioEstandar servicioEstandar)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            this.servicioEstandar = servicioEstandar;
        }
        public async Task Crear(RQCompra rQCompra)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync (@" PA_WEB_ReqCompra_Inserta @cia_codcia = @cia_codcia ,@suc_codsuc = @suc_codsuc,@rco_codepk = @rco_codepk,
                 @rco_numrco = @Rco_numero ,@tin_codtin = @tin_codtin,@rco_motivo = @rco_motivo,@rco_glorco = @rco_glorco,
                 @cco_codepk = @cco_codepk, @rco_sitrco = @rco_sitrco, @rco_codusu = @rco_codusu,@ung_codepk = @ung_codepk, @rco_indval = @rco_indval,
                 @rco_indest =  @rco_indest, @rco_rembls = @rco_rembls, @rco_presup = @rco_presup,@rco_priori = @rco_priori, @tre_codepk = @tre_codepk,
                 @rco_estado = @rco_estado, @dis_codepk = @dis_codepk, @s10_codepk  = @s10_codepk, @occ_codepk = @occ_codepk,
                 @rcd_corite = @DPrd_item,  @prd_codepk = @DPrd_codigo, @rcd_desprd = @DPrd_descri,@rcd_glorcd = @DPrd_glosa ,@rcd_canate=@DPrd_cantidad,
                 @ccr_codepk = @DPrd_codprov,@ume_codepk =@DPrd_unidad  ", rQCompra);
        }
        public async Task<IEnumerable<RQCompraCab>> Obtener(string periodo,PaginacionViewModel paginacion,int EpkUser,string orden,string estado1,string estado2) 
        {
            using var connection = new SqlConnection(connectionString);
            return orden switch
            {
                "1" => await connection.QueryAsync<RQCompraCab>(@$" SELECT*FROM V_WEB_REQCOMPRAS_Index
                Where cia=1 AND suc=1 AND periodo =@periodo AND  uap_codepk = @EpkUser  AND estado in(@estado1,@estado2)
                ORDER BY Rco_Numero DESC 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo, EpkUser ,estado1, estado2 }),
                
                _   => await connection.QueryAsync<RQCompraCab>(@$"SELECT*FROM V_WEB_REQCOMPRAS_Index
                Where cia=1 AND suc=1 AND periodo =@periodo AND uap_codepk = @EpkUser  AND estado in(@estado1,@estado2)
                ORDER BY Rco_Numero DESC 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo, EpkUser ,estado1, estado2 }),
            };
        }
        public async Task<IEnumerable<RQCompraCab>> BusquedaMultiple(string periodo, PaginacionViewModel paginacion, int EpkUser, string busqueda,string estado1,string estado2)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<RQCompraCab>(@$"SELECT * FROM V_WEB_REQCOMPRAS_Index
             Where cia=1 AND suc=1 AND periodo =@periodo AND uap_codepk = @EpkUser  AND estado in(@estado1,@estado2)
             AND Rco_Numero LIKE '%'+@busqueda+'%' OR User_Solicita LIKE '%'+@busqueda+'%' OR U_Negocio LIKE '%'+@busqueda+'%' OR Centro_Costo LIKE '%'+@busqueda+'%' 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo, EpkUser, busqueda, estado1, estado2 });         
        }  
        public async Task<int> ContarRegistros(string periodo, int EpkUser, string estado1, string estado2)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.ExecuteScalarAsync<int>(@"SELECT COUNT(*) FROM V_WEB_REQCOMPRAS_Index
                Where cia=1 AND suc=1 AND periodo =@periodo AND uap_codepk = @EpkUser   AND estado in(@estado1,@estado2)", 
                new { periodo,EpkUser, estado1, estado2 });
        }
        public async Task<int> ContarRegistrosBusqueda(string periodo, int EpkUser, string busqueda, string estado1,string estado2)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.ExecuteScalarAsync<int>(@"SELECT COUNT(*) FROM V_WEB_REQCOMPRAS_Index
             Where cia=1 AND suc=1 AND periodo =@periodo AND uap_codepk = @EpkUser  AND estado in(@estado1,@estado2)
             AND Rco_Numero LIKE '%'+@busqueda+'%' OR User_Solicita LIKE '%'+@busqueda+'%' OR U_Negocio LIKE '%'+@busqueda+'%' OR Centro_Costo LIKE '%'+@busqueda+'%'",
                new { periodo, EpkUser, busqueda, estado1,estado2 });
        }
        public async Task Actualizar(RQCompra rQCompraEd)            
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"PA_HD_WEB_RQ_RQCompraCab_Update @Rco_Numero=@Rco_numero ,@Rco_Fec_Registro=@Rco_fec_registro ,@Rco_Motivo =@Rco_motivo,
                            @U_Negocio =@U_negocio, @Centro_Costo=Centro_costo, @Rco_Situacion_Aprobado=@Rco_situacion_aprobado,
                            @Rco_Prioridad =@Rco_prioridad,  @Rco_Justificacion=Rco_justificacion , @Rco_Reembolso =@Rco_reembolso,
                            @Rco_Presupuesto =@Rco_presupuesto,@Rco_Categorizado = @Rco_categorizado,  @Rco_Disciplina = @Rco_disciplina", rQCompraEd);
        }
        public async Task<RQCompra> ObtenerporCodigo(string Rco_numero) 
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<RQCompra>(@" SELECT * FROM V_WEB_REQCOMPRAS_Index
                  WHERE Rco_Numero = @Rco_numero ", new {Rco_numero});
        }
        public async Task<RQCompra> ObtenerporEpk(int Rco_codepk)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<RQCompra>(@" SELECT * FROM V_WEB_REQCOMPRAS_Index
                  WHERE rco_codepk = @Rco_codepk ", new { Rco_codepk });
        }
        public async Task<string> ObtenerMaxRCO()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleAsync<string>(@"SELECT TOP 1 A.rco_numrco 
                            FROM REQ_REQUI_COMPRA_RCO A ORDER BY A.rco_numrco DESC");
        }
        public async Task<int> AprobarReq(int cia,int suc,int epk,int uap)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleAsync<int>(@" PA_WEB_RQ_Aprueba
                 @p_CodCia = @cia, @p_CodSuc = @suc, @p_NumRQ=@epk, @p_CodUsr=@uap ", new { cia, suc, epk, uap });
        }
        public async Task<int> RechazaReq(int cia, int suc, int epk, int uap, string mot)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleAsync<int>(@" PA_WEB_RQ_Rechaza
                 @p_CodCia = @cia, @p_CodSuc = @suc, @p_NumRQ =@epk, @p_CodUsr=@uap, @p_Motivo = @mot", new { cia, suc, epk, uap, mot });
        }
        public async Task<int> DevuelveReq(int cia, int suc, int epk, int uap)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleAsync<int>(@" PA_WEB_RQ_Devuelve
                 @p_CodCia = @cia, @p_CodSuc = @suc, @p_NumRQ=@epk, @p_CodUsr=@uap ", new { cia, suc, epk, uap });
        }



    }
}
