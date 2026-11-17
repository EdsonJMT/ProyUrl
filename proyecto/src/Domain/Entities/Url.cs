namespace proyecto.src.Domain.Entities
{
    public class Url
    {
        public int Id { get; private set; } 
        public string? FullUrl { get; private set; } = string.Empty;
        public string? ShortUrl { get; private set; } = string.Empty;
        public DateTime? CreateAt { get; private set; }

        // Constructor 1: Para crear URLs nuevas (asigna DateTime.UtcNow)
        public Url(string fullUrl, string shortUrl)
        {
            if (string.IsNullOrEmpty(fullUrl))
            {
                throw new ArgumentException("La URL original no puede estar vacia");
            }

            if (string.IsNullOrEmpty(shortUrl))
            {
                throw new ArgumentException("El codigo corto no puede estar vacio");
            }

            FullUrl = fullUrl;
            ShortUrl = shortUrl;
            CreateAt = DateTime.UtcNow;
        }

        // Constructor 2: Para reconstruir desde la Base de Datos (Infrastructure)
        public Url(int id, string fullUrl, string shortUrl, DateTime? createAt)
        {
            Id = id;
            FullUrl = fullUrl;
            ShortUrl = shortUrl;
            CreateAt = createAt;
        }
    }
}