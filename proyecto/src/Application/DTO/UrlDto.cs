namespace proyecto.src.Application.DTO
{
    public class UrlDto{
        public string FullUrl { get; set; } = string.Empty;
        public string? ShortUrl { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}