using System.Text;
using System.Text.Json;

namespace SinaisAPI.Services
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ExternalApiService> _logger;

        public ExternalApiService(HttpClient httpClient, IConfiguration configuration, ILogger<ExternalApiService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> GetOpenAIResponseAsync(string prompt)
        {
            try
            {
                var apiKey = _configuration["ExternalApis:OpenAI:ApiKey"];
                if (string.IsNullOrEmpty(apiKey) || apiKey == "your-openai-api-key-here")
                {
                    return "API Key não configurada. Configure a chave da OpenAI no appsettings.json";
                }

                var baseUrl = _configuration["ExternalApis:OpenAI:BaseUrl"];
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "system", content = "Você é um assistente especializado em prevenção de vício em apostas e jogos. Forneça conselhos úteis e motivacionais." },
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 500,
                    temperature = 0.7
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{baseUrl}/chat/completions", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var openAiResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
                    
                    if (openAiResponse.TryGetProperty("choices", out var choices) && 
                        choices.GetArrayLength() > 0)
                    {
                        var message = choices[0].GetProperty("message").GetProperty("content").GetString();
                        return message ?? "Resposta não disponível";
                    }
                }

                return "Erro ao conectar com a API da OpenAI";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao chamar API da OpenAI");
                return "Erro interno ao processar solicitação";
            }
        }

        public async Task<object> GetCepInfoAsync(string cep)
        {
            try
            {
                var baseUrl = _configuration["ExternalApis:ViaCep:BaseUrl"];
                var response = await _httpClient.GetAsync($"{baseUrl}/{cep}/json/");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var cepInfo = JsonSerializer.Deserialize<JsonElement>(content);
                    
                    if (cepInfo.TryGetProperty("erro", out var erro) && erro.GetBoolean())
                    {
                        return new { error = "CEP não encontrado" };
                    }
                    
                    return new
                    {
                        cep = cepInfo.GetProperty("cep").GetString(),
                        logradouro = cepInfo.GetProperty("logradouro").GetString(),
                        complemento = cepInfo.GetProperty("complemento").GetString(),
                        bairro = cepInfo.GetProperty("bairro").GetString(),
                        localidade = cepInfo.GetProperty("localidade").GetString(),
                        uf = cepInfo.GetProperty("uf").GetString(),
                        ibge = cepInfo.GetProperty("ibge").GetString(),
                        gia = cepInfo.GetProperty("gia").GetString(),
                        ddd = cepInfo.GetProperty("ddd").GetString(),
                        siafi = cepInfo.GetProperty("siafi").GetString()
                    };
                }

                return new { error = "Erro ao consultar CEP" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar CEP");
                return new { error = "Erro interno ao consultar CEP" };
            }
        }

        public async Task<string> GetMotivationalMessageAsync()
        {
            return await Task.FromResult(GetRandomMotivationalMessage());
        }

        private string GetRandomMotivationalMessage()
        {
            var motivationalMessages = new[]
            {
                "Você está no caminho certo! Cada dia sem apostas é uma vitória.",
                "A recuperação é uma jornada, não um destino. Continue firme!",
                "Você é mais forte do que imagina. Acredite em si mesmo!",
                "Cada pequeno passo conta. Você está fazendo progresso!",
                "A vida tem muito mais a oferecer além dos jogos. Descubra!",
                "Você não está sozinho nesta jornada. Estamos aqui para ajudar!",
                "A mudança começa com uma decisão. Você já deu o primeiro passo!",
                "Seja gentil consigo mesmo. A recuperação leva tempo.",
                "Celebre suas pequenas vitórias. Elas são importantes!",
                "O futuro que você quer está sendo construído hoje. Continue!"
            };

            var random = new Random();
            return motivationalMessages[random.Next(motivationalMessages.Length)];
        }

        public async Task<string> GetCrisisSupportMessageAsync()
        {
            return await Task.FromResult("Se você está passando por uma crise, lembre-se: você não está sozinho. " +
                   "Ligue para o CVV (188) ou procure ajuda profissional. " +
                   "A recuperação é possível e você merece apoio. " +
                   "Respire fundo e saiba que esta sensação vai passar.");
        }

        public async Task<object> GetHealthResourcesAsync(string city)
        {
            try
            {
                return await Task.FromResult(GetHealthResourcesForCity(city));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar recursos de saúde");
                return new { error = "Erro interno ao buscar recursos" };
            }
        }

        private object GetHealthResourcesForCity(string city)
        {
            // Simulação de busca por recursos de saúde mental na cidade
            var resources = new List<object>
                {
                    new
                    {
                        nome = "Centro de Valorização da Vida (CVV)",
                        telefone = "188",
                        tipo = "Crise",
                        disponibilidade = "24h",
                        descricao = "Atendimento gratuito para prevenção do suicídio"
                    },
                    new
                    {
                        nome = "CAPS - Centro de Atenção Psicossocial",
                        telefone = "Varia por região",
                        tipo = "Atenção Psicossocial",
                        disponibilidade = "Segunda a Sexta, 8h às 17h",
                        descricao = "Atendimento especializado em saúde mental"
                    },
                    new
                    {
                        nome = "Alcoólicos Anônimos",
                        telefone = "Varia por região",
                        tipo = "Grupo de Apoio",
                        disponibilidade = "Vários horários",
                        descricao = "Grupos de apoio para dependência de álcool"
                    },
                    new
                    {
                        nome = "Jogadores Anônimos",
                        telefone = "Varia por região",
                        tipo = "Grupo de Apoio",
                        disponibilidade = "Vários horários",
                        descricao = "Grupos de apoio para dependência de jogos"
                    }
                };

            return new
            {
                cidade = city,
                recursos = resources,
                total = resources.Count,
                ultimaAtualizacao = DateTime.Now
            };
        }
    }
}
