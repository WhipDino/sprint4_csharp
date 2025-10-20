using Microsoft.EntityFrameworkCore;
using SinaisAPI.Data;
using SinaisAPI.DTOs;
using SinaisAPI.Models;

namespace SinaisAPI.Services
{
    public class RelatorioProgressoService : IRelatorioProgressoService
    {
        private readonly SinaisDbContext _context;

        public RelatorioProgressoService(SinaisDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RelatorioProgressoDTO>> GetAllRelatoriosAsync()
        {
            var relatorios = await _context.RelatoriosProgresso
                .Include(r => r.Usuario)
                .Select(r => new RelatorioProgressoDTO
                {
                    Id = r.Id,
                    UsuarioId = r.UsuarioId,
                    DataRelatorio = r.DataRelatorio,
                    TipoRelatorio = r.TipoRelatorio,
                    DiasSobriedade = r.DiasSobriedade,
                    SessoesRealizadas = r.SessoesRealizadas,
                    AlertasEnviados = r.AlertasEnviados,
                    Observacoes = r.Observacoes,
                    StatusGeral = r.StatusGeral,
                    PontuacaoProgresso = r.PontuacaoProgresso,
                    MetasAlcancadas = r.MetasAlcancadas,
                    DesafiosIdentificados = r.DesafiosIdentificados
                })
                .OrderByDescending(r => r.DataRelatorio)
                .ToListAsync();

            return relatorios;
        }

        public async Task<RelatorioProgressoDTO?> GetRelatorioByIdAsync(int id)
        {
            var relatorio = await _context.RelatoriosProgresso
                .Include(r => r.Usuario)
                .Where(r => r.Id == id)
                .Select(r => new RelatorioProgressoDTO
                {
                    Id = r.Id,
                    UsuarioId = r.UsuarioId,
                    DataRelatorio = r.DataRelatorio,
                    TipoRelatorio = r.TipoRelatorio,
                    DiasSobriedade = r.DiasSobriedade,
                    SessoesRealizadas = r.SessoesRealizadas,
                    AlertasEnviados = r.AlertasEnviados,
                    Observacoes = r.Observacoes,
                    StatusGeral = r.StatusGeral,
                    PontuacaoProgresso = r.PontuacaoProgresso,
                    MetasAlcancadas = r.MetasAlcancadas,
                    DesafiosIdentificados = r.DesafiosIdentificados
                })
                .FirstOrDefaultAsync();

            return relatorio;
        }

        public async Task<IEnumerable<RelatorioProgressoDTO>> GetRelatoriosByUsuarioAsync(int usuarioId)
        {
            var relatorios = await _context.RelatoriosProgresso
                .Where(r => r.UsuarioId == usuarioId)
                .Select(r => new RelatorioProgressoDTO
                {
                    Id = r.Id,
                    UsuarioId = r.UsuarioId,
                    DataRelatorio = r.DataRelatorio,
                    TipoRelatorio = r.TipoRelatorio,
                    DiasSobriedade = r.DiasSobriedade,
                    SessoesRealizadas = r.SessoesRealizadas,
                    AlertasEnviados = r.AlertasEnviados,
                    Observacoes = r.Observacoes,
                    StatusGeral = r.StatusGeral,
                    PontuacaoProgresso = r.PontuacaoProgresso,
                    MetasAlcancadas = r.MetasAlcancadas,
                    DesafiosIdentificados = r.DesafiosIdentificados
                })
                .OrderByDescending(r => r.DataRelatorio)
                .ToListAsync();

            return relatorios;
        }

        public async Task<RelatorioProgressoDTO> CreateRelatorioAsync(CreateRelatorioProgressoDTO createRelatorioDTO)
        {
            var relatorio = new RelatorioProgresso
            {
                UsuarioId = createRelatorioDTO.UsuarioId,
                TipoRelatorio = createRelatorioDTO.TipoRelatorio,
                DiasSobriedade = createRelatorioDTO.DiasSobriedade,
                SessoesRealizadas = createRelatorioDTO.SessoesRealizadas,
                AlertasEnviados = createRelatorioDTO.AlertasEnviados,
                Observacoes = createRelatorioDTO.Observacoes,
                StatusGeral = createRelatorioDTO.StatusGeral,
                PontuacaoProgresso = createRelatorioDTO.PontuacaoProgresso,
                MetasAlcancadas = createRelatorioDTO.MetasAlcancadas,
                DesafiosIdentificados = createRelatorioDTO.DesafiosIdentificados,
                DataRelatorio = DateTime.Now
            };

            _context.RelatoriosProgresso.Add(relatorio);
            await _context.SaveChangesAsync();

            return new RelatorioProgressoDTO
            {
                Id = relatorio.Id,
                UsuarioId = relatorio.UsuarioId,
                DataRelatorio = relatorio.DataRelatorio,
                TipoRelatorio = relatorio.TipoRelatorio,
                DiasSobriedade = relatorio.DiasSobriedade,
                SessoesRealizadas = relatorio.SessoesRealizadas,
                AlertasEnviados = relatorio.AlertasEnviados,
                Observacoes = relatorio.Observacoes,
                StatusGeral = relatorio.StatusGeral,
                PontuacaoProgresso = relatorio.PontuacaoProgresso,
                MetasAlcancadas = relatorio.MetasAlcancadas,
                DesafiosIdentificados = relatorio.DesafiosIdentificados
            };
        }

        public async Task<RelatorioProgressoDTO?> UpdateRelatorioAsync(int id, UpdateRelatorioProgressoDTO updateRelatorioDTO)
        {
            var relatorio = await _context.RelatoriosProgresso.FindAsync(id);
            if (relatorio == null) return null;

            if (updateRelatorioDTO.TipoRelatorio != null)
                relatorio.TipoRelatorio = updateRelatorioDTO.TipoRelatorio;
            if (updateRelatorioDTO.DiasSobriedade.HasValue)
                relatorio.DiasSobriedade = updateRelatorioDTO.DiasSobriedade.Value;
            if (updateRelatorioDTO.SessoesRealizadas.HasValue)
                relatorio.SessoesRealizadas = updateRelatorioDTO.SessoesRealizadas.Value;
            if (updateRelatorioDTO.AlertasEnviados.HasValue)
                relatorio.AlertasEnviados = updateRelatorioDTO.AlertasEnviados.Value;
            if (updateRelatorioDTO.Observacoes != null)
                relatorio.Observacoes = updateRelatorioDTO.Observacoes;
            if (updateRelatorioDTO.StatusGeral != null)
                relatorio.StatusGeral = updateRelatorioDTO.StatusGeral;
            if (updateRelatorioDTO.PontuacaoProgresso.HasValue)
                relatorio.PontuacaoProgresso = updateRelatorioDTO.PontuacaoProgresso.Value;
            if (updateRelatorioDTO.MetasAlcancadas != null)
                relatorio.MetasAlcancadas = updateRelatorioDTO.MetasAlcancadas;
            if (updateRelatorioDTO.DesafiosIdentificados != null)
                relatorio.DesafiosIdentificados = updateRelatorioDTO.DesafiosIdentificados;

            await _context.SaveChangesAsync();

            return new RelatorioProgressoDTO
            {
                Id = relatorio.Id,
                UsuarioId = relatorio.UsuarioId,
                DataRelatorio = relatorio.DataRelatorio,
                TipoRelatorio = relatorio.TipoRelatorio,
                DiasSobriedade = relatorio.DiasSobriedade,
                SessoesRealizadas = relatorio.SessoesRealizadas,
                AlertasEnviados = relatorio.AlertasEnviados,
                Observacoes = relatorio.Observacoes,
                StatusGeral = relatorio.StatusGeral,
                PontuacaoProgresso = relatorio.PontuacaoProgresso,
                MetasAlcancadas = relatorio.MetasAlcancadas,
                DesafiosIdentificados = relatorio.DesafiosIdentificados
            };
        }

        public async Task<bool> DeleteRelatorioAsync(int id)
        {
            var relatorio = await _context.RelatoriosProgresso.FindAsync(id);
            if (relatorio == null) return false;

            _context.RelatoriosProgresso.Remove(relatorio);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<RelatorioProgressoDTO>> GetRelatoriosByTipoAsync(string tipo)
        {
            var relatorios = await _context.RelatoriosProgresso
                .Where(r => r.TipoRelatorio == tipo)
                .Select(r => new RelatorioProgressoDTO
                {
                    Id = r.Id,
                    UsuarioId = r.UsuarioId,
                    DataRelatorio = r.DataRelatorio,
                    TipoRelatorio = r.TipoRelatorio,
                    DiasSobriedade = r.DiasSobriedade,
                    SessoesRealizadas = r.SessoesRealizadas,
                    AlertasEnviados = r.AlertasEnviados,
                    Observacoes = r.Observacoes,
                    StatusGeral = r.StatusGeral,
                    PontuacaoProgresso = r.PontuacaoProgresso,
                    MetasAlcancadas = r.MetasAlcancadas,
                    DesafiosIdentificados = r.DesafiosIdentificados
                })
                .OrderByDescending(r => r.DataRelatorio)
                .ToListAsync();

            return relatorios;
        }

        public async Task<IEnumerable<RelatorioProgressoDTO>> GetRelatoriosByPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            var relatorios = await _context.RelatoriosProgresso
                .Where(r => r.DataRelatorio >= dataInicio && r.DataRelatorio <= dataFim)
                .Select(r => new RelatorioProgressoDTO
                {
                    Id = r.Id,
                    UsuarioId = r.UsuarioId,
                    DataRelatorio = r.DataRelatorio,
                    TipoRelatorio = r.TipoRelatorio,
                    DiasSobriedade = r.DiasSobriedade,
                    SessoesRealizadas = r.SessoesRealizadas,
                    AlertasEnviados = r.AlertasEnviados,
                    Observacoes = r.Observacoes,
                    StatusGeral = r.StatusGeral,
                    PontuacaoProgresso = r.PontuacaoProgresso,
                    MetasAlcancadas = r.MetasAlcancadas,
                    DesafiosIdentificados = r.DesafiosIdentificados
                })
                .OrderBy(r => r.DataRelatorio)
                .ToListAsync();

            return relatorios;
        }

        public async Task<RelatorioProgressoDTO?> GetUltimoRelatorioUsuarioAsync(int usuarioId)
        {
            var relatorio = await _context.RelatoriosProgresso
                .Where(r => r.UsuarioId == usuarioId)
                .OrderByDescending(r => r.DataRelatorio)
                .Select(r => new RelatorioProgressoDTO
                {
                    Id = r.Id,
                    UsuarioId = r.UsuarioId,
                    DataRelatorio = r.DataRelatorio,
                    TipoRelatorio = r.TipoRelatorio,
                    DiasSobriedade = r.DiasSobriedade,
                    SessoesRealizadas = r.SessoesRealizadas,
                    AlertasEnviados = r.AlertasEnviados,
                    Observacoes = r.Observacoes,
                    StatusGeral = r.StatusGeral,
                    PontuacaoProgresso = r.PontuacaoProgresso,
                    MetasAlcancadas = r.MetasAlcancadas,
                    DesafiosIdentificados = r.DesafiosIdentificados
                })
                .FirstOrDefaultAsync();

            return relatorio;
        }

        public async Task<IEnumerable<RelatorioProgressoDTO>> GetRelatoriosComProgressoAsync()
        {
            var relatorios = await _context.RelatoriosProgresso
                .Where(r => r.PontuacaoProgresso > 0)
                .Select(r => new RelatorioProgressoDTO
                {
                    Id = r.Id,
                    UsuarioId = r.UsuarioId,
                    DataRelatorio = r.DataRelatorio,
                    TipoRelatorio = r.TipoRelatorio,
                    DiasSobriedade = r.DiasSobriedade,
                    SessoesRealizadas = r.SessoesRealizadas,
                    AlertasEnviados = r.AlertasEnviados,
                    Observacoes = r.Observacoes,
                    StatusGeral = r.StatusGeral,
                    PontuacaoProgresso = r.PontuacaoProgresso,
                    MetasAlcancadas = r.MetasAlcancadas,
                    DesafiosIdentificados = r.DesafiosIdentificados
                })
                .OrderByDescending(r => r.PontuacaoProgresso)
                .ToListAsync();

            return relatorios;
        }
    }
}
