using SinaisAPI.DTOs;

namespace SinaisAPI.Services
{
    public interface ISessaoApoioService
    {
        Task<IEnumerable<SessaoApoioDTO>> GetAllSessoesAsync();
        Task<SessaoApoioDTO?> GetSessaoByIdAsync(int id);
        Task<IEnumerable<SessaoApoioDTO>> GetSessoesByUsuarioAsync(int usuarioId);
        Task<SessaoApoioDTO> CreateSessaoAsync(CreateSessaoApoioDTO createSessaoDTO);
        Task<SessaoApoioDTO?> UpdateSessaoAsync(int id, UpdateSessaoApoioDTO updateSessaoDTO);
        Task<bool> DeleteSessaoAsync(int id);
        Task<IEnumerable<SessaoApoioDTO>> GetSessoesByTipoAsync(string tipo);
        Task<IEnumerable<SessaoApoioDTO>> GetSessoesByPeriodoAsync(DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<SessaoApoioDTO>> GetSessoesRealizadasAsync();
    }
}
