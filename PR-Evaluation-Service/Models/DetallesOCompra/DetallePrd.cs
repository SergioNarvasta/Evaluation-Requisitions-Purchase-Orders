using Dapper;
using HDProjectWeb.Models.Detalles;
using Microsoft.Data.SqlClient;

namespace ProjectWeb_DRA.Models.DetallesOCompra
{
    public class DetallePrd
    {
        //Model for details Oc  
        public int Occ_codepk            { get; set; }
        public string Ocd_corite            { get; set; }
        public int  Prd_codepk            { get; set; }
        public int  Tin_codtin            { get; set; }
        public string  Prd_desprd            { get; set; }
        public string  Ocd_especi            { get; set; }
        public double  Ocd_cansol            { get; set; }
        public double  Ocd_poraju            { get; set; }
        public double Ocd_canaju            { get; set; }
        public int     OCD_Ume_Compra         { get; set; }
        public int    Ume_codepk            { get; set; }
        public double  Ocd_preuni            { get; set; }
        public double  Ocd_impbru            { get; set; }
        public double  Ocd_impdes            { get; set; }
        public double  Ocd_impigv            { get; set; }
        public double  Ocd_imptot            { get; set; }
        public double  OCD_Cantidad_Real     { get; set; }
        public double  OCD_Cantidad_Atendida { get; set; }
        public double  OCD_Cantidad_Saldo    { get; set; }
        public double  OCD_Valor_Venta       { get; set; }
        
    }
    public interface IDetallePrdService
    {
        Task<IEnumerable<DetallePrd>> GetDetallePrd(int Occ_codepk);
    }
    public class DetallePrdService : IDetallePrdService
    {
        private readonly string connectionString;
        public DetallePrdService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<DetallePrd>> GetDetallePrd(int Occ_codepk)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<DetallePrd>(@"Select 
                 A.occ_codepk,A.ocd_corite, A.prd_codepk , B.tin_codtin , B.prd_desprd, A.ocd_especi, A.ocd_cansol, a.ocd_poraju, a.ocd_canaju ,
                 A.equ_codequ as OCD_Ume_Compra, A.ume_codepk ,A.ocd_preuni ,A.ocd_impbru ,A.ocd_impdes ,A.ocd_impigv ,A.ocd_imptot,
                 (Case When Isnull(c.equ_canequ,0)<=0 Then 0 Else 
                       Round((((Isnull(a.ocd_cansol,0)+Isnull(a.ocd_canaju,0)) * Isnull(c.equ_canori,0)) / Isnull(c.equ_canequ,0)),3) End) As OCD_Cantidad_Real ,
                         A.ocd_canate as OCD_Cantidad_Atendida,
                 ((Case When Isnull(c.equ_canequ,0)<=0 Then 0 Else 
                        Round((((Isnull(a.ocd_cansol,0)+Isnull(a.ocd_canaju,0)) * Isnull(c.equ_canori,0)) / Isnull(c.equ_canequ,0)),3) End)-ISNULL(a.ocd_canate,0)) As OCD_Cantidad_Saldo,
                 Round(Isnull(a.ocd_impbru,0) - (Isnull(a.ocd_impdes,0)),3) As OCD_Valor_Venta
                 From OCOMPRA_OCD A
                 Left Join PRODUCTOS_PRD B     ON (a.cia_codcia=b.cia_codcia And a.prd_codepk=b.prd_codepk)
                 Left Join UEQUIVALENCIA_EQU C ON (A.cia_codcia=C.cia_codcia AND A.ume_codepk=C.ume_codepk and A.equ_codequ=C.equ_codequ)  
                 LEFT JOIN UMEDIDA_UME D       ON (A.ume_codepk=D.ume_codepk AND A.cia_codcia=D.cia_codcia)
                 WHERE A.occ_codepk = @Occ_codepk ", new { Occ_codepk });
        }
    }
}
