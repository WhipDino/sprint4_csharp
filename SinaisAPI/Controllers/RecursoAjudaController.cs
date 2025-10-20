using Microsoft.AspNetCore.Mvc;
using SinaisAPI.DTOs;
using SinaisAPI.Services;

namespace SinaisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecursoAjudaController : ControllerBase
    {
        private readonly IRecursoAjudaService _recursoService;
        private readonly ILogger<RecursoAjudaController> _logger;

        public RecursoAjudaController(IRecursoAjudaService recursoService, ILogger<RecursoAjudaController> logger)
        {
            _recursoService = recursoService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os recursos de ajuda
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecursoAjudaDTO>>> GetRecursos()
        {
            try
            {
                var recursos = await _recursoService.GetAllRecursosAsync();
                return Ok(recursos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar recursos de ajuda");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém um recurso de ajuda por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<RecursoAjudaDTO>> GetRecurso(int id)
        {
            try
            {
                var recurso = await _recursoService.GetRecursoByIdAsync(id);
                if (recurso == null)
                    return NotFound($"Recurso com ID {id} não encontrado");

                // Incrementa visualização
                await _recursoService.IncrementarVisualizacaoAsync(id);

                return Ok(recurso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar recurso {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Cria um novo recurso de ajuda
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<RecursoAjudaDTO>> CreateRecurso(CreateRecursoAjudaDTO createRecursoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var recurso = await _recursoService.CreateRecursoAsync(createRecursoDTO);
                return CreatedAtAction(nameof(GetRecurso), new { id = recurso.Id }, recurso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar recurso de ajuda");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza um recurso de ajuda existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecurso(int id, UpdateRecursoAjudaDTO updateRecursoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var recurso = await _recursoService.UpdateRecursoAsync(id, updateRecursoDTO);
                if (recurso == null)
                    return NotFound($"Recurso com ID {id} não encontrado");

                return Ok(recurso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar recurso {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Remove um recurso de ajuda
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecurso(int id)
        {
            try
            {
                var result = await _recursoService.DeleteRecursoAsync(id);
                if (!result)
                    return NotFound($"Recurso com ID {id} não encontrado");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar recurso {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém recursos por categoria
        /// </summary>
        [HttpGet("categoria/{categoria}")]
        public async Task<ActionResult<IEnumerable<RecursoAjudaDTO>>> GetRecursosByCategoria(string categoria)
        {
            try
            {
                var recursos = await _recursoService.GetRecursosByCategoriaAsync(categoria);
                return Ok(recursos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar recursos por categoria {Categoria}", categoria);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém apenas recursos ativos
        /// </summary>
        [HttpGet("ativos")]
        public async Task<ActionResult<IEnumerable<RecursoAjudaDTO>>> GetRecursosAtivos()
        {
            try
            {
                var recursos = await _recursoService.GetRecursosAtivosAsync();
                return Ok(recursos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar recursos ativos");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Busca recursos por termo de pesquisa
        /// </summary>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<RecursoAjudaDTO>>> SearchRecursos([FromQuery] string term)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(term))
                    return BadRequest("Termo de pesquisa é obrigatório");

                var recursos = await _recursoService.SearchRecursosAsync(term);
                return Ok(recursos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar recursos com termo {Term}", term);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém recursos por prioridade
        /// </summary>
        [HttpGet("prioridade/{prioridade}")]
        public async Task<ActionResult<IEnumerable<RecursoAjudaDTO>>> GetRecursosByPrioridade(string prioridade)
        {
            try
            {
                var recursos = await _recursoService.GetRecursosByPrioridadeAsync(prioridade);
                return Ok(recursos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar recursos por prioridade {Prioridade}", prioridade);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
