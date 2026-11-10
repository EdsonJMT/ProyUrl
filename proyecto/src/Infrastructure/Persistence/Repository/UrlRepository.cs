using Microsoft.Data.SqlClient;
using System.Data;
using proyecto.src.Application.DTO.Responses; 
using proyecto.src.Application.DTO;

namespace proyecto.src.Infrastructure.Presistence.Repository
{
    public class UrlRepository
    {
        private readonly string _connectionString;

        public UrlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Si este método inserta y además devuelve el registro creado:
        public async Task<IngresoUrlResponse> InsertUrlAsync(string fullUrl, string shortCode)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_InserUrl", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Longurl", fullUrl);
            command.Parameters.AddWithValue("@ShortCode", shortCode);

            await connection.OpenAsync();
            
            // CORRECCIÓN 1: Usamos ExecuteReaderAsync porque el SP devuelve filas
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                string dbLongUrl = reader["LongUrl"].ToString()!;
                string dbShortCode = reader["ShortCode"].ToString()!;
                DateTime createdAt = reader.GetDateTime(reader.GetOrdinal("CreatedAd"));

                return new IngresoUrlResponse{FullUrl = dbLongUrl, ShortCode = dbShortCode, CreatedFecha = createdAt};
            }

            throw new Exception("No se pudo registrar la URL.");
        }

        public async Task<ClicksUrlResponse?> ClickUrlAsync(string shortCodeParam)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_GeturlByCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ShortCode", shortCodeParam);

            await connection.OpenAsync();

            // CORRECCIÓN 2: Debe ser ExecuteReaderAsync para poder leer las columnas devueltas
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                string longUrl = reader["LongUrl"].ToString()!;
                int clicks = Convert.ToInt32(reader["Clicks"]);

                return new ClicksUrlResponse { FullUrl = longUrl, Clicks = clicks };
            }

            return null;
        }

        public async Task<ListaUrlResponse> ListaUrlAsync()
        {
            var urlsList = new List<Url>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_ListarUrls", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                string shortCode = reader["ShortCode"].ToString()!;
                string longUrl = reader["LongUrl"].ToString()!;
                DateTime createdAt = reader.GetDateTime(reader.GetOrdinal("CreatedAd"));

                urlsList.Add(new Url 
                { 
                    FullUrl = longUrl, 
                    ShortUrl = shortCode,
                    CreateAt = createdAt

                });
            }

            // Retornamos el objeto contenedor que definiste
            return new ListaUrlResponse 
            { 
                Url = urlsList 
            };
        }
    }
}