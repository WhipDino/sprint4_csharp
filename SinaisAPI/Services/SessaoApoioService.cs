using Microsoft.EntityFrameworkCore;
using SinaisAPI.Data;
using SinaisAPI.DTOs;
using SinaisAPI.Models;

namespace SinaisAPI.Services
{
    public class SessaoApoioService : ISessaoApoioService
    {
        private readonly SinaisDbContext _context;

        public SessaoApoioService(SinaisDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SessaoApoioDTO>> GetAllSessoesAsync()
        {
            var sessoes = await _context.SessoesApoio
                .Include(s => s.Usuario)
                .Select(s => new SessaoApoioDTO
                {
                    Id = s.Id,
                    UsuarioId = s.UsuarioId,
                    DataSessao = s.DataSessao,
                    TipoSessao = s.TipoSessao,
                    TemaSessao = s.TemaSessao,
                    Descricao = s.Descricao,
                    Status = s.Status,
                    Observacoes = s.Observacoes,
                    DuracaoMinutos = s.DuracaoMinutos,
                    Facilitador = s.Facilitador
                })
                .ToListAsync();

            return sessoes;
        }

        public async Task<SessaoApoioDTO?> GetSessaoByIdAsync(int id)
        {
            var sessao = await _context.SessoesApoio
                .Include(s => s.Usuario)
                .Where(s => s.Id == id)
                .Select(s => new SessaoApoioDTO
                {
                    Id = s.Id,
                    UsuarioId = s.UsuarioId,
                    DataSessao = s.DataSessao,
                    TipoSessao = s.TipoSessao,
                    TemaSessao = s.TemaSessao,
                    Descricao = s.Descricao,
                    Status = s.Status,
                    Observacoes = s.Observacoes,
                    DuracaoMinutos = s.DuracaoMinutos,
                    Facilitador = s.Facilitador
                })
                .FirstOrDefaultAsync();

            return sessao;
        }

        public async Task<IEnumerable<SessaoApoioDTO>> GetSessoesByUsuarioAsync(int usuarioId)
        {
            var sessoes = await _context.SessoesApoio
                .Where(s => s.UsuarioId == usuarioId)
                .Select(s => new SessaoApoioDTO
                {
                    Id = s.Id,
                    UsuarioId = s.UsuarioId,
                    DataSessao = s.DataSessao,
                    TipoSessao = s.TipoSessao,
                    TemaSessao = s.TemaSessao,
                    Descricao = s.Descricao,
                    Status = s.Status,
                    Observacoes = s.Observacoes,
                    DuracaoMinutos = s.DuracaoMinutos,
                    Facilitador = s.Facilitador
                })
                .OrderByDescending(s => s.DataSessao)
                .ToListAsync();

            return sessoes;
        }

        public async Task<SessaoApoioDTO> CreateSessaoAsync(CreateSessaoApoioDTO createSessaoDTO)
        {
            var sessao = new SessaoApoio
            {
                UsuarioId = createSessaoDTO.UsuarioId,
                DataSessao = createSessaoDTO.DataSessao,
                TipoSessao = createSessaoDTO.TipoSessao,
                TemaSessao = createSessaoDTO.TemaSessao,
                Descricao = createSessaoDTO.Descricao,
                Observacoes = createSessaoDTO.Observacoes,
                DuracaoMinutos = createSessaoDTO.DuracaoMinutos,
                Facilitador = createSessaoDTO.Facilitador,
                Status = "Agendada"
            };

            _context.SessoesApoio.Add(sessao);
            await _context.SaveChangesAsync();

            return new SessaoApoioDTO
            {
                Id = sessao.Id,
                UsuarioId = sessao.UsuarioId,
                DataSessao = sessao.DataSessao,
                TipoSessao = sessao.TipoSessao,
                TemaSessao = sessao.TemaSessao,
                Descricao = sessao.Descricao,
                Status = sessao.Status,
                Observacoes = sessao.Observacoes,
                DuracaoMinutos = sessao.DuracaoMinutos,
                Facilitador = sessao.Facilitador
            };
        }

        public async Task<SessaoApoioDTO?> UpdateSessaoAsync(int id, UpdateSessaoApoioDTO updateSessaoDTO)
        {
            var sessao = await _context.SessoesApoio.FindAsync(id);
            if (sessao == null) return null;

            if (updateSessaoDTO.DataSessao.HasValue)
                sessao.DataSessao = updateSessaoDTO.DataSessao.Value;
            if (updateSessaoDTO.TipoSessao != null)
                sessao.TipoSessao = updateSessaoDTO.TipoSessao;
            if (updateSessaoDTO.TemaSessao != null)
                sessao.TemaSessao = updateSessaoDTO.TemaSessao;
            if (updateSessaoDTO.Descricao != null)
                sessao.Descricao = updateSessaoDTO.Descricao;
            if (updateSessaoDTO.Status != null)
                sessao.Status = updateSessaoDTO.Status;
            if (updateSessaoDTO.Observacoes != null)
                sessao.Observacoes = updateSessaoDTO.Observacoes;
            if (updateSessaoDTO.DuracaoMinutos.HasValue)
                sessao.DuracaoMinutos = updateSessaoDTO.DuracaoMinutos.Value;
            if (updateSessaoDTO.Facilitador != null)
                sessao.Facilitador = updateSessaoDTO.Facilitador;

            await _context.SaveChangesAsync();

            return new SessaoApoioDTO
            {
                Id = sessao.Id,
                UsuarioId = sessao.UsuarioId,
                DataSessao = sessao.DataSessao,
                TipoSessao = sessao.TipoSessao,
                TemaSessao = sessao.TemaSessao,
                Descricao = sessao.Descricao,
                Status = sessao.Status,
                Observacoes = sessao.Observacoes,
                DuracaoMinutos = sessao.DuracaoMinutos,
                Facilitador = sessao.Facilitador
            };
        }

        public async Task<bool> DeleteSessaoAsync(int id)
        {
            var sessao = await _context.SessoesApoio.FindAsync(id);
            if (sessao == null) return false;

            _context.SessoesApoio.Remove(sessao);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SessaoApoioDTO>> GetSessoesByTipoAsync(string tipo)
        {
            var sessoes = await _context.SessoesApoio
                .Where(s => s.TipoSessao == tipo)
                .Select(s => new SessaoApoioDTO
                {
                    Id = s.Id,
                    UsuarioId = s.UsuarioId,
                    DataSessao = s.DataSessao,
                    TipoSessao = s.TipoSessao,
                    TemaSessao = s.TemaSessao,
                    Descricao = s.Descricao,
                    Status = s.Status,
                    Observacoes = s.Observacoes,
                    DuracaoMinutos = s.DuracaoMinutos,
                    Facilitador = s.Facilitador
                })
                .OrderByDescending(s => s.DataSessao)
                .ToListAsync();

            return sessoes;
        }

        public async Task<IEnumerable<SessaoApoioDTO>> GetSessoesByPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            var sessoes = await _context.SessoesApoio
                .Where(s => s.DataSessao >= dataInicio && s.DataSessao <= dataFim)
                .Select(s => new SessaoApoioDTO
                {
                    Id = s.Id,
                    UsuarioId = s.UsuarioId,
                    DataSessao = s.DataSessao,
                    TipoSessao = s.TipoSessao,
                    TemaSessao = s.TemaSessao,
                    Descricao = s.Descricao,
                    Status = s.Status,
                    Observacoes = s.Observacoes,
                    DuracaoMinutos = s.DuracaoMinutos,
                    Facilitador = s.Facilitador
                })
                .OrderBy(s => s.DataSessao)
                .ToListAsync();

            return sessoes;
        }

        public async Task<IEnumerable<SessaoApoioDTO>> GetSessoesRealizadasAsync()
        {
            var sessoes = await _context.SessoesApoio
                .Where(s => s.Status == "Realizada")
                .Select(s => new SessaoApoioDTO
                {
                    Id = s.Id,
                    UsuarioId = s.UsuarioId,
                    DataSessao = s.DataSessao,
                    TipoSessao = s.TipoSessao,
                    TemaSessao = s.TemaSessao,
                    Descricao = s.Descricao,
                    Status = s.Status,
                    Observacoes = s.Observacoes,
                    DuracaoMinutos = s.DuracaoMinutos,
                    Facilitador = s.Facilitador
                })
                .OrderByDescending(s => s.DataSessao)
                .ToListAsync();

            return sessoes;
        }
    }
}
