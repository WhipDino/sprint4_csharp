using Microsoft.AspNetCore.Mvc;
using SinaisAPI.DTOs;
using SinaisAPI.Services;

namespace SinaisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessaoApoioController : ControllerBase
    {
        private readonly ISessaoApoioService _sessaoService;
        private readonly ILogger<SessaoApoioController> _logger;

        public SessaoApoioController(ISessaoApoioService sessaoService, ILogger<SessaoApoioController> logger)
        {
            _sessaoService = sessaoService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todas as sessões de apoio
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessaoApoioDTO>>> GetSessoes()
        {
            try
            {
                var sessoes = await _sessaoService.GetAllSessoesAsync();
                return Ok(sessoes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar sessões de apoio");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém uma sessão de apoio por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<SessaoApoioDTO>> GetSessao(int id)
        {
            try
            {
                var sessao = await _sessaoService.GetSessaoByIdAsync(id);
                if (sessao == null)
                    return NotFound($"Sessão com ID {id} não encontrada");

                return Ok(sessao);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar sessão {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém sessões de apoio por usuário
        /// </summary>
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<SessaoApoioDTO>>> GetSessoesByUsuario(int usuarioId)
        {
            try
            {
                var sessoes = await _sessaoService.GetSessoesByUsuarioAsync(usuarioId);
                return Ok(sessoes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar sessões do usuário {UsuarioId}", usuarioId);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Cria uma nova sessão de apoio
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<SessaoApoioDTO>> CreateSessao(CreateSessaoApoioDTO createSessaoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var sessao = await _sessaoService.CreateSessaoAsync(createSessaoDTO);
                return CreatedAtAction(nameof(GetSessao), new { id = sessao.Id }, sessao);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar sessão de apoio");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza uma sessão de apoio existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSessao(int id, UpdateSessaoApoioDTO updateSessaoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var sessao = await _sessaoService.UpdateSessaoAsync(id, updateSessaoDTO);
                if (sessao == null)
                    return NotFound($"Sessão com ID {id} não encontrada");

                return Ok(sessao);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar sessão {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Remove uma sessão de apoio
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessao(int id)
        {
            try
            {
                var result = await _sessaoService.DeleteSessaoAsync(id);
                if (!result)
                    return NotFound($"Sessão com ID {id} não encontrada");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar sessão {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém sessões por tipo
        /// </summary>
        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult<IEnumerable<SessaoApoioDTO>>> GetSessoesByTipo(string tipo)
        {
            try
            {
                var sessoes = await _sessaoService.GetSessoesByTipoAsync(tipo);
                return Ok(sessoes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar sessões por tipo {Tipo}", tipo);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém sessões por período
        /// </summary>
        [HttpGet("periodo")]
        public async Task<ActionResult<IEnumerable<SessaoApoioDTO>>> GetSessoesByPeriodo(
            [FromQuery] DateTime dataInicio, 
            [FromQuery] DateTime dataFim)
        {
            try
            {
                var sessoes = await _sessaoService.GetSessoesByPeriodoAsync(dataInicio, dataFim);
                return Ok(sessoes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar sessões por período");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém apenas sessões realizadas
        /// </summary>
        [HttpGet("realizadas")]
        public async Task<ActionResult<IEnumerable<SessaoApoioDTO>>> GetSessoesRealizadas()
        {
            try
            {
                var sessoes = await _sessaoService.GetSessoesRealizadasAsync();
                return Ok(sessoes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar sessões realizadas");
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
