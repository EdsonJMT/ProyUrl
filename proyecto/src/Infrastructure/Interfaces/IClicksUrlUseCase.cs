namespace proyecto.src.Infrastructure.Interfaces

{
    public interface IClicksUrlUseCase
    {
        Task<ClicksUrlResponse> ExecuteAsync(ClicksUrlRequest request);
    }
}