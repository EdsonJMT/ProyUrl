using System.Runtime.InteropServices;
using proyecto.src.Interface.Request;
using proyecto.src.Interface.Responses;

namespace proyecto.src.Interface
{
    public interface IUrl
    {
        Task<IngresoUrlResponse> IngresoUrl(IngresoUrlRequest request);

        Task<ClickUrlResponse> ClickUrl(ClickUrlRequest request);
    }
}