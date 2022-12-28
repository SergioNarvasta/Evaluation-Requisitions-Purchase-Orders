using Dapper;
using HDProjectWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace HDProjectWeb.Models.Detalles
{

    public class Adjuntos
    {
        //Model for details products   
        public string Item { get; set; }
        public string Descri { get; set; }
        public string Codigo { get; set; }
        public string Glosa { get; set; }
        public string Unidad { get; set; }
        public string Cantidad { get; set; }
        public string Codprov { get; set; }
        public string Nomprov { get; set; }
    }
    public interface IAdjuntosService
    {
        
    }
    public class AdjuntosService : IAdjuntosService
    {
        private readonly string connectionString;
        public AdjuntosService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<Adjuntos>> GetDetalleReq(string Rco_numero)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Adjuntos>(@"SELECT  B.rcd_corite as item ,B.prd_codprd as codigo,rcd_desprd as descri,rcd_glorcd as glosa,ume_codume as unidad,
	                                    rcd_canapr as cantidad,J.AUX_CODAUX as codprov, J.AUX_NOMAUX as nomprov
	                                    FROM REQUERIMIENTO_COMPRA_RCO A
	                                    LEFT JOIN REQUERIMIENTO_COMPRA_RCD B ON A.cia_codcia=B.cia_codcia AND A.suc_codsuc=B.suc_codsuc AND A.rco_numrco=B.rco_numrco
	                                    Left Join ORDEN_COMPRA_OCC I on a.cia_codcia=i.CIA_CODCIA and a.suc_codsuc=i.suc_codsuc and a.ocm_corocm=i.ocm_corocm
                                        Left Join AUXILIARES_AUX J on i.cia_codcia=j.CIA_CODCIA and i.aux_codaux=j.AUX_CODAUX
	                                    WHERE A.rco_numrco =@Rco_numero", new { Rco_numero });
        }
    }
}
