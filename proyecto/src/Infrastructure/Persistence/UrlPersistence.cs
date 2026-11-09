using Microsoft.Data.SqlClient;
using System.Data;

namespace proyecto.src.Infrastructure.Presistence
{
    public class UrlPersistence
    {
        private readonly string _connectionString;

        public UrlPersistence(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task InsertUrlAsync(string fullUrl, string shortCode)
        {
            using var connection = new SqlConnection(_connectionString);

            using var command = new SqlCommand("sp_InserUrl", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Longurl", fullUrl);
            command.Parameters.AddWithValue("@ShortCode", shortCode);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}