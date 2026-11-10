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
            using var reader = command.ExecuteNonQueryAsync();
            if (await reader.ReadAsync())
            {
                string longUrl = reader["LongUrl"].ToString()!;
                string shortCode = reader["ShortCode"].ToString()!;
                string createdAd = reader["CreatedAd"].ToString()!; 

                return new IngresoUrl(LongUrl, ShortCode, CreatedAt);
            }
        }

        public async Task<string> ClickUrlAsync(string shortCode)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_GeturlByCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ShortCode", shortCode);

            await connection.OpenAsync();

            using var reader = await command.ExecuteNonQueryAsync();

            if (await reader.ReadAsync())
            {
                string longUrl = reader["LongUrl"].ToString()!;
                int clicks = Convert.ToInt32(reader["Clicks"]);

                return new ClickUrl(longUrl, click);
            }
        }
    }
}