using Microsoft.EntityFrameworkCore;
using SinaisAPI.Data;
using SinaisAPI.DTOs;
using SinaisAPI.Models;

namespace SinaisAPI.Services
{
    public class AlertaService : IAlertaService
    {
        private readonly SinaisDbContext _context;

        public AlertaService(SinaisDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlertaDTO>> GetAllAlertasAsync()
        {
            var alertas = await _context.Alertas
                .Include(a => a.Usuario)
                .Select(a => new AlertaDTO
                {
                    Id = a.Id,
                    UsuarioId = a.UsuarioId,
                    Titulo = a.Titulo,
                    Mensagem = a.Mensagem,
                    TipoAlerta = a.TipoAlerta,
                    DataCriacao = a.DataCriacao,
                    DataAgendamento = a.DataAgendamento,
                    Enviado = a.Enviado,
                    DataEnvio = a.DataEnvio,
                    Prioridade = a.Prioridade,
                    CanalEnvio = a.CanalEnvio
                })
                .OrderByDescending(a => a.DataCriacao)
                .ToListAsync();

            return alertas;
        }

        public async Task<AlertaDTO?> GetAlertaByIdAsync(int id)
        {
            var alerta = await _context.Alertas
                .Include(a => a.Usuario)
                .Where(a => a.Id == id)
                .Select(a => new AlertaDTO
                {
                    Id = a.Id,
                    UsuarioId = a.UsuarioId,
                    Titulo = a.Titulo,
                    Mensagem = a.Mensagem,
                    TipoAlerta = a.TipoAlerta,
                    DataCriacao = a.DataCriacao,
                    DataAgendamento = a.DataAgendamento,
                    Enviado = a.Enviado,
                    DataEnvio = a.DataEnvio,
                    Prioridade = a.Prioridade,
                    CanalEnvio = a.CanalEnvio
                })
                .FirstOrDefaultAsync();

            return alerta;
        }

        public async Task<IEnumerable<AlertaDTO>> GetAlertasByUsuarioAsync(int usuarioId)
        {
            var alertas = await _context.Alertas
                .Where(a => a.UsuarioId == usuarioId)
                .Select(a => new AlertaDTO
                {
                    Id = a.Id,
                    UsuarioId = a.UsuarioId,
                    Titulo = a.Titulo,
                    Mensagem = a.Mensagem,
                    TipoAlerta = a.TipoAlerta,
                    DataCriacao = a.DataCriacao,
                    DataAgendamento = a.DataAgendamento,
                    Enviado = a.Enviado,
                    DataEnvio = a.DataEnvio,
                    Prioridade = a.Prioridade,
                    CanalEnvio = a.CanalEnvio
                })
                .OrderByDescending(a => a.DataCriacao)
                .ToListAsync();

            return alertas;
        }

        public async Task<AlertaDTO> CreateAlertaAsync(CreateAlertaDTO createAlertaDTO)
        {
            var alerta = new Alerta
            {
                UsuarioId = createAlertaDTO.UsuarioId,
                Titulo = createAlertaDTO.Titulo,
                Mensagem = createAlertaDTO.Mensagem,
                TipoAlerta = createAlertaDTO.TipoAlerta,
                DataAgendamento = createAlertaDTO.DataAgendamento,
                Prioridade = createAlertaDTO.Prioridade,
                CanalEnvio = createAlertaDTO.CanalEnvio,
                DataCriacao = DateTime.Now,
                Enviado = false
            };

            _context.Alertas.Add(alerta);
            await _context.SaveChangesAsync();

            return new AlertaDTO
            {
                Id = alerta.Id,
                UsuarioId = alerta.UsuarioId,
                Titulo = alerta.Titulo,
                Mensagem = alerta.Mensagem,
                TipoAlerta = alerta.TipoAlerta,
                DataCriacao = alerta.DataCriacao,
                DataAgendamento = alerta.DataAgendamento,
                Enviado = alerta.Enviado,
                DataEnvio = alerta.DataEnvio,
                Prioridade = alerta.Prioridade,
                CanalEnvio = alerta.CanalEnvio
            };
        }

        public async Task<AlertaDTO?> UpdateAlertaAsync(int id, UpdateAlertaDTO updateAlertaDTO)
        {
            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta == null) return null;

            if (updateAlertaDTO.Titulo != null)
                alerta.Titulo = updateAlertaDTO.Titulo;
            if (updateAlertaDTO.Mensagem != null)
                alerta.Mensagem = updateAlertaDTO.Mensagem;
            if (updateAlertaDTO.TipoAlerta != null)
                alerta.TipoAlerta = updateAlertaDTO.TipoAlerta;
            if (updateAlertaDTO.DataAgendamento.HasValue)
                alerta.DataAgendamento = updateAlertaDTO.DataAgendamento;
            if (updateAlertaDTO.Prioridade != null)
                alerta.Prioridade = updateAlertaDTO.Prioridade;
            if (updateAlertaDTO.CanalEnvio != null)
                alerta.CanalEnvio = updateAlertaDTO.CanalEnvio;
            if (updateAlertaDTO.Enviado.HasValue)
                alerta.Enviado = updateAlertaDTO.Enviado.Value;

            await _context.SaveChangesAsync();

            return new AlertaDTO
            {
                Id = alerta.Id,
                UsuarioId = alerta.UsuarioId,
                Titulo = alerta.Titulo,
                Mensagem = alerta.Mensagem,
                TipoAlerta = alerta.TipoAlerta,
                DataCriacao = alerta.DataCriacao,
                DataAgendamento = alerta.DataAgendamento,
                Enviado = alerta.Enviado,
                DataEnvio = alerta.DataEnvio,
                Prioridade = alerta.Prioridade,
                CanalEnvio = alerta.CanalEnvio
            };
        }

