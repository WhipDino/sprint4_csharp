using SinaisAPI.DTOs;

namespace SinaisAPI.Services
{
    public interface IRelatorioProgressoService
    {
        Task<IEnumerable<RelatorioProgressoDTO>> GetAllRelatoriosAsync();
        Task<RelatorioProgressoDTO?> GetRelatorioByIdAsync(int id);
        Task<IEnumerable<RelatorioProgressoDTO>> GetRelatoriosByUsuarioAsync(int usuarioId);
        Task<RelatorioProgressoDTO> CreateRelatorioAsync(CreateRelatorioProgressoDTO createRelatorioDTO);
        Task<RelatorioProgressoDTO?> UpdateRelatorioAsync(int id, UpdateRelatorioProgressoDTO updateRelatorioDTO);
        Task<bool> DeleteRelatorioAsync(int id);
        Task<IEnumerable<RelatorioProgressoDTO>> GetRelatoriosByTipoAsync(string tipo);
        Task<IEnumerable<RelatorioProgressoDTO>> GetRelatoriosByPeriodoAsync(DateTime dataInicio, DateTime dataFim);
        Task<RelatorioProgressoDTO?> GetUltimoRelatorioUsuarioAsync(int usuarioId);
        Task<IEnumerable<RelatorioProgressoDTO>> GetRelatoriosComProgressoAsync();
    }
}
