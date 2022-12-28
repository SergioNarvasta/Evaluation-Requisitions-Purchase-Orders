using Dapper;
using HDProjectWeb.Services;
using System.Data.SqlClient;

namespace HDProjectWeb.Models.Helps
{
    public class Disciplina
    {
        public string Cia_codcia { get; set; }
        public string Dis_coddis { get; set; }
        public string Dis_deslar { get; set; }
    }
    public interface IDisciplinaService
    {
        Task<IEnumerable<Disciplina>> ListaAyudaDisciplina();
    }
    public class DisciplinaService : IDisciplinaService
    {
        private readonly string connectionString;

        public DisciplinaService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<Disciplina>> ListaAyudaDisciplina()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Disciplina>(@"SELECT cia_codcia,dis_codepk as dis_coddis,dis_deslar FROM DISCIPLINAS_DIS 
                                                             WHERE cia_codcia =1 AND dis_estado =1");
        }
    }
   
}
