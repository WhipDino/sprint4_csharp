using Microsoft.EntityFrameworkCore;
using SinaisAPI.Data;
using SinaisAPI.DTOs;
using SinaisAPI.Models;

namespace SinaisAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly SinaisDbContext _context;

        public UsuarioService(SinaisDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync()
        {
            var usuarios = await _context.Usuarios
                .Select(u => new UsuarioDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Telefone = u.Telefone,
                    DataNascimento = u.DataNascimento,
                    Genero = u.Genero,
                    DataCadastro = u.DataCadastro,
                    Ativo = u.Ativo,
                    Observacoes = u.Observacoes
                })
                .ToListAsync();

            return usuarios;
        }

        public async Task<UsuarioDTO?> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.Id == id)
                .Select(u => new UsuarioDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Telefone = u.Telefone,
                    DataNascimento = u.DataNascimento,
                    Genero = u.Genero,
                    DataCadastro = u.DataCadastro,
                    Ativo = u.Ativo,
                    Observacoes = u.Observacoes
                })
                .FirstOrDefaultAsync();

            return usuario;
        }

        public async Task<UsuarioDTO> CreateUsuarioAsync(CreateUsuarioDTO createUsuarioDTO)
        {
            var usuario = new Usuario
            {
                Nome = createUsuarioDTO.Nome,
                Email = createUsuarioDTO.Email,
                Telefone = createUsuarioDTO.Telefone,
                DataNascimento = createUsuarioDTO.DataNascimento,
                Genero = createUsuarioDTO.Genero,
                Observacoes = createUsuarioDTO.Observacoes,
                DataCadastro = DateTime.Now,
                Ativo = true
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                DataNascimento = usuario.DataNascimento,
                Genero = usuario.Genero,
                DataCadastro = usuario.DataCadastro,
                Ativo = usuario.Ativo,
                Observacoes = usuario.Observacoes
            };
        }

        public async Task<UsuarioDTO?> UpdateUsuarioAsync(int id, UpdateUsuarioDTO updateUsuarioDTO)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return null;

            if (updateUsuarioDTO.Nome != null)
                usuario.Nome = updateUsuarioDTO.Nome;
            if (updateUsuarioDTO.Email != null)
                usuario.Email = updateUsuarioDTO.Email;
            if (updateUsuarioDTO.Telefone != null)
                usuario.Telefone = updateUsuarioDTO.Telefone;
            if (updateUsuarioDTO.DataNascimento.HasValue)
                usuario.DataNascimento = updateUsuarioDTO.DataNascimento.Value;
            if (updateUsuarioDTO.Genero != null)
                usuario.Genero = updateUsuarioDTO.Genero;
            if (updateUsuarioDTO.Observacoes != null)
                usuario.Observacoes = updateUsuarioDTO.Observacoes;
            if (updateUsuarioDTO.Ativo.HasValue)
                usuario.Ativo = updateUsuarioDTO.Ativo.Value;

            await _context.SaveChangesAsync();

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                DataNascimento = usuario.DataNascimento,
                Genero = usuario.Genero,
                DataCadastro = usuario.DataCadastro,
                Ativo = usuario.Ativo,
                Observacoes = usuario.Observacoes
            };
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UsuarioDTO>> SearchUsuariosAsync(string searchTerm)
        {
            var usuarios = await _context.Usuarios
                .Where(u => u.Nome.Contains(searchTerm) || 
                           u.Email.Contains(searchTerm) || 
                           u.Telefone.Contains(searchTerm))
                .Select(u => new UsuarioDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Telefone = u.Telefone,
                    DataNascimento = u.DataNascimento,
                    Genero = u.Genero,
                    DataCadastro = u.DataCadastro,
                    Ativo = u.Ativo,
                    Observacoes = u.Observacoes
                })
                .ToListAsync();

            return usuarios;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetUsuariosAtivosAsync()
        {
            var usuarios = await _context.Usuarios
                .Where(u => u.Ativo)
                .Select(u => new UsuarioDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Telefone = u.Telefone,
                    DataNascimento = u.DataNascimento,
                    Genero = u.Genero,
                    DataCadastro = u.DataCadastro,
                    Ativo = u.Ativo,
                    Observacoes = u.Observacoes
                })
                .ToListAsync();

            return usuarios;
        }

        public async Task<UsuarioDTO?> GetUsuarioByEmailAsync(string email)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.Email == email)
                .Select(u => new UsuarioDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Telefone = u.Telefone,
                    DataNascimento = u.DataNascimento,
                    Genero = u.Genero,
                    DataCadastro = u.DataCadastro,
                    Ativo = u.Ativo,
                    Observacoes = u.Observacoes
                })
                .FirstOrDefaultAsync();

            return usuario;
        }
    }
}
