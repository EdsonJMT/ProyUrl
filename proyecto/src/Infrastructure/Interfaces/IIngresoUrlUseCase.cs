using proyecto.src.Interface.Request;
using proyecto.src.Interface.Responses;

namespace proyecto.src.Infrastructure.Interfaces

{
    public interface IIngresoUrlUseCase
    {
        Task<IngresoUrlResponse> ExecuteAsync(IngresoUrlRequest request);
    }
}