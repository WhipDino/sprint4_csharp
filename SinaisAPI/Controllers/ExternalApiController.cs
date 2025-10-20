using Microsoft.AspNetCore.Mvc;
using SinaisAPI.Services;

namespace SinaisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExternalApiController : ControllerBase
    {
        private readonly IExternalApiService _externalApiService;
        private readonly ILogger<ExternalApiController> _logger;

        public ExternalApiController(IExternalApiService externalApiService, ILogger<ExternalApiController> logger)
        {
            _externalApiService = externalApiService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém resposta da OpenAI para um prompt
        /// </summary>
        [HttpPost("openai")]
        public async Task<ActionResult<string>> GetOpenAIResponse([FromBody] string prompt)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(prompt))
                    return BadRequest("Prompt é obrigatório");

                var response = await _externalApiService.GetOpenAIResponseAsync(prompt);
                return Ok(new { response });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao chamar API da OpenAI");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Consulta informações de CEP
        /// </summary>
        [HttpGet("cep/{cep}")]
        public async Task<ActionResult<object>> GetCepInfo(string cep)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cep) || cep.Length != 8)
                    return BadRequest("CEP deve ter 8 dígitos");

                var cepInfo = await _externalApiService.GetCepInfoAsync(cep);
                return Ok(cepInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar CEP {Cep}", cep);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém mensagem motivacional
        /// </summary>
        [HttpGet("mensagem-motivacional")]
        public async Task<ActionResult<string>> GetMotivationalMessage()
        {
            try
            {
                var message = await _externalApiService.GetMotivationalMessageAsync();
                return Ok(new { message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter mensagem motivacional");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém mensagem de apoio em crise
        /// </summary>
        [HttpGet("apoio-crise")]
        public async Task<ActionResult<string>> GetCrisisSupportMessage()
        {
            try
            {
                var message = await _externalApiService.GetCrisisSupportMessageAsync();
                return Ok(new { message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter mensagem de apoio em crise");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Busca recursos de saúde mental por cidade
        /// </summary>
        [HttpGet("recursos-saude/{cidade}")]
        public async Task<ActionResult<object>> GetHealthResources(string cidade)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cidade))
                    return BadRequest("Cidade é obrigatória");

                var resources = await _externalApiService.GetHealthResourcesAsync(cidade);
                return Ok(resources);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar recursos de saúde para {Cidade}", cidade);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
