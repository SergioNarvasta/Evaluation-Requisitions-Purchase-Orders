using Dapper;
using HDProjectWeb.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace HDProjectWeb.Services
{
    public interface IRepositorioUsuario
    {
        Task<int> CrearUsuario(_Login usuario);
    }
    public class RepositorioUsuario :IRepositorioUsuario
    {
        private readonly string connectionString;

        public RepositorioUsuario (IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task <int> CrearUsuario (_Login usuario)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@" "); //INSERT INTO 
            return id;
        }

    }
}
