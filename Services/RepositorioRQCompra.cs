using Dapper;
using HDProjectWeb.Models;
using HDProjectWeb.Models.Detalles;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Data.SqlClient;

namespace HDProjectWeb.Services
{
    public interface IRepositorioRQCompra
    {
        //Interface Obtener para la clase diseñada de vista RQComp
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
        private readonly IServicioPeriodo servicioPeriodo;

        public RepositorioRQCompra(IConfiguration configuration,IServicioPeriodo servicioPeriodo)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            this.servicioPeriodo = servicioPeriodo;
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
                "1" => await connection.QueryAsync<RQCompraCab>(@$"Select 
                a.rco_numrco as Rco_Numero,             
            	a.rco_fecreg as Rco_Fec_Registro, 
            	Isnull(rtrim(g.s10_nomusu),'') as Usuario_Origen,
            	Isnull(rtrim(d.aux_nomaux),'') as User_Solicita,
            	a.rco_motivo as Rco_Motivo,
            	rtrim(b.ung_deslar) as U_Negocio,
            	rtrim(a.cco_codcco) + '-' + rtrim(c.cco_deslar) as Centro_Costo,
            	e.Dis_DesLar as Disciplina,
            	(Case a.rco_sitrco When '1' Then 'PENDIENTE'
                               When '2' Then 'APROBADO'
                               When '3' Then 'RECHAZADO'
                               Else 'NO DEFINIDO' End) as Rco_Situacion_Aprobado,
                (Case a.rco_priori When '1' Then 'MUY ALTA'
                               When '2' Then 'ALTA'
                               When '3' Then 'MEDIA'
                               When '4' Then 'BAJA'
                               Else 'NO DEFINIDO' End) as Rco_Prioridad,
            	Isnull(a.rco_obspri,'') as Rco_Justificacion,
            	(Case a.rco_rembls When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Reembolso,
            	(Case a.rco_presup When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Presupuesto,
                a.rco_indval as RCO_Categorizado,
            	 Isnull((
                   Select count(*) as CCC_TotFil From CUADRO_COMPARATIVO_COMPRAS_CCC X 
                   Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                   Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and y.scc_indest='1' 
                     and isnull(x.ccc_indoky,'0')='1'
                 ),0) as CCC_NumeroCCC,
                 Isnull((Select Top 1 scc_numscc From Solicitud_Compra_Scc X 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc
                    and x.scc_indest='1' and x.rco_numrco=a.rco_numrco),''
                 ) as SCC_Cotizacion,
                
                 Isnull((Select Top 1 x.ocm_corocm From Orden_Compra_Occ X 
                    Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
            	  ),'') as OCC_NumeroOCC,
            	 Case (Select Top 1 x.occ_sitapr From Orden_Compra_Occ X 
                       Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                       Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' )
            	   When '1' Then 'PENDIENTE'
            	   When '2' Then 'APROBADO'
            	   When '3' Then 'RECHAZADO'
            	   Else ''
                 End As OCC_DesSituacionOCC,
            	 Isnull((Select Top 1 z.aux_nomaux From Orden_Compra_Occ X
                     Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                     Left Join AUXILIARES_AUX z On z.CIA_CODCIA = x.cia_codcia And z.AUX_CODAUX = x.aux_codaux
                     Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
                 ),'') as OCC_ProveedorOCC
             
             From Requerimiento_Compra_Rco A
             Left Join Unidad_Negocio_Ung  B on a.cia_codcia=b.cia_codcia and a.ung_codung=b.ung_codung
             Left Join Centro_Costo_Cco    C on a.cia_codcia=c.cia_codcia and a.cco_codcco=c.cco_codcco
             Left Join Auxiliares_AUX      D on a.cia_codcia=d.cia_codcia and a.s10_usuario=d.aux_codaux
             Left Join DISCIPLINAS_DIS     E on a.cia_codcia=e.cia_codcia and a.dis_coddis=e.dis_coddis
             Left Join APROBAC_REQCOM_APROBACIONES_ARA F On a.cia_codcia=f.cia_codcia and a.suc_codsuc=f.suc_codsuc and a.rco_numrco=f.rco_numrco and f.anm_codanm='0'
             Left Join sys_tabla_usuarios_s10          G on f.s10_usuario=g.s10_usuario
             Left Join tipo_requisicion_tir            H On h.cia_codcia = a.cia_codcia And h.rco_tiprco = a.rco_tiprco
             Where A.cia_codcia=1 AND A.suc_codsuc=1 AND Cast(YEAR(A.rco_fecreg) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.rco_fecreg)as char(2))),len(ltrim(Cast(MONTH(A.rco_fecreg)as char(2)))),2) =@periodo
             AND isnull(a.rco_flgmig,'0')='0' AND G.S10_USUARIO = @CodUser 
             AND A.rco_indest in(@estado1,@estado2)
             ORDER BY A.rco_numrco DESC 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo, CodUser ,estado1, estado2 }),
                "2" => await connection.QueryAsync<RQCompraCab>(@$"Select 
                a.rco_numrco as Rco_Numero,             
            	a.rco_fecreg as Rco_Fec_Registro, 
            	Isnull(rtrim(g.s10_nomusu),'') as Usuario_Origen,
            	Isnull(rtrim(d.aux_nomaux),'') as User_Solicita,
            	a.rco_motivo as Rco_Motivo,
            	rtrim(b.ung_deslar) as U_Negocio,
            	rtrim(a.cco_codcco) + '-' + rtrim(c.cco_deslar) as Centro_Costo,
            	e.Dis_DesLar as Disciplina,
            	(Case a.rco_sitrco When '1' Then 'PENDIENTE'
                               When '2' Then 'APROBADO'
                               When '3' Then 'RECHAZADO'
                               Else 'NO DEFINIDO' End) as Rco_Situacion_Aprobado,
                (Case a.rco_priori When '1' Then 'MUY ALTA'
                               When '2' Then 'ALTA'
                               When '3' Then 'MEDIA'
                               When '4' Then 'BAJA'
                               Else 'NO DEFINIDO' End) as Rco_Prioridad,
            	Isnull(a.rco_obspri,'') as Rco_Justificacion,
            	(Case a.rco_rembls When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Reembolso,
            	(Case a.rco_presup When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Presupuesto,
                a.rco_indval as RCO_Categorizado,
            	 Isnull((
                   Select count(*) as CCC_TotFil From CUADRO_COMPARATIVO_COMPRAS_CCC X 
                   Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                   Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and y.scc_indest='1' 
                     and isnull(x.ccc_indoky,'0')='1'
                 ),0) as CCC_NumeroCCC,
                 Isnull((Select Top 1 scc_numscc From Solicitud_Compra_Scc X 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc
                    and x.scc_indest='1' and x.rco_numrco=a.rco_numrco),''
                 ) as SCC_Cotizacion,
                
                 Isnull((Select Top 1 x.ocm_corocm From Orden_Compra_Occ X 
                    Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
            	  ),'') as OCC_NumeroOCC,
            	 Case (Select Top 1 x.occ_sitapr From Orden_Compra_Occ X 
                       Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                       Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' )
            	   When '1' Then 'PENDIENTE'
            	   When '2' Then 'APROBADO'
            	   When '3' Then 'RECHAZADO'
            	   Else ''
                 End As OCC_DesSituacionOCC,
            	 Isnull((Select Top 1 z.aux_nomaux From Orden_Compra_Occ X
                     Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                     Left Join AUXILIARES_AUX z On z.CIA_CODCIA = x.cia_codcia And z.AUX_CODAUX = x.aux_codaux
                     Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
                 ),'') as OCC_ProveedorOCC
             
             From Requerimiento_Compra_Rco A
             Left Join Unidad_Negocio_Ung  B on a.cia_codcia=b.cia_codcia and a.ung_codung=b.ung_codung
             Left Join Centro_Costo_Cco    C on a.cia_codcia=c.cia_codcia and a.cco_codcco=c.cco_codcco
             Left Join Auxiliares_AUX      D on a.cia_codcia=d.cia_codcia and a.s10_usuario=d.aux_codaux
             Left Join DISCIPLINAS_DIS     E on a.cia_codcia=e.cia_codcia and a.dis_coddis=e.dis_coddis
             Left Join APROBAC_REQCOM_APROBACIONES_ARA F On a.cia_codcia=f.cia_codcia and a.suc_codsuc=f.suc_codsuc and a.rco_numrco=f.rco_numrco and f.anm_codanm='0'
             Left Join sys_tabla_usuarios_s10          G on f.s10_usuario=g.s10_usuario
             Left Join tipo_requisicion_tir            H On h.cia_codcia = a.cia_codcia And h.rco_tiprco = a.rco_tiprco
             Where A.cia_codcia=1 AND A.suc_codsuc=1 AND Cast(YEAR(A.rco_fecreg) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.rco_fecreg)as char(2))),len(ltrim(Cast(MONTH(A.rco_fecreg)as char(2)))),2) =@periodo
             AND isnull(a.rco_flgmig,'0')='0' AND G.S10_USUARIO = @CodUser 
             AND A.rco_indest in(@estado1,@estado2)
             ORDER BY A.rco_fecreg  DESC 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo, CodUser ,estado1, estado2 }),
                "3" => await connection.QueryAsync<RQCompraCab>(@$"Select 
                a.rco_numrco as Rco_Numero,             
            	a.rco_fecreg as Rco_Fec_Registro, 
            	Isnull(rtrim(g.s10_nomusu),'') as Usuario_Origen,
            	Isnull(rtrim(d.aux_nomaux),'') as User_Solicita,
            	a.rco_motivo as Rco_Motivo,
            	rtrim(b.ung_deslar) as U_Negocio,
            	rtrim(a.cco_codcco) + '-' + rtrim(c.cco_deslar) as Centro_Costo,
            	e.Dis_DesLar as Disciplina,
            	(Case a.rco_sitrco When '1' Then 'PENDIENTE'
                               When '2' Then 'APROBADO'
                               When '3' Then 'RECHAZADO'
                               Else 'NO DEFINIDO' End) as Rco_Situacion_Aprobado,
                (Case a.rco_priori When '1' Then 'MUY ALTA'
                               When '2' Then 'ALTA'
                               When '3' Then 'MEDIA'
                               When '4' Then 'BAJA'
                               Else 'NO DEFINIDO' End) as Rco_Prioridad,
            	Isnull(a.rco_obspri,'') as Rco_Justificacion,
            	(Case a.rco_rembls When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Reembolso,
            	(Case a.rco_presup When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Presupuesto,
                a.rco_indval as RCO_Categorizado,
            	 Isnull((
                   Select count(*) as CCC_TotFil From CUADRO_COMPARATIVO_COMPRAS_CCC X 
                   Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                   Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and y.scc_indest='1' 
                     and isnull(x.ccc_indoky,'0')='1'
                 ),0) as CCC_NumeroCCC,
                 Isnull((Select Top 1 scc_numscc From Solicitud_Compra_Scc X 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc
                    and x.scc_indest='1' and x.rco_numrco=a.rco_numrco),''
                 ) as SCC_Cotizacion,
                
                 Isnull((Select Top 1 x.ocm_corocm From Orden_Compra_Occ X 
                    Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
            	  ),'') as OCC_NumeroOCC,
            	 Case (Select Top 1 x.occ_sitapr From Orden_Compra_Occ X 
                       Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                       Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' )
            	   When '1' Then 'PENDIENTE'
            	   When '2' Then 'APROBADO'
            	   When '3' Then 'RECHAZADO'
            	   Else ''
                 End As OCC_DesSituacionOCC,
            	 Isnull((Select Top 1 z.aux_nomaux From Orden_Compra_Occ X
                     Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                     Left Join AUXILIARES_AUX z On z.CIA_CODCIA = x.cia_codcia And z.AUX_CODAUX = x.aux_codaux
                     Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
                 ),'') as OCC_ProveedorOCC
             
             From Requerimiento_Compra_Rco A
             Left Join Unidad_Negocio_Ung  B on a.cia_codcia=b.cia_codcia and a.ung_codung=b.ung_codung
             Left Join Centro_Costo_Cco    C on a.cia_codcia=c.cia_codcia and a.cco_codcco=c.cco_codcco
             Left Join Auxiliares_AUX      D on a.cia_codcia=d.cia_codcia and a.s10_usuario=d.aux_codaux
             Left Join DISCIPLINAS_DIS     E on a.cia_codcia=e.cia_codcia and a.dis_coddis=e.dis_coddis
             Left Join APROBAC_REQCOM_APROBACIONES_ARA F On a.cia_codcia=f.cia_codcia and a.suc_codsuc=f.suc_codsuc and a.rco_numrco=f.rco_numrco and f.anm_codanm='0'
             Left Join sys_tabla_usuarios_s10          G on f.s10_usuario=g.s10_usuario
             Left Join tipo_requisicion_tir            H On h.cia_codcia = a.cia_codcia And h.rco_tiprco = a.rco_tiprco
             Where A.cia_codcia=1 AND A.suc_codsuc=1 AND Cast(YEAR(A.rco_fecreg) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.rco_fecreg)as char(2))),len(ltrim(Cast(MONTH(A.rco_fecreg)as char(2)))),2) =@periodo
             AND isnull(a.rco_flgmig,'0')='0' AND G.S10_USUARIO = @CodUser 
             AND A.rco_indest in(@estado1,@estado2)
             ORDER BY D.aux_nomaux DESC 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo, CodUser ,estado1, estado2 }),
                "4" => await connection.QueryAsync<RQCompraCab>(@$"Select 
                a.rco_numrco as Rco_Numero,             
            	a.rco_fecreg as Rco_Fec_Registro, 
            	Isnull(rtrim(g.s10_nomusu),'') as Usuario_Origen,
            	Isnull(rtrim(d.aux_nomaux),'') as User_Solicita,
            	a.rco_motivo as Rco_Motivo,
            	rtrim(b.ung_deslar) as U_Negocio,
            	rtrim(a.cco_codcco) + '-' + rtrim(c.cco_deslar) as Centro_Costo,
            	e.Dis_DesLar as Disciplina,
            	(Case a.rco_sitrco When '1' Then 'PENDIENTE'
                               When '2' Then 'APROBADO'
                               When '3' Then 'RECHAZADO'
                               Else 'NO DEFINIDO' End) as Rco_Situacion_Aprobado,
                (Case a.rco_priori When '1' Then 'MUY ALTA'
                               When '2' Then 'ALTA'
                               When '3' Then 'MEDIA'
                               When '4' Then 'BAJA'
                               Else 'NO DEFINIDO' End) as Rco_Prioridad,
            	Isnull(a.rco_obspri,'') as Rco_Justificacion,
            	(Case a.rco_rembls When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Reembolso,
            	(Case a.rco_presup When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Presupuesto,
                a.rco_indval as RCO_Categorizado,
            	 Isnull((
                   Select count(*) as CCC_TotFil From CUADRO_COMPARATIVO_COMPRAS_CCC X 
                   Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                   Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and y.scc_indest='1' 
                     and isnull(x.ccc_indoky,'0')='1'
                 ),0) as CCC_NumeroCCC,
                 Isnull((Select Top 1 scc_numscc From Solicitud_Compra_Scc X 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc
                    and x.scc_indest='1' and x.rco_numrco=a.rco_numrco),''
                 ) as SCC_Cotizacion,
                
                 Isnull((Select Top 1 x.ocm_corocm From Orden_Compra_Occ X 
                    Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
            	  ),'') as OCC_NumeroOCC,
            	 Case (Select Top 1 x.occ_sitapr From Orden_Compra_Occ X 
                       Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                       Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' )
            	   When '1' Then 'PENDIENTE'
            	   When '2' Then 'APROBADO'
            	   When '3' Then 'RECHAZADO'
            	   Else ''
                 End As OCC_DesSituacionOCC,
            	 Isnull((Select Top 1 z.aux_nomaux From Orden_Compra_Occ X
                     Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                     Left Join AUXILIARES_AUX z On z.CIA_CODCIA = x.cia_codcia And z.AUX_CODAUX = x.aux_codaux
                     Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
                 ),'') as OCC_ProveedorOCC
             
             From Requerimiento_Compra_Rco A
             Left Join Unidad_Negocio_Ung  B on a.cia_codcia=b.cia_codcia and a.ung_codung=b.ung_codung
             Left Join Centro_Costo_Cco    C on a.cia_codcia=c.cia_codcia and a.cco_codcco=c.cco_codcco
             Left Join Auxiliares_AUX      D on a.cia_codcia=d.cia_codcia and a.s10_usuario=d.aux_codaux
             Left Join DISCIPLINAS_DIS     E on a.cia_codcia=e.cia_codcia and a.dis_coddis=e.dis_coddis
             Left Join APROBAC_REQCOM_APROBACIONES_ARA F On a.cia_codcia=f.cia_codcia and a.suc_codsuc=f.suc_codsuc and a.rco_numrco=f.rco_numrco and f.anm_codanm='0'
             Left Join sys_tabla_usuarios_s10          G on f.s10_usuario=g.s10_usuario
             Left Join tipo_requisicion_tir            H On h.cia_codcia = a.cia_codcia And h.rco_tiprco = a.rco_tiprco
             Where A.cia_codcia=1 AND A.suc_codsuc=1 AND Cast(YEAR(A.rco_fecreg) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.rco_fecreg)as char(2))),len(ltrim(Cast(MONTH(A.rco_fecreg)as char(2)))),2) =@periodo
             AND isnull(a.rco_flgmig,'0')='0' AND G.S10_USUARIO = @CodUser    
             AND A.rco_indest in(@estado1,@estado2)
             ORDER BY B.ung_deslar DESC 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo, CodUser ,estado1, estado2 }),
                "5" => await connection.QueryAsync<RQCompraCab>(@$"Select 
                a.rco_numrco as Rco_Numero,             
            	a.rco_fecreg as Rco_Fec_Registro, 
            	Isnull(rtrim(g.s10_nomusu),'') as Usuario_Origen,
            	Isnull(rtrim(d.aux_nomaux),'') as User_Solicita,
            	a.rco_motivo as Rco_Motivo,
            	rtrim(b.ung_deslar) as U_Negocio,
            	rtrim(a.cco_codcco) + '-' + rtrim(c.cco_deslar) as Centro_Costo,
            	e.Dis_DesLar as Disciplina,
            	(Case a.rco_sitrco When '1' Then 'PENDIENTE'
                               When '2' Then 'APROBADO'
                               When '3' Then 'RECHAZADO'
                               Else 'NO DEFINIDO' End) as Rco_Situacion_Aprobado,
                (Case a.rco_priori When '1' Then 'MUY ALTA'
                               When '2' Then 'ALTA'
                               When '3' Then 'MEDIA'
                               When '4' Then 'BAJA'
                               Else 'NO DEFINIDO' End) as Rco_Prioridad,
            	Isnull(a.rco_obspri,'') as Rco_Justificacion,
            	(Case a.rco_rembls When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Reembolso,
            	(Case a.rco_presup When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Presupuesto,
                a.rco_indval as RCO_Categorizado,
            	 Isnull((
                   Select count(*) as CCC_TotFil From CUADRO_COMPARATIVO_COMPRAS_CCC X 
                   Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                   Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and y.scc_indest='1' 
                     and isnull(x.ccc_indoky,'0')='1'
                 ),0) as CCC_NumeroCCC,
                 Isnull((Select Top 1 scc_numscc From Solicitud_Compra_Scc X 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc
                    and x.scc_indest='1' and x.rco_numrco=a.rco_numrco),''
                 ) as SCC_Cotizacion,
                
                 Isnull((Select Top 1 x.ocm_corocm From Orden_Compra_Occ X 
                    Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
            	  ),'') as OCC_NumeroOCC,
            	 Case (Select Top 1 x.occ_sitapr From Orden_Compra_Occ X 
                       Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                       Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' )
            	   When '1' Then 'PENDIENTE'
            	   When '2' Then 'APROBADO'
            	   When '3' Then 'RECHAZADO'
            	   Else ''
                 End As OCC_DesSituacionOCC,
            	 Isnull((Select Top 1 z.aux_nomaux From Orden_Compra_Occ X
                     Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                     Left Join AUXILIARES_AUX z On z.CIA_CODCIA = x.cia_codcia And z.AUX_CODAUX = x.aux_codaux
                     Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
                 ),'') as OCC_ProveedorOCC
             
             From Requerimiento_Compra_Rco A
             Left Join Unidad_Negocio_Ung  B on a.cia_codcia=b.cia_codcia and a.ung_codung=b.ung_codung
             Left Join Centro_Costo_Cco    C on a.cia_codcia=c.cia_codcia and a.cco_codcco=c.cco_codcco
             Left Join Auxiliares_AUX      D on a.cia_codcia=d.cia_codcia and a.s10_usuario=d.aux_codaux
             Left Join DISCIPLINAS_DIS     E on a.cia_codcia=e.cia_codcia and a.dis_coddis=e.dis_coddis
             Left Join APROBAC_REQCOM_APROBACIONES_ARA F On a.cia_codcia=f.cia_codcia and a.suc_codsuc=f.suc_codsuc and a.rco_numrco=f.rco_numrco and f.anm_codanm='0'
             Left Join sys_tabla_usuarios_s10          G on f.s10_usuario=g.s10_usuario
             Left Join tipo_requisicion_tir            H On h.cia_codcia = a.cia_codcia And h.rco_tiprco = a.rco_tiprco
             Where A.cia_codcia=1 AND A.suc_codsuc=1 AND Cast(YEAR(A.rco_fecreg) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.rco_fecreg)as char(2))),len(ltrim(Cast(MONTH(A.rco_fecreg)as char(2)))),2) =@periodo
             AND isnull(a.rco_flgmig,'0')='0' AND G.S10_USUARIO = @CodUser  
             AND A.rco_indest in(@estado1,@estado2)
             ORDER BY A.cco_codcco DESC 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo, CodUser ,estado1, estado2 }),
                "6" => await connection.QueryAsync<RQCompraCab>(@$"Select 
                a.rco_numrco as Rco_Numero,             
            	a.rco_fecreg as Rco_Fec_Registro, 
            	Isnull(rtrim(g.s10_nomusu),'') as Usuario_Origen,
            	Isnull(rtrim(d.aux_nomaux),'') as User_Solicita,
            	a.rco_motivo as Rco_Motivo,
            	rtrim(b.ung_deslar) as U_Negocio,
            	rtrim(a.cco_codcco) + '-' + rtrim(c.cco_deslar) as Centro_Costo,
            	e.Dis_DesLar as Disciplina,
            	(Case a.rco_sitrco When '1' Then 'PENDIENTE'
                               When '2' Then 'APROBADO'
                               When '3' Then 'RECHAZADO'
                               Else 'NO DEFINIDO' End) as Rco_Situacion_Aprobado,
                (Case a.rco_priori When '1' Then 'MUY ALTA'
                               When '2' Then 'ALTA'
                               When '3' Then 'MEDIA'
                               When '4' Then 'BAJA'
                               Else 'NO DEFINIDO' End) as Rco_Prioridad,
            	Isnull(a.rco_obspri,'') as Rco_Justificacion,
            	(Case a.rco_rembls When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Reembolso,
            	(Case a.rco_presup When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Presupuesto,
                a.rco_indval as RCO_Categorizado,
            	 Isnull((
                   Select count(*) as CCC_TotFil From CUADRO_COMPARATIVO_COMPRAS_CCC X 
                   Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                   Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and y.scc_indest='1' 
                     and isnull(x.ccc_indoky,'0')='1'
                 ),0) as CCC_NumeroCCC,
                 Isnull((Select Top 1 scc_numscc From Solicitud_Compra_Scc X 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc
                    and x.scc_indest='1' and x.rco_numrco=a.rco_numrco),''
                 ) as SCC_Cotizacion,
                
                 Isnull((Select Top 1 x.ocm_corocm From Orden_Compra_Occ X 
                    Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
            	  ),'') as OCC_NumeroOCC,
            	 Case (Select Top 1 x.occ_sitapr From Orden_Compra_Occ X 
                       Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                       Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' )
            	   When '1' Then 'PENDIENTE'
            	   When '2' Then 'APROBADO'
            	   When '3' Then 'RECHAZADO'
            	   Else ''
                 End As OCC_DesSituacionOCC,
            	 Isnull((Select Top 1 z.aux_nomaux From Orden_Compra_Occ X
                     Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                     Left Join AUXILIARES_AUX z On z.CIA_CODCIA = x.cia_codcia And z.AUX_CODAUX = x.aux_codaux
                     Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
                 ),'') as OCC_ProveedorOCC
             
             From Requerimiento_Compra_Rco A
             Left Join Unidad_Negocio_Ung  B on a.cia_codcia=b.cia_codcia and a.ung_codung=b.ung_codung
             Left Join Centro_Costo_Cco    C on a.cia_codcia=c.cia_codcia and a.cco_codcco=c.cco_codcco
             Left Join Auxiliares_AUX      D on a.cia_codcia=d.cia_codcia and a.s10_usuario=d.aux_codaux
             Left Join DISCIPLINAS_DIS     E on a.cia_codcia=e.cia_codcia and a.dis_coddis=e.dis_coddis
             Left Join APROBAC_REQCOM_APROBACIONES_ARA F On a.cia_codcia=f.cia_codcia and a.suc_codsuc=f.suc_codsuc and a.rco_numrco=f.rco_numrco and f.anm_codanm='0'
             Left Join sys_tabla_usuarios_s10          G on f.s10_usuario=g.s10_usuario
             Left Join tipo_requisicion_tir            H On h.cia_codcia = a.cia_codcia And h.rco_tiprco = a.rco_tiprco
             Where A.cia_codcia=1 AND A.suc_codsuc=1 AND Cast(YEAR(A.rco_fecreg) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.rco_fecreg)as char(2))),len(ltrim(Cast(MONTH(A.rco_fecreg)as char(2)))),2) =@periodo
             AND isnull(a.rco_flgmig,'0')='0' AND G.S10_USUARIO = @CodUser 
             AND A.rco_indest in(@estado1,@estado2)
             ORDER BY A.rco_sitrco DESC 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo, CodUser ,estado1, estado2 }),
                "7" => await connection.QueryAsync<RQCompraCab>(@$"Select 
                a.rco_numrco as Rco_Numero,             
            	a.rco_fecreg as Rco_Fec_Registro, 
            	Isnull(rtrim(g.s10_nomusu),'') as Usuario_Origen,
            	Isnull(rtrim(d.aux_nomaux),'') as User_Solicita,
            	a.rco_motivo as Rco_Motivo,
            	rtrim(b.ung_deslar) as U_Negocio,
            	rtrim(a.cco_codcco) + '-' + rtrim(c.cco_deslar) as Centro_Costo,
            	e.Dis_DesLar as Disciplina,
            	(Case a.rco_sitrco When '1' Then 'PENDIENTE'
                               When '2' Then 'APROBADO'
                               When '3' Then 'RECHAZADO'
                               Else 'NO DEFINIDO' End) as Rco_Situacion_Aprobado,
                (Case a.rco_priori When '1' Then 'MUY ALTA'
                               When '2' Then 'ALTA'
                               When '3' Then 'MEDIA'
                               When '4' Then 'BAJA'
                               Else 'NO DEFINIDO' End) as Rco_Prioridad,
            	Isnull(a.rco_obspri,'') as Rco_Justificacion,
            	(Case a.rco_rembls When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Reembolso,
            	(Case a.rco_presup When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Presupuesto,
                a.rco_indval as RCO_Categorizado,
            	 Isnull((
                   Select count(*) as CCC_TotFil From CUADRO_COMPARATIVO_COMPRAS_CCC X 
                   Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                   Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and y.scc_indest='1' 
                     and isnull(x.ccc_indoky,'0')='1'
                 ),0) as CCC_NumeroCCC,
                 Isnull((Select Top 1 scc_numscc From Solicitud_Compra_Scc X 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc
                    and x.scc_indest='1' and x.rco_numrco=a.rco_numrco),''
                 ) as SCC_Cotizacion,
                
                 Isnull((Select Top 1 x.ocm_corocm From Orden_Compra_Occ X 
                    Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
            	  ),'') as OCC_NumeroOCC,
            	 Case (Select Top 1 x.occ_sitapr From Orden_Compra_Occ X 
                       Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                       Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' )
            	   When '1' Then 'PENDIENTE'
            	   When '2' Then 'APROBADO'
            	   When '3' Then 'RECHAZADO'
            	   Else ''
                 End As OCC_DesSituacionOCC,
            	 Isnull((Select Top 1 z.aux_nomaux From Orden_Compra_Occ X
                     Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                     Left Join AUXILIARES_AUX z On z.CIA_CODCIA = x.cia_codcia And z.AUX_CODAUX = x.aux_codaux
                     Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
                 ),'') as OCC_ProveedorOCC
             
             From Requerimiento_Compra_Rco A
             Left Join Unidad_Negocio_Ung  B on a.cia_codcia=b.cia_codcia and a.ung_codung=b.ung_codung
             Left Join Centro_Costo_Cco    C on a.cia_codcia=c.cia_codcia and a.cco_codcco=c.cco_codcco
             Left Join Auxiliares_AUX      D on a.cia_codcia=d.cia_codcia and a.s10_usuario=d.aux_codaux
             Left Join DISCIPLINAS_DIS     E on a.cia_codcia=e.cia_codcia and a.dis_coddis=e.dis_coddis
             Left Join APROBAC_REQCOM_APROBACIONES_ARA F On a.cia_codcia=f.cia_codcia and a.suc_codsuc=f.suc_codsuc and a.rco_numrco=f.rco_numrco and f.anm_codanm='0'
             Left Join sys_tabla_usuarios_s10          G on f.s10_usuario=g.s10_usuario
             Left Join tipo_requisicion_tir            H On h.cia_codcia = a.cia_codcia And h.rco_tiprco = a.rco_tiprco
             Where A.cia_codcia=1 AND A.suc_codsuc=1 AND Cast(YEAR(A.rco_fecreg) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.rco_fecreg)as char(2))),len(ltrim(Cast(MONTH(A.rco_fecreg)as char(2)))),2) =@periodo
             AND isnull(a.rco_flgmig,'0')='0' AND G.S10_USUARIO = @CodUser    
             AND A.rco_indest in(@estado1,@estado2)
             ORDER BY A.rco_priori DESC 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo, CodUser ,estado1, estado2 }),
                _   => await connection.QueryAsync<RQCompraCab>(@$"Select 
                a.rco_numrco as Rco_Numero,             
            	a.rco_fecreg as Rco_Fec_Registro, 
            	Isnull(rtrim(g.s10_nomusu),'') as Usuario_Origen,
            	Isnull(rtrim(d.aux_nomaux),'') as User_Solicita,
            	a.rco_motivo as Rco_Motivo,
            	rtrim(b.ung_deslar) as U_Negocio,
            	rtrim(a.cco_codcco) + '-' + rtrim(c.cco_deslar) as Centro_Costo,
            	e.Dis_DesLar as Disciplina,
            	(Case a.rco_sitrco When '1' Then 'PENDIENTE'
                               When '2' Then 'APROBADO'
                               When '3' Then 'RECHAZADO'
                               Else 'NO DEFINIDO' End) as Rco_Situacion_Aprobado,
                (Case a.rco_priori When '1' Then 'MUY ALTA'
                               When '2' Then 'ALTA'
                               When '3' Then 'MEDIA'
                               When '4' Then 'BAJA'
                               Else 'NO DEFINIDO' End) as Rco_Prioridad,
            	Isnull(a.rco_obspri,'') as Rco_Justificacion,
            	(Case a.rco_rembls When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Reembolso,
            	(Case a.rco_presup When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Presupuesto,
                a.rco_indval as RCO_Categorizado,
            	 Isnull((
                   Select count(*) as CCC_TotFil From CUADRO_COMPARATIVO_COMPRAS_CCC X 
                   Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                   Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and y.scc_indest='1' 
                     and isnull(x.ccc_indoky,'0')='1'
                 ),0) as CCC_NumeroCCC,
                 Isnull((Select Top 1 scc_numscc From Solicitud_Compra_Scc X 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc
                    and x.scc_indest='1' and x.rco_numrco=a.rco_numrco),''
                 ) as SCC_Cotizacion,
                
                 Isnull((Select Top 1 x.ocm_corocm From Orden_Compra_Occ X 
                    Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
            	  ),'') as OCC_NumeroOCC,
            	 Case (Select Top 1 x.occ_sitapr From Orden_Compra_Occ X 
                       Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                       Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' )
            	   When '1' Then 'PENDIENTE'
            	   When '2' Then 'APROBADO'
            	   When '3' Then 'RECHAZADO'
            	   Else ''
                 End As OCC_DesSituacionOCC,
            	 Isnull((Select Top 1 z.aux_nomaux From Orden_Compra_Occ X
                     Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                     Left Join AUXILIARES_AUX z On z.CIA_CODCIA = x.cia_codcia And z.AUX_CODAUX = x.aux_codaux
                     Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
                 ),'') as OCC_ProveedorOCC
             
             From Requerimiento_Compra_Rco A
             Left Join Unidad_Negocio_Ung  B on a.cia_codcia=b.cia_codcia and a.ung_codung=b.ung_codung
             Left Join Centro_Costo_Cco    C on a.cia_codcia=c.cia_codcia and a.cco_codcco=c.cco_codcco
             Left Join Auxiliares_AUX      D on a.cia_codcia=d.cia_codcia and a.s10_usuario=d.aux_codaux
             Left Join DISCIPLINAS_DIS     E on a.cia_codcia=e.cia_codcia and a.dis_coddis=e.dis_coddis
             Left Join APROBAC_REQCOM_APROBACIONES_ARA F On a.cia_codcia=f.cia_codcia and a.suc_codsuc=f.suc_codsuc and a.rco_numrco=f.rco_numrco and f.anm_codanm='0'
             Left Join sys_tabla_usuarios_s10          G on f.s10_usuario=g.s10_usuario
             Left Join tipo_requisicion_tir            H On h.cia_codcia = a.cia_codcia And h.rco_tiprco = a.rco_tiprco
             Where A.cia_codcia=1 AND A.suc_codsuc=1 AND Cast(YEAR(A.rco_fecreg) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.rco_fecreg)as char(2))),len(ltrim(Cast(MONTH(A.rco_fecreg)as char(2)))),2) =@periodo
             AND isnull(a.rco_flgmig,'0')='0' AND G.S10_USUARIO = @CodUser 
             AND A.rco_indest in(@estado1,@estado2)
             ORDER BY A.Rco_fec_registro DESC 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo, CodUser ,estado1, estado2 }),
            };
        }
        public async Task<IEnumerable<RQCompraCab>> BusquedaMultiple(string periodo, PaginacionViewModel paginacion, string CodUser,string busqueda,string estado1,string estado2)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<RQCompraCab>(@$"Select 
                a.rco_numrco as Rco_Numero,             
            	a.rco_fecreg as Rco_Fec_Registro, 
            	Isnull(rtrim(g.s10_nomusu),'') as Usuario_Origen,
            	Isnull(rtrim(d.aux_nomaux),'') as User_Solicita,
            	a.rco_motivo as Rco_Motivo,
            	rtrim(b.ung_deslar) as U_Negocio,
            	rtrim(a.cco_codcco) + '-' + rtrim(c.cco_deslar) as Centro_Costo,
            	e.Dis_DesLar as Disciplina,
            	(Case a.rco_sitrco When '1' Then 'PENDIENTE'
                               When '2' Then 'APROBADO'
                               When '3' Then 'RECHAZADO'
                               Else 'NO DEFINIDO' End) as Rco_Situacion_Aprobado,
                (Case a.rco_priori When '1' Then 'MUY ALTA'
                               When '2' Then 'ALTA'
                               When '3' Then 'MEDIA'
                               When '4' Then 'BAJA'
                               Else 'NO DEFINIDO' End) as Rco_Prioridad,
            	Isnull(a.rco_obspri,'') as Rco_Justificacion,
            	(Case a.rco_rembls When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Reembolso,
            	(Case a.rco_presup When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Presupuesto,
                a.rco_indval as RCO_Categorizado,
            	 Isnull((
                   Select count(*) as CCC_TotFil From CUADRO_COMPARATIVO_COMPRAS_CCC X 
                   Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                   Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and y.scc_indest='1' 
                     and isnull(x.ccc_indoky,'0')='1'
                 ),0) as CCC_NumeroCCC,
                 Isnull((Select Top 1 scc_numscc From Solicitud_Compra_Scc X 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc
                    and x.scc_indest='1' and x.rco_numrco=a.rco_numrco),''
                 ) as SCC_Cotizacion,
                
                 Isnull((Select Top 1 x.ocm_corocm From Orden_Compra_Occ X 
                    Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                    Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
            	  ),'') as OCC_NumeroOCC,
            	 Case (Select Top 1 x.occ_sitapr From Orden_Compra_Occ X 
                       Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                       Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' )
            	   When '1' Then 'PENDIENTE'
            	   When '2' Then 'APROBADO'
            	   When '3' Then 'RECHAZADO'
            	   Else ''
                 End As OCC_DesSituacionOCC,
            	 Isnull((Select Top 1 z.aux_nomaux From Orden_Compra_Occ X
                     Left Join Solicitud_Compra_Scc Y on x.cia_codcia=y.cia_codcia and x.suc_codsuc=y.suc_codsuc and x.scc_numscc=y.scc_numscc 
                     Left Join AUXILIARES_AUX z On z.CIA_CODCIA = x.cia_codcia And z.AUX_CODAUX = x.aux_codaux
                     Where x.cia_codcia=a.cia_codcia and x.suc_codsuc=a.suc_codsuc and y.rco_numrco=a.rco_numrco and x.occ_indest='1' and y.scc_indest='1' 
                 ),'') as OCC_ProveedorOCC
             
             From Requerimiento_Compra_Rco A
             Left Join Unidad_Negocio_Ung  B on a.cia_codcia=b.cia_codcia and a.ung_codung=b.ung_codung
             Left Join Centro_Costo_Cco    C on a.cia_codcia=c.cia_codcia and a.cco_codcco=c.cco_codcco
             Left Join Auxiliares_AUX      D on a.cia_codcia=d.cia_codcia and a.s10_usuario=d.aux_codaux
             Left Join DISCIPLINAS_DIS     E on a.cia_codcia=e.cia_codcia and a.dis_coddis=e.dis_coddis
             Left Join APROBAC_REQCOM_APROBACIONES_ARA F On a.cia_codcia=f.cia_codcia and a.suc_codsuc=f.suc_codsuc and a.rco_numrco=f.rco_numrco and f.anm_codanm='0'
             Left Join sys_tabla_usuarios_s10          G on f.s10_usuario=g.s10_usuario
             Left Join tipo_requisicion_tir            H On h.cia_codcia = a.cia_codcia And h.rco_tiprco = a.rco_tiprco
             Where A.cia_codcia=1 AND A.suc_codsuc=1 AND Cast(YEAR(A.rco_fecreg) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.rco_fecreg)as char(2))),len(ltrim(Cast(MONTH(A.rco_fecreg)as char(2)))),2) =@periodo
             AND isnull(a.rco_flgmig,'0')='0' AND G.S10_USUARIO = @CodUser 
             AND A.rco_numrco LIKE '%'+@busqueda+'%' OR D.aux_nomaux LIKE '%'+@busqueda+'%' OR b.ung_deslar LIKE '%'+@busqueda+'%' OR rtrim(a.cco_codcco)+ rtrim(c.cco_deslar) LIKE '%'+@busqueda+'%'
             AND A.rco_indest in(@estado1,@estado2)
             ORDER BY A.rco_fecreg DESC 
                OFFSET {paginacion.RecordsASaltar}
                ROWS FETCH NEXT {paginacion.RecordsPorPagina} 
                ROWS ONLY", new { periodo, CodUser, busqueda, estado1, estado2 });         
        }  
        public async Task<int> ContarRegistros(string periodo,string CodUser, string estado1, string estado2)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM REQUERIMIENTO_COMPRA_RCO A
                Left Join APROBAC_REQCOM_APROBACIONES_ARA F On a.cia_codcia=f.cia_codcia and a.suc_codsuc=f.suc_codsuc and a.rco_numrco=f.rco_numrco and f.anm_codanm='0'
                LEFT JOIN SYS_TABLA_USUARIOS_S10 G ON F.s10_usuario =G.S10_USUARIO
                WHERE Cast(YEAR(A.rco_fecreg) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.rco_fecreg)as char(2))),len(ltrim(Cast(MONTH(A.rco_fecreg)as char(2)))),2)=@periodo
                AND  G.S10_USUARIO = @CodUser 
                AND A.rco_indest in(@estado1,@estado2) ", new { periodo,CodUser, estado1, estado2 });
        }
        public async Task<int> ContarRegistrosBusqueda(string periodo, string CodUser, string busqueda, string estado1,string estado2)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM REQUERIMIENTO_COMPRA_RCO A
                Left Join APROBAC_REQCOM_APROBACIONES_ARA F On a.cia_codcia=f.cia_codcia and a.suc_codsuc=f.suc_codsuc and a.rco_numrco=f.rco_numrco and f.anm_codanm='0'
                LEFT JOIN SYS_TABLA_USUARIOS_S10 G ON F.s10_usuario =G.S10_USUARIO
                Left Join Unidad_Negocio_Ung  B on a.cia_codcia=b.cia_codcia and a.ung_codung=b.ung_codung
                Left Join Auxiliares_AUX      D on a.cia_codcia=d.cia_codcia and a.s10_usuario=d.aux_codaux                
                Left Join Centro_Costo_Cco    C on a.cia_codcia=c.cia_codcia and a.cco_codcco=c.cco_codcco
                WHERE Cast(YEAR(A.rco_fecreg) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.rco_fecreg)as char(2))),len(ltrim(Cast(MONTH(A.rco_fecreg)as char(2)))),2) =@periodo 
                AND  G.S10_USUARIO = @CodUser AND A.rco_indest in(@estado1,@estado2)
                AND A.rco_numrco LIKE '%'+@busqueda+'%' OR D.aux_nomaux LIKE '%'+@busqueda+'%' OR B.ung_deslar LIKE '%'+@busqueda+'%' OR rtrim(A.cco_codcco)+ rtrim(C.cco_deslar) LIKE '%'+@busqueda+'%'",
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
