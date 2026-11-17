using proyecto.src.Application.DTO;
using proyecto.src.Application.DTO.Responses;
using proyecto.src.Domain.Entities;

namespace proyecto.src.Application.Mappers
{
    public static class UrlMapper
    {
        // 1. Mapeo para el detalle del Clic (Nota: como el Entity no tiene Clicks, se lo pasamos opcional o lo manejamos)
        public static ClickUrlResponse ToResponseClick(this Url urlEntity, int? clicks = null)
        {
            return new ClickUrlResponse
            {
                FullUrl = urlEntity.FullUrl ?? string.Empty,
                Clicks = clicks
            };
        }

        // 2. Mapeo para la Inserción (Traduce de la Entidad al Response de Creación)
        public static IngresoUrlResponse ToResponseInsert(this Url urlEntity)
        {
            return new IngresoUrlResponse
            {
                ShortCode = urlEntity.ShortUrl ?? string.Empty,
                FullUrl = urlEntity.FullUrl ?? string.Empty,
                CreatedFecha = urlEntity.CreateAt
            };
        }

        // 3. Mapeo para Listar todas (Convierte la lista de Entidades a ListaUrlResponse con sus UrlDto)
        public static ListaUrlResponse ToResponseList(this IEnumerable<Url> urlEntities)
        {
            var urlDtos = urlEntities.Select(u => new UrlDto
            {
                FullUrl = u.FullUrl ?? string.Empty,
                ShortUrl = u.ShortUrl,
                CreateAt = u.CreateAt
            }).ToList();

            return new ListaUrlResponse
            {
                Url = urlDtos
            };
        }
    }
}