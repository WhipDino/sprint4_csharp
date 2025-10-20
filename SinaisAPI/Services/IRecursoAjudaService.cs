using SinaisAPI.DTOs;

namespace SinaisAPI.Services
{
    public interface IRecursoAjudaService
    {
        Task<IEnumerable<RecursoAjudaDTO>> GetAllRecursosAsync();
        Task<RecursoAjudaDTO?> GetRecursoByIdAsync(int id);
        Task<RecursoAjudaDTO> CreateRecursoAsync(CreateRecursoAjudaDTO createRecursoDTO);
        Task<RecursoAjudaDTO?> UpdateRecursoAsync(int id, UpdateRecursoAjudaDTO updateRecursoDTO);
        Task<bool> DeleteRecursoAsync(int id);
        Task<IEnumerable<RecursoAjudaDTO>> GetRecursosByCategoriaAsync(string categoria);
        Task<IEnumerable<RecursoAjudaDTO>> GetRecursosAtivosAsync();
        Task<IEnumerable<RecursoAjudaDTO>> SearchRecursosAsync(string searchTerm);
        Task<IEnumerable<RecursoAjudaDTO>> GetRecursosByPrioridadeAsync(string prioridade);
        Task<bool> IncrementarVisualizacaoAsync(int id);
    }
}
