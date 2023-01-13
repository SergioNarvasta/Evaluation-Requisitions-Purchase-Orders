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
        public string Nombre { get; set; }
        public string Archivo { get; set; }
        public string CodArchivo { get; set; }
    }
    public interface IAdjuntosService
    {
        Task<IEnumerable<Adjuntos>> GetAdjuntos(string Rco_numero);
    }
    public class AdjuntosService : IAdjuntosService
    {
        private readonly string connectionString;
        public AdjuntosService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<Adjuntos>> GetAdjuntos(string Rco_numero)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Adjuntos>(@"SELECT rcf_corite as item ,rcf_nomarc as nombre,rcf_file as archivo, rcf_codarc as codarchivo 
                     FROM REQ_REQUI_FILES_RCF B LEFT JOIN REQ_REQUI_COMPRA_RCO A ON A.cia_codcia=B.cia_codcia AND A.rco_codepk=B.rco_codepk
                     WHERE A.rco_numrco =@Rco_numero", new { Rco_numero });
        }
    }
}
