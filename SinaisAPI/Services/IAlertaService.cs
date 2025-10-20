using SinaisAPI.DTOs;

namespace SinaisAPI.Services
{
    public interface IAlertaService
    {
        Task<IEnumerable<AlertaDTO>> GetAllAlertasAsync();
        Task<AlertaDTO?> GetAlertaByIdAsync(int id);
        Task<IEnumerable<AlertaDTO>> GetAlertasByUsuarioAsync(int usuarioId);
        Task<AlertaDTO> CreateAlertaAsync(CreateAlertaDTO createAlertaDTO);
        Task<AlertaDTO?> UpdateAlertaAsync(int id, UpdateAlertaDTO updateAlertaDTO);
        Task<bool> DeleteAlertaAsync(int id);
        Task<IEnumerable<AlertaDTO>> GetAlertasNaoEnviadosAsync();
        Task<IEnumerable<AlertaDTO>> GetAlertasByTipoAsync(string tipo);
        Task<IEnumerable<AlertaDTO>> GetAlertasByPrioridadeAsync(string prioridade);
        Task<bool> MarcarAlertaComoEnviadoAsync(int id);
    }
}
