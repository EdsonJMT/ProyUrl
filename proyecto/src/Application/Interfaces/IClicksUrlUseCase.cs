using proyecto.src.Application.DTO.Request;
using proyecto.src.Application.DTO.Responses;

namespace proyecto.src.Infrastructure.Interfaces
{
    public interface IClicksUrlUseCase
    {
        Task<ClicksUrlResponse> ClickUrl(ClicksUrlRequest request);
    }
}