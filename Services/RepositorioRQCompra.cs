using Dapper;
using HDProjectWeb.Models;
using HDProjectWeb.Models.Detalles;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Data.SqlClient;

namespace HDProjectWeb.Services
{
    public interface IRepositorioRQCompra
    {
        //Interface Obtener para la clase diseñada de vista RQCompra
        Task<RQCompra> ObtenerporCodigo(string Rco_numero);
        Task Actualizar(RQCompra rQCompraEd);
        Task Crear(RQCompra rQCompra);
        Task<IEnumerable<RQCompraCab>> Obtener(string periodo, PaginacionViewModel paginacion, string CodAuxUser,string orden, string estado1, string estado2);
        Task<int> ContarRegistros(string periodo, string CodUser, string estado1, string estado2);  
        Task<int> ContarRegistrosBusqueda(string periodo, string CodUser, string busqueda, string estado1, string estado2);
        Task<IEnumerable<RQCompraCab>> BusquedaMultiple(string periodo, PaginacionViewModel paginacion, string CodUser, string busqueda, string estado1, string estado2);
 
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
            await connection.ExecuteAsync (@" PA_HD_WEB_RQ_Registra_Cabecera @cia_codcia = @cia_codcia, @suc_codsuc = @suc_codsuc, @rco_numrco = @rco_numrco, @tin_codtin = @tin_codtin
                                        ,@s10_usuario = @s10_usuario, @rco_motivo = @rco_motivo, @cco_codcco = @cco_codcco,@rco_sitrco = @rco_sitrco, @rco_sitlog = @rco_sitlog
                                        ,@ano_codano = @ano_codano, @mes_codmes = @mes_codmes, @rco_indest = @rco_indest, @rco_usucre = @rco_usucre , @rco_tiprco = @rco_tiprco
                                        ,@rco_codusu = @rco_codusu, @ung_codung = @ung_codung, @rco_indcie = @rco_indcie, @rco_indval = @rco_indval , @rco_priori = @rco_priori
                                        ,@rco_rembls = @rco_rembls, @rco_presup = @rco_presup, @adi_codadi = @adi_codadi",rQCompra);
        }
        public async Task RegistraDetalle(DetalleReq detalleReq)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@" ", detalleReq);
        }
        public async Task<IEnumerable<RQCompraCab>> Obtener(string periodo,PaginacionViewModel paginacion,string CodUser,string orden,string estado1,string estado2) 
        {
            using var connection = new SqlConnection(connectionString);
            return orden switch
            {
                "1" => await connection.QueryAsync<RQCompraCab>(@$" SELECT*FROM V_WEB_REQCOMPRAS_Index
                Where cia=1 AND suc=1 AND periodo =@periodo AND User_Solicita = @CodUser  AND estado in(@estado1,@estado2)
                ORDER BY Rco_Numero DESC 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo, CodUser ,estado1, estado2 }),
                
                _   => await connection.QueryAsync<RQCompraCab>(@$"SELECT*FROM V_WEB_REQCOMPRAS_Index
                Where cia=1 AND suc=1 AND periodo =@periodo AND User_Solicita = @CodUser  AND estado in(@estado1,@estado2)
                ORDER BY Rco_Numero DESC 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo, CodUser ,estado1, estado2 }),
            };
        }
        public async Task<IEnumerable<RQCompraCab>> BusquedaMultiple(string periodo, PaginacionViewModel paginacion, string CodUser,string busqueda,string estado1,string estado2)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<RQCompraCab>(@$"SELECT * FROM V_WEB_REQCOMPRAS_Index
             Where cia=1 AND suc=1 AND periodo =@periodo AND User_Solicita = @CodUser  AND estado in(@estado1,@estado2)
             AND Rco_Numero LIKE '%'+@busqueda+'%' OR User_Solicita LIKE '%'+@busqueda+'%' OR U_Negocio LIKE '%'+@busqueda+'%' OR Centro_Costo LIKE '%'+@busqueda+'%' 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo, CodUser, busqueda, estado1, estado2 });         
        }  
        public async Task<int> ContarRegistros(string periodo,string CodUser, string estado1, string estado2)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.ExecuteScalarAsync<int>(@"SELECT COUNT(*)FROM V_WEB_REQCOMPRAS_Index
                Where cia=1 AND suc=1 AND periodo =@periodo AND User_Solicita = @CodUser  AND estado in(@estado1,@estado2)", 
                new { periodo,CodUser, estado1, estado2 });
        }
        public async Task<int> ContarRegistrosBusqueda(string periodo, string CodUser, string busqueda, string estado1,string estado2)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.ExecuteScalarAsync<int>(@"SELECT COUNT(*) FROM V_WEB_REQCOMPRAS_Index
             Where cia=1 AND suc=1 AND periodo =@periodo AND User_Solicita = @CodUser  AND estado in(@estado1,@estado2)
             AND Rco_Numero LIKE '%'+@busqueda+'%' OR User_Solicita LIKE '%'+@busqueda+'%' OR U_Negocio LIKE '%'+@busqueda+'%' OR Centro_Costo LIKE '%'+@busqueda+'%'",
                new { periodo, CodUser, busqueda, estado1,estado2 });
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
            return await connection.QueryFirstOrDefaultAsync<RQCompra>(@" SELECT  A.rco_numrco,A.rco_fecreg ,A.rco_motivo,A.ung_codung,A.cco_codcco ,D.CCO_DESLAR as cco_deslar,A.dis_coddis,A.rco_sitrco,A.rco_priori,
	              A.rco_obspri,A.rco_rembls,A.rco_presup,A.rco_indest,A.rco_tiprco,Cast(A.rco_indval as bit)as rco_indval,B.DIS_DESLAR as dis_deslar,
	              A.s10_usuario,A.rco_glorco,Cast(Isnull((
                             Select count(*) as CCC_TotFil From CUADRO_COMPARATIVO_COMPRAS_CCC X 
                             Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                             Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and y.scc_indest='1' 
                               and isnull(x.ccc_indoky,'0')='1'
                           ),0)as bit) as ccc_numero,
                     Isnull((Select Top 1 x.ocm_corocm From Orden_Compra_Occ X 
                              Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                              Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
                         ),'') as occ_numero
                  FROM REQUERIMIENTO_COMPRA_RCO A  
	              LEFT JOIN DISCIPLINAS_DIS     B ON A.cia_codcia=B.CIA_CODCIA AND A.dis_coddis=B.DIS_CODDIS
	              Left Join Unidad_Negocio_Ung  C on a.cia_codcia=C.cia_codcia and a.ung_codung=C.ung_codung
	              LEFT JOIN CENTRO_COSTO_CCO D ON A.cia_codcia=D.CIA_CODCIA AND A.cco_codcco=D.CCO_CODCCO   
                  WHERE rco_numrco = @Rco_numero ", new {Rco_numero});
        }

    }
}
