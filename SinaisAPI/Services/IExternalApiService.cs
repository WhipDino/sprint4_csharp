namespace SinaisAPI.Services
{
    public interface IExternalApiService
    {
        Task<string> GetOpenAIResponseAsync(string prompt);
        Task<object> GetCepInfoAsync(string cep);
        Task<string> GetMotivationalMessageAsync();
        Task<string> GetCrisisSupportMessageAsync();
        Task<object> GetHealthResourcesAsync(string city);
    }
}
