using Microsoft.AspNetCore.Mvc;
using SinaisAPI.DTOs;
using SinaisAPI.Services;

namespace SinaisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioProgressoController : ControllerBase
    {
        private readonly IRelatorioProgressoService _relatorioService;
        private readonly ILogger<RelatorioProgressoController> _logger;

        public RelatorioProgressoController(IRelatorioProgressoService relatorioService, ILogger<RelatorioProgressoController> logger)
        {
            _relatorioService = relatorioService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os relatórios de progresso
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelatorioProgressoDTO>>> GetRelatorios()
        {
            try
            {
                var relatorios = await _relatorioService.GetAllRelatoriosAsync();
                return Ok(relatorios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar relatórios de progresso");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém um relatório de progresso por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<RelatorioProgressoDTO>> GetRelatorio(int id)
        {
            try
            {
                var relatorio = await _relatorioService.GetRelatorioByIdAsync(id);
                if (relatorio == null)
                    return NotFound($"Relatório com ID {id} não encontrado");

                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar relatório {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém relatórios de progresso por usuário
        /// </summary>
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<RelatorioProgressoDTO>>> GetRelatoriosByUsuario(int usuarioId)
        {
            try
            {
                var relatorios = await _relatorioService.GetRelatoriosByUsuarioAsync(usuarioId);
                return Ok(relatorios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar relatórios do usuário {UsuarioId}", usuarioId);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Cria um novo relatório de progresso
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<RelatorioProgressoDTO>> CreateRelatorio(CreateRelatorioProgressoDTO createRelatorioDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var relatorio = await _relatorioService.CreateRelatorioAsync(createRelatorioDTO);
                return CreatedAtAction(nameof(GetRelatorio), new { id = relatorio.Id }, relatorio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar relatório de progresso");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza um relatório de progresso existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRelatorio(int id, UpdateRelatorioProgressoDTO updateRelatorioDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var relatorio = await _relatorioService.UpdateRelatorioAsync(id, updateRelatorioDTO);
                if (relatorio == null)
                    return NotFound($"Relatório com ID {id} não encontrado");

                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar relatório {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Remove um relatório de progresso
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRelatorio(int id)
        {
            try
            {
                var result = await _relatorioService.DeleteRelatorioAsync(id);
                if (!result)
                    return NotFound($"Relatório com ID {id} não encontrado");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar relatório {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém relatórios por tipo
        /// </summary>
        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult<IEnumerable<RelatorioProgressoDTO>>> GetRelatoriosByTipo(string tipo)
        {
            try
            {
                var relatorios = await _relatorioService.GetRelatoriosByTipoAsync(tipo);
                return Ok(relatorios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar relatórios por tipo {Tipo}", tipo);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém relatórios por período
        /// </summary>
        [HttpGet("periodo")]
        public async Task<ActionResult<IEnumerable<RelatorioProgressoDTO>>> GetRelatoriosByPeriodo(
            [FromQuery] DateTime dataInicio, 
            [FromQuery] DateTime dataFim)
        {
            try
            {
                var relatorios = await _relatorioService.GetRelatoriosByPeriodoAsync(dataInicio, dataFim);
                return Ok(relatorios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar relatórios por período");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém o último relatório de um usuário
        /// </summary>
        [HttpGet("usuario/{usuarioId}/ultimo")]
        public async Task<ActionResult<RelatorioProgressoDTO>> GetUltimoRelatorioUsuario(int usuarioId)
        {
            try
            {
                var relatorio = await _relatorioService.GetUltimoRelatorioUsuarioAsync(usuarioId);
                if (relatorio == null)
                    return NotFound($"Nenhum relatório encontrado para o usuário {usuarioId}");

                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar último relatório do usuário {UsuarioId}", usuarioId);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém relatórios com progresso (pontuação > 0)
        /// </summary>
        [HttpGet("com-progresso")]
        public async Task<ActionResult<IEnumerable<RelatorioProgressoDTO>>> GetRelatoriosComProgresso()
        {
            try
            {
                var relatorios = await _relatorioService.GetRelatoriosComProgressoAsync();
                return Ok(relatorios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar relatórios com progresso");
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
