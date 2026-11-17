using System.Data;
using Microsoft.Data.SqlClient; // <-- Tu paquete correcto
using proyecto.src.Domain.Entities;
using proyecto.src.Domain.Interfaces; // <-- Implementa la interfaz del Dominio

namespace proyecto.src.Infrastrucutre.Persistence.Repository // (Ajusta tu namespace si es necesario)
{
    public class UrlRepository : IUrlRepository
    {
        private readonly string _connectionString;

        public UrlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // 1. Obtener todas (Retorna IEnumerable<Url>)
        public async Task<IEnumerable<Url>> ObtenerTodasAsync()
        {
            var urlsList = new List<Url>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_ListarUrls", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int id = reader.GetInt32(reader.GetOrdinal("Id"));
                string shortCode = reader["ShortCode"].ToString()!;
                string longUrl = reader["LongUrl"].ToString()!;
                DateTime createdAt = reader.GetDateTime(reader.GetOrdinal("CreatedAd"));

                // Retorna la Entidad de Dominio
                urlsList.Add(new Url(id, longUrl, shortCode, createdAt));
            }

            return urlsList;
        }

        // 2. Insertar (Recibe o procesa y retorna la Entidad de Dominio creada)
        public async Task<Url> InsertUrlAsync(string fullUrl, string shortCode)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_InserUrl", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Longurl", fullUrl);
            command.Parameters.AddWithValue("@ShortCode", shortCode);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                int id = reader.GetInt32(reader.GetOrdinal("Id")); // Asegúrate de leer el Id si lo devuelve tu SP
                string dbLongUrl = reader["LongUrl"].ToString()!;
                string dbShortCode = reader["ShortCode"].ToString()!;
                DateTime createdAt = reader.GetDateTime(reader.GetOrdinal("CreatedAd"));

                // Retorna la Entidad de Dominio pura
                return new Url(id, dbLongUrl, dbShortCode, createdAt);
            }

            throw new Exception("No se pudo registrar la URL.");
        }

        // 3. Buscar por código / Clic (Retorna la Entidad de Dominio o null)
        public async Task<Url?> ClickUrlAsync(string shortCodeParam)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_GeturlByCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ShortCode", shortCodeParam);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                int id = reader.GetInt32(reader.GetOrdinal("Id"));
                string longUrl = reader["LongUrl"].ToString()!;
                string shortCode = reader["ShortCode"].ToString()!;
                DateTime createdAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                // (Nota: Si tu SP devuelve clics y tu entidad necesita guardarlos, los puedes gestionar aquí)

                // Retorna la Entidad de Dominio
                return new Url(id, longUrl, shortCode, createdAt);
            }

            return null;
        }
    }
}