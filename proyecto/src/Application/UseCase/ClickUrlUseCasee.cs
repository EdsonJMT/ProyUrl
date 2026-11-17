using proyecto.src.Application.DTO.Request;
using proyecto.src.Application.DTO.Responses;
using proyecto.src.Application.Mappers;
using proyecto.src.Domain.Interfaces;

namespace proyecto.src.Application.UseCases
{
    public class ClickUrlUseCase
    {
        private readonly IUrlRepository _urlRepository;

        public ClickUrlUseCase(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<ClickUrlResponse> EjecutarAsync(ClicksUrlRequest request)
        {
            // 1. Buscar la entidad de dominio en el repositorio
            var urlDomain = await _urlRepository.ClickUrlAsync(request.ShortCode);

            // 2. Validar regla de negocio: si no existe, lanzamos excepción
            if (urlDomain == null)
            {
                throw new KeyNotFoundException("La URL corta especificada no existe.");
            }

            // 3. (Opcional) Si tu base de datos calcula o actualiza los clics, los pasas aquí
            int? totalClicks = 1; 

            // 4. Mapear a tu ClickUrlResponse usando el mapper
            return urlDomain.ToResponseClick(totalClicks);
        }
    }
}