        public async Task<bool> DeleteAlertaAsync(int id)
        {
            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta == null) return false;

            _context.Alertas.Remove(alerta);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AlertaDTO>> GetAlertasNaoEnviadosAsync()
        {
            var alertas = await _context.Alertas
                .Where(a => !a.Enviado)
                .Select(a => new AlertaDTO
                {
                    Id = a.Id,
                    UsuarioId = a.UsuarioId,
                    Titulo = a.Titulo,
                    Mensagem = a.Mensagem,
                    TipoAlerta = a.TipoAlerta,
                    DataCriacao = a.DataCriacao,
                    DataAgendamento = a.DataAgendamento,
                    Enviado = a.Enviado,
                    DataEnvio = a.DataEnvio,
                    Prioridade = a.Prioridade,
                    CanalEnvio = a.CanalEnvio
                })
                .OrderBy(a => a.DataAgendamento)
                .ToListAsync();

            return alertas;
        }

        public async Task<IEnumerable<AlertaDTO>> GetAlertasByTipoAsync(string tipo)
        {
            var alertas = await _context.Alertas
                .Where(a => a.TipoAlerta == tipo)
                .Select(a => new AlertaDTO
                {
                    Id = a.Id,
                    UsuarioId = a.UsuarioId,
                    Titulo = a.Titulo,
                    Mensagem = a.Mensagem,
                    TipoAlerta = a.TipoAlerta,
                    DataCriacao = a.DataCriacao,
                    DataAgendamento = a.DataAgendamento,
                    Enviado = a.Enviado,
                    DataEnvio = a.DataEnvio,
                    Prioridade = a.Prioridade,
                    CanalEnvio = a.CanalEnvio
                })
                .OrderByDescending(a => a.DataCriacao)
                .ToListAsync();

            return alertas;
        }

        public async Task<IEnumerable<AlertaDTO>> GetAlertasByPrioridadeAsync(string prioridade)
        {
            var alertas = await _context.Alertas
                .Where(a => a.Prioridade == prioridade)
                .Select(a => new AlertaDTO
                {
                    Id = a.Id,
                    UsuarioId = a.UsuarioId,
                    Titulo = a.Titulo,
                    Mensagem = a.Mensagem,
                    TipoAlerta = a.TipoAlerta,
                    DataCriacao = a.DataCriacao,
                    DataAgendamento = a.DataAgendamento,
                    Enviado = a.Enviado,
                    DataEnvio = a.DataEnvio,
                    Prioridade = a.Prioridade,
                    CanalEnvio = a.CanalEnvio
                })
                .OrderByDescending(a => a.DataCriacao)
                .ToListAsync();

            return alertas;
        }

        public async Task<bool> MarcarAlertaComoEnviadoAsync(int id)
        {
            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta == null) return false;

            alerta.Enviado = true;
            alerta.DataEnvio = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
