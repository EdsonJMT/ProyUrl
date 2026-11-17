using proyecto.src.Domain.Entities;

namespace proyecto.src.Domain.Interfaces
{
    public interface IUrlRepository
    {
        Task<IEnumerable<Url>> ObtenerTodasAsync();
        Task<Url?> ClickUrlAsync(string shortCodeParam);
        Task <Url> InsertUrlAsync(string fullUrl, string shortCode);
    }
}