using proyecto.src.Application.DTO.Request;
using proyecto.src.Application.DTO.Responses;

namespace proyecto.src.Infrastructure.Interfaces

{
    public interface IIngresoUrlUseCase
    {
        Task<IngresoUrlResponse> IngresoUrl(IngresoUrlRequest request);
    }
}