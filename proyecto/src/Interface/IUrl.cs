using System.Runtime.InteropServices;
using proyecto.src.Application.DTO.Request;
using proyecto.src.Application.DTO.Responses;

namespace proyecto.src.Interface
{
    public interface IUrl
    {
        Task<IngresoUrlResponse> IngresoUrl(IngresoUrlRequest request);

        Task<ClicksUrlResponse> ClickUrl(ClicksUrlRequest request);

        Task<ListaUrlResponse> ListaUrl();
    }
}