using proyecto.src.Application.DTO.Request;
using proyecto.src.Application.DTO.Responses;
using proyecto.src.Application.Mappers;
using proyecto.src.Domain.Entities;
using proyecto.src.Domain.Interfaces;

namespace proyecto.src.Application.UseCases
{
    public class InsertarUrlUseCase
    {
        private readonly IUrlRepository _urlRepository;

        public InsertarUrlUseCase(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<IngresoUrlResponse> EjecutarAsync(IngresoUrlRequest request)
        {
            
            string shortCode = Guid.NewGuid().ToString("N").Substring(0, 6);

            // 2. Crear la Entidad de Dominio (aquí se disparan las validaciones de tu constructor)
            Url urlDomain = await _urlRepository.InsertUrlAsync(request.FullUrl, shortCode);

            return urlDomain.ToResponseInsert();
        }
    }
}