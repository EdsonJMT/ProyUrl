namespace proyecto.src.Infrastructure.DTO
{
    public record IngresoUrlDto(
        string LongUrl, 
        string ShortCode, 
        string CreatedAt
    );
}