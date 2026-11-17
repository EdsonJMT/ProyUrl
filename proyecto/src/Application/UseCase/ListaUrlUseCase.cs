using proyecto.src.Application.DTO.Responses;
using proyecto.src.Application.Interfaces;
using proyecto.src.Application.Mappers;
using proyecto.src.Domain.Interfaces;

namespace proyecto.src.Application.UseCases
{
    public class ListaUrlUseCase : IListaUrlUseCase
    {
        private readonly IUrlRepository _urlRepository;

        public ListaUrlUseCase(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<ListaUrlResponse> ListaUrl()
        {
            // 1. Obtener la lista de entidades de dominio desde el repositorio
            var urlEntities = await _urlRepository.ObtenerTodasAsync();

            // 2. Mapear la colección completa al ListaUrlResponse usando el mapper
            return urlEntities.ToResponseList();
        }
    }
}