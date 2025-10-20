using Microsoft.AspNetCore.Mvc;
using SinaisAPI.DTOs;
using SinaisAPI.Services;

namespace SinaisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertaController : ControllerBase
    {
        private readonly IAlertaService _alertaService;
        private readonly ILogger<AlertaController> _logger;

        public AlertaController(IAlertaService alertaService, ILogger<AlertaController> logger)
        {
            _alertaService = alertaService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os alertas
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertaDTO>>> GetAlertas()
        {
            try
            {
                var alertas = await _alertaService.GetAllAlertasAsync();
                return Ok(alertas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar alertas");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém um alerta por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<AlertaDTO>> GetAlerta(int id)
        {
            try
            {
                var alerta = await _alertaService.GetAlertaByIdAsync(id);
                if (alerta == null)
                    return NotFound($"Alerta com ID {id} não encontrado");

                return Ok(alerta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar alerta {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém alertas por usuário
        /// </summary>
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<AlertaDTO>>> GetAlertasByUsuario(int usuarioId)
        {
            try
            {
                var alertas = await _alertaService.GetAlertasByUsuarioAsync(usuarioId);
                return Ok(alertas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar alertas do usuário {UsuarioId}", usuarioId);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Cria um novo alerta
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<AlertaDTO>> CreateAlerta(CreateAlertaDTO createAlertaDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var alerta = await _alertaService.CreateAlertaAsync(createAlertaDTO);
                return CreatedAtAction(nameof(GetAlerta), new { id = alerta.Id }, alerta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar alerta");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza um alerta existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlerta(int id, UpdateAlertaDTO updateAlertaDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var alerta = await _alertaService.UpdateAlertaAsync(id, updateAlertaDTO);
                if (alerta == null)
                    return NotFound($"Alerta com ID {id} não encontrado");

                return Ok(alerta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar alerta {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Remove um alerta
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlerta(int id)
        {
            try
            {
                var result = await _alertaService.DeleteAlertaAsync(id);
                if (!result)
                    return NotFound($"Alerta com ID {id} não encontrado");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar alerta {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém alertas não enviados
        /// </summary>
        [HttpGet("nao-enviados")]
        public async Task<ActionResult<IEnumerable<AlertaDTO>>> GetAlertasNaoEnviados()
        {
            try
            {
                var alertas = await _alertaService.GetAlertasNaoEnviadosAsync();
                return Ok(alertas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar alertas não enviados");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém alertas por tipo
        /// </summary>
        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult<IEnumerable<AlertaDTO>>> GetAlertasByTipo(string tipo)
        {
            try
            {
                var alertas = await _alertaService.GetAlertasByTipoAsync(tipo);
                return Ok(alertas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar alertas por tipo {Tipo}", tipo);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém alertas por prioridade
        /// </summary>
        [HttpGet("prioridade/{prioridade}")]
        public async Task<ActionResult<IEnumerable<AlertaDTO>>> GetAlertasByPrioridade(string prioridade)
        {
            try
            {
                var alertas = await _alertaService.GetAlertasByPrioridadeAsync(prioridade);
                return Ok(alertas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar alertas por prioridade {Prioridade}", prioridade);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Marca um alerta como enviado
        /// </summary>
        [HttpPost("{id}/marcar-enviado")]
        public async Task<IActionResult> MarcarAlertaComoEnviado(int id)
        {
            try
            {
                var result = await _alertaService.MarcarAlertaComoEnviadoAsync(id);
                if (!result)
                    return NotFound($"Alerta com ID {id} não encontrado");

                return Ok(new { message = "Alerta marcado como enviado com sucesso" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao marcar alerta {Id} como enviado", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
