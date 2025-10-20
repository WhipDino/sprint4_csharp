using Microsoft.EntityFrameworkCore;
using SinaisAPI.Data;
using SinaisAPI.DTOs;
using SinaisAPI.Models;

namespace SinaisAPI.Services
{
    public class RecursoAjudaService : IRecursoAjudaService
    {
        private readonly SinaisDbContext _context;

        public RecursoAjudaService(SinaisDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RecursoAjudaDTO>> GetAllRecursosAsync()
        {
            var recursos = await _context.RecursosAjuda
                .Select(r => new RecursoAjudaDTO
                {
                    Id = r.Id,
                    Titulo = r.Titulo,
                    Categoria = r.Categoria,
                    Descricao = r.Descricao,
                    Url = r.Url,
                    Telefone = r.Telefone,
                    Email = r.Email,
                    Ativo = r.Ativo,
                    DataCriacao = r.DataCriacao,
                    Prioridade = r.Prioridade,
                    Tags = r.Tags,
                    Visualizacoes = r.Visualizacoes
                })
                .OrderBy(r => r.Prioridade)
                .ThenBy(r => r.Titulo)
                .ToListAsync();

            return recursos;
        }

        public async Task<RecursoAjudaDTO?> GetRecursoByIdAsync(int id)
        {
            var recurso = await _context.RecursosAjuda
                .Where(r => r.Id == id)
                .Select(r => new RecursoAjudaDTO
                {
                    Id = r.Id,
                    Titulo = r.Titulo,
                    Categoria = r.Categoria,
                    Descricao = r.Descricao,
                    Url = r.Url,
                    Telefone = r.Telefone,
                    Email = r.Email,
                    Ativo = r.Ativo,
                    DataCriacao = r.DataCriacao,
                    Prioridade = r.Prioridade,
                    Tags = r.Tags,
                    Visualizacoes = r.Visualizacoes
                })
                .FirstOrDefaultAsync();

            return recurso;
        }

        public async Task<RecursoAjudaDTO> CreateRecursoAsync(CreateRecursoAjudaDTO createRecursoDTO)
        {
            var recurso = new RecursoAjuda
            {
                Titulo = createRecursoDTO.Titulo,
                Categoria = createRecursoDTO.Categoria,
                Descricao = createRecursoDTO.Descricao,
                Url = createRecursoDTO.Url,
                Telefone = createRecursoDTO.Telefone,
                Email = createRecursoDTO.Email,
                Prioridade = createRecursoDTO.Prioridade,
                Tags = createRecursoDTO.Tags,
                Ativo = true,
                DataCriacao = DateTime.Now,
                Visualizacoes = 0
            };

            _context.RecursosAjuda.Add(recurso);
            await _context.SaveChangesAsync();

            return new RecursoAjudaDTO
            {
                Id = recurso.Id,
                Titulo = recurso.Titulo,
                Categoria = recurso.Categoria,
                Descricao = recurso.Descricao,
                Url = recurso.Url,
                Telefone = recurso.Telefone,
                Email = recurso.Email,
                Ativo = recurso.Ativo,
                DataCriacao = recurso.DataCriacao,
                Prioridade = recurso.Prioridade,
                Tags = recurso.Tags,
                Visualizacoes = recurso.Visualizacoes
            };
        }

        public async Task<RecursoAjudaDTO?> UpdateRecursoAsync(int id, UpdateRecursoAjudaDTO updateRecursoDTO)
        {
            var recurso = await _context.RecursosAjuda.FindAsync(id);
            if (recurso == null) return null;

            if (updateRecursoDTO.Titulo != null)
                recurso.Titulo = updateRecursoDTO.Titulo;
            if (updateRecursoDTO.Categoria != null)
                recurso.Categoria = updateRecursoDTO.Categoria;
            if (updateRecursoDTO.Descricao != null)
                recurso.Descricao = updateRecursoDTO.Descricao;
            if (updateRecursoDTO.Url != null)
                recurso.Url = updateRecursoDTO.Url;
            if (updateRecursoDTO.Telefone != null)
                recurso.Telefone = updateRecursoDTO.Telefone;
            if (updateRecursoDTO.Email != null)
                recurso.Email = updateRecursoDTO.Email;
            if (updateRecursoDTO.Ativo.HasValue)
                recurso.Ativo = updateRecursoDTO.Ativo.Value;
            if (updateRecursoDTO.Prioridade != null)
                recurso.Prioridade = updateRecursoDTO.Prioridade;
            if (updateRecursoDTO.Tags != null)
                recurso.Tags = updateRecursoDTO.Tags;

            await _context.SaveChangesAsync();

            return new RecursoAjudaDTO
            {
                Id = recurso.Id,
                Titulo = recurso.Titulo,
                Categoria = recurso.Categoria,
                Descricao = recurso.Descricao,
                Url = recurso.Url,
                Telefone = recurso.Telefone,
                Email = recurso.Email,
                Ativo = recurso.Ativo,
                DataCriacao = recurso.DataCriacao,
                Prioridade = recurso.Prioridade,
                Tags = recurso.Tags,
                Visualizacoes = recurso.Visualizacoes
            };
        }

        public async Task<bool> DeleteRecursoAsync(int id)
        {
            var recurso = await _context.RecursosAjuda.FindAsync(id);
            if (recurso == null) return false;

            _context.RecursosAjuda.Remove(recurso);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<RecursoAjudaDTO>> GetRecursosByCategoriaAsync(string categoria)
        {
            var recursos = await _context.RecursosAjuda
                .Where(r => r.Categoria == categoria)
                .Select(r => new RecursoAjudaDTO
                {
                    Id = r.Id,
                    Titulo = r.Titulo,
                    Categoria = r.Categoria,
                    Descricao = r.Descricao,
                    Url = r.Url,
                    Telefone = r.Telefone,
                    Email = r.Email,
                    Ativo = r.Ativo,
                    DataCriacao = r.DataCriacao,
                    Prioridade = r.Prioridade,
                    Tags = r.Tags,
                    Visualizacoes = r.Visualizacoes
                })
                .OrderBy(r => r.Prioridade)
                .ThenBy(r => r.Titulo)
                .ToListAsync();

            return recursos;
        }

        public async Task<IEnumerable<RecursoAjudaDTO>> GetRecursosAtivosAsync()
        {
            var recursos = await _context.RecursosAjuda
                .Where(r => r.Ativo)
                .Select(r => new RecursoAjudaDTO
                {
                    Id = r.Id,
                    Titulo = r.Titulo,
                    Categoria = r.Categoria,
                    Descricao = r.Descricao,
                    Url = r.Url,
                    Telefone = r.Telefone,
                    Email = r.Email,
                    Ativo = r.Ativo,
                    DataCriacao = r.DataCriacao,
                    Prioridade = r.Prioridade,
                    Tags = r.Tags,
                    Visualizacoes = r.Visualizacoes
                })
                .OrderBy(r => r.Prioridade)
                .ThenBy(r => r.Titulo)
                .ToListAsync();

            return recursos;
        }

        public async Task<IEnumerable<RecursoAjudaDTO>> SearchRecursosAsync(string searchTerm)
        {
            var recursos = await _context.RecursosAjuda
                .Where(r => r.Titulo.Contains(searchTerm) || 
                           r.Descricao.Contains(searchTerm) || 
                           (r.Tags != null && r.Tags.Contains(searchTerm)))
                .Select(r => new RecursoAjudaDTO
                {
                    Id = r.Id,
                    Titulo = r.Titulo,
                    Categoria = r.Categoria,
                    Descricao = r.Descricao,
                    Url = r.Url,
                    Telefone = r.Telefone,
                    Email = r.Email,
                    Ativo = r.Ativo,
                    DataCriacao = r.DataCriacao,
                    Prioridade = r.Prioridade,
                    Tags = r.Tags,
                    Visualizacoes = r.Visualizacoes
                })
                .OrderBy(r => r.Prioridade)
                .ThenBy(r => r.Titulo)
                .ToListAsync();

            return recursos;
        }

        public async Task<IEnumerable<RecursoAjudaDTO>> GetRecursosByPrioridadeAsync(string prioridade)
        {
            var recursos = await _context.RecursosAjuda
                .Where(r => r.Prioridade == prioridade)
                .Select(r => new RecursoAjudaDTO
                {
                    Id = r.Id,
                    Titulo = r.Titulo,
                    Categoria = r.Categoria,
                    Descricao = r.Descricao,
                    Url = r.Url,
                    Telefone = r.Telefone,
                    Email = r.Email,
                    Ativo = r.Ativo,
                    DataCriacao = r.DataCriacao,
                    Prioridade = r.Prioridade,
                    Tags = r.Tags,
                    Visualizacoes = r.Visualizacoes
                })
                .OrderBy(r => r.Titulo)
                .ToListAsync();

            return recursos;
        }

        public async Task<bool> IncrementarVisualizacaoAsync(int id)
        {
            var recurso = await _context.RecursosAjuda.FindAsync(id);
            if (recurso == null) return false;

            recurso.Visualizacoes++;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
