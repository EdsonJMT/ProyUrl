namespace proyecto.src.Application.DTO.Responses
{
    public class IngresoUrlResponse{
        public string ShortCode  { get; set; } = string.Empty;
        public string FullUrl { get; set; } = string.Empty;
        public DateTime? CreatedFecha  { get; set; }
    }   
}