using proyecto.src.Application.DTO.Request;
using proyecto.src.Application.DTO.Responses;

namespace proyecto.src.Application.Interfaces

{
    public interface IListaUrlUseCase
    {
        Task<ListaUrlResponse> ListaUrl();
    }
}