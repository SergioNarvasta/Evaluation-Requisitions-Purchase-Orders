using Dapper;
using HDProjectWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace HDProjectWeb.Models.Detalles
{
    public class DetalleReq
    {
        //Model for details products
        public int Codigoepk { get; set; }
        public string Item { get; set; }
        public string Descri { get; set; }
        public string Codigo { get; set; }
        public string Glosa { get; set; }
        public string Unidad { get; set; }
        public string Cantidad { get; set; }
        public string Codprov { get; set; }
        public string Nomprov { get; set; }
    }
    public interface IDetalleReqService
    {
        Task<IEnumerable<DetalleReq>> GetDetalleReq(string Rco_numero);
    }
    public class DetalleReqService : IDetalleReqService
    {
        private readonly string connectionString;
        public DetalleReqService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<DetalleReq>> GetDetalleReq(string Rco_numero)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<DetalleReq>(@"SELECT  B.rcd_corite as item ,L.prd_codprd as codigo,B.rcd_desprd as descri,B.rcd_glorcd as glosa,K.ume_codume as unidad,
	                                    rcd_canate as cantidad,Isnull(J.ccr_codccr,'000001') as codprov, J.ccr_nomaux as nomprov
	                                    FROM REQ_REQUI_COMPRA_RCO A
	                                    LEFT JOIN REQ_REQUI_COMPRA_RCD B ON A.cia_codcia=B.cia_codcia AND A.suc_codsuc=B.suc_codsuc AND A.rco_codepk=B.rco_codepk
	                                    Left Join OCOMPRA_OCC I on a.cia_codcia=i.CIA_CODCIA and a.suc_codsuc=i.suc_codsuc and A.occ_codepk =I.occ_codepk
                                        Left Join CUEN_CORR_CCR J on i.cia_codcia=j.CIA_CODCIA and i.ccr_codepk=j.ccr_codepk
										LEFT JOIN UMEDIDA_UME K ON A.cia_codcia=K.cia_codcia AND B.ume_codepk=K.ume_codepk
										LEFT JOIN PRODUCTOS_PRD L ON A.cia_codcia=L.cia_codcia AND B.prd_codepk=L.prd_codepk
	                                    WHERE A.rco_numrco =@Rco_numero", new { Rco_numero });
        }
    }
}
