using SinaisAPI.DTOs;

namespace SinaisAPI.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync();
        Task<UsuarioDTO?> GetUsuarioByIdAsync(int id);
        Task<UsuarioDTO> CreateUsuarioAsync(CreateUsuarioDTO createUsuarioDTO);
        Task<UsuarioDTO?> UpdateUsuarioAsync(int id, UpdateUsuarioDTO updateUsuarioDTO);
        Task<bool> DeleteUsuarioAsync(int id);
        Task<IEnumerable<UsuarioDTO>> SearchUsuariosAsync(string searchTerm);
        Task<IEnumerable<UsuarioDTO>> GetUsuariosAtivosAsync();
        Task<UsuarioDTO?> GetUsuarioByEmailAsync(string email);
    }
}
