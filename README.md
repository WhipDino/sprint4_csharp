# Sinais API - Prevenção de Vício em Apostas e Jogos

## 👥 Integrantes do Grupo

- **João Victor** - RM550453
- **Pedro Henrique Farath** - RM98608
- **Lucca Vilaça** - RM551538
- **Luana Cabezaollias** - RM99320
- **Juliana Maita** - RM99224

## 📋 Sobre o Projeto

A **Sinais API** é uma aplicação desenvolvida em ASP.NET Core Web API com Entity Framework, focada na prevenção e tratamento de vício em apostas e jogos. O sistema oferece uma plataforma completa para acompanhamento de usuários, sessões de apoio, alertas personalizados e relatórios de progresso.

## 🎯 Objetivo

Fornecer uma API robusta e escalável para aplicativos de prevenção de vício, oferecendo:
- Gestão completa de usuários
- Agendamento e acompanhamento de sessões de apoio
- Sistema de alertas e notificações
- Relatórios de progresso detalhados
- Recursos de ajuda e suporte
- Integração com APIs externas para apoio adicional

## 🛠️ Tecnologias Utilizadas

- **.NET 8.0** - Framework principal
- **ASP.NET Core Web API** - Arquitetura da API
- **Entity Framework Core** - ORM para acesso a dados
- **SQL Server LocalDB** - Banco de dados
- **Swagger/OpenAPI** - Documentação da API
- **LINQ** - Consultas avançadas no banco de dados
- **HttpClient** - Integração com APIs externas

## 📁 Estrutura do Projeto

```
SinaisAPI/
├── Controllers/           # Controladores da API
│   ├── UsuarioController.cs
│   ├── SessaoApoioController.cs
│   ├── AlertaController.cs
│   ├── RelatorioProgressoController.cs
│   ├── RecursoAjudaController.cs
│   └── ExternalApiController.cs
├── Data/                 # Contexto do Entity Framework
│   └── SinaisDbContext.cs
├── DTOs/                 # Data Transfer Objects
│   ├── UsuarioDTO.cs
│   ├── SessaoApoioDTO.cs
│   ├── AlertaDTO.cs
│   ├── RelatorioProgressoDTO.cs
│   └── RecursoAjudaDTO.cs
├── Models/               # Entidades do domínio
│   ├── Usuario.cs
│   ├── SessaoApoio.cs
│   ├── Alerta.cs
│   ├── RelatorioProgresso.cs
│   └── RecursoAjuda.cs
├── Services/             # Lógica de negócio
│   ├── IUsuarioService.cs
│   ├── UsuarioService.cs
│   ├── ISessaoApoioService.cs
│   ├── SessaoApoioService.cs
│   ├── IAlertaService.cs
│   ├── AlertaService.cs
│   ├── IRelatorioProgressoService.cs
│   ├── RelatorioProgressoService.cs
│   ├── IRecursoAjudaService.cs
│   ├── RecursoAjudaService.cs
│   ├── IExternalApiService.cs
│   └── ExternalApiService.cs
├── Program.cs            # Configuração da aplicação
└── appsettings.json      # Configurações
```

## 🚀 Como Executar

### 📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- **.NET 8.0 SDK** - [Download aqui](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server LocalDB** (incluído no Visual Studio) ou **SQL Server Express**
- **Visual Studio 2022** ou **VS Code** (recomendado)

### 🔧 Instalação Passo a Passo

#### **1. Clone o repositório**
```bash
git clone https://github.com/WhipDino/sprint4_csharp.git
cd sprint4_csharp/SinaisAPI
```

#### **2. Verifique se o .NET está instalado**
```bash
dotnet --version
```
**Deve retornar:** `8.0.x` ou superior

#### **3. Restaure as dependências**
```bash
dotnet restore
```

#### **4. Configure o banco de dados**
A aplicação usa SQL Server LocalDB por padrão. Se você não tiver o LocalDB instalado:

**Opção A: Instalar SQL Server LocalDB**
- Baixe e instale o [SQL Server Express LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)

**Opção B: Usar SQL Server Express**
- Edite o arquivo `appsettings.json` e altere a connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SinaisDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

#### **5. Execute a aplicação**
```bash
dotnet run
```

**Ou com URLs específicas:**
```bash
dotnet run --urls "https://localhost:7000;http://localhost:5000"
```

#### **6. Acesse a documentação**
- **Swagger UI (HTTPS):** https://localhost:7000
- **Swagger UI (HTTP):** http://localhost:5000
- **API Base URL:** https://localhost:7000/api

### 🐛 Solução de Problemas

#### **Erro: "command not found: dotnet"**
```bash
# No macOS/Linux, adicione ao PATH:
echo 'export PATH="$PATH:/Users/[seu-usuario]/.dotnet"' >> ~/.zshrc
source ~/.zshrc

# Ou use o caminho completo:
/Users/[seu-usuario]/.dotnet/dotnet run
```

#### **Erro de conexão com banco de dados**
1. Verifique se o SQL Server LocalDB está rodando
2. Ou altere a connection string no `appsettings.json`
3. Ou instale o SQL Server Express

#### **Erro de porta em uso**
```bash
# Use uma porta diferente:
dotnet run --urls "https://localhost:7001;http://localhost:5001"
```

### 🧪 Testando a API

1. **Acesse o Swagger UI** em https://localhost:7000
2. **Teste o endpoint de saúde:**
   ```bash
   curl https://localhost:7000/api/usuario
   ```
3. **Crie um usuário de teste:**
   ```bash
   curl -X POST "https://localhost:7000/api/usuario" \
     -H "Content-Type: application/json" \
     -d '{
       "nome": "Usuário Teste",
       "email": "teste@email.com",
       "telefone": "11999999999",
       "dataNascimento": "1990-01-01T00:00:00",
       "genero": "Masculino"
     }'
   ```

### 📱 Usando com Postman/Insomnia

1. **Base URL:** `https://localhost:7000/api`
2. **Headers:** `Content-Type: application/json`
3. **Exemplos completos:** Veja o arquivo `EXEMPLOS_API.md`

## 📊 Modelo de Dados

### Entidades Principais

#### Usuario
- Informações pessoais e de contato
- Status ativo/inativo
- Relacionamentos com outras entidades

#### SessaoApoio
- Agendamento de sessões de apoio
- Tipos: Individual, Grupo, Online
- Status: Agendada, Realizada, Cancelada

#### Alerta
- Sistema de notificações
- Tipos: Lembrete, Aviso, Emergência
- Prioridades: Baixa, Média, Alta

#### RelatorioProgresso
- Acompanhamento de progresso
- Métricas de sobriedade
- Análise de evolução

#### RecursoAjuda
- Base de recursos de apoio
- Categorias: Artigo, Vídeo, Contato, Link
- Sistema de priorização

## 🔗 Endpoints da API

### Usuários
- `GET /api/usuario` - Listar todos os usuários
- `GET /api/usuario/{id}` - Obter usuário por ID
- `POST /api/usuario` - Criar novo usuário
- `PUT /api/usuario/{id}` - Atualizar usuário
- `DELETE /api/usuario/{id}` - Remover usuário
- `GET /api/usuario/search?term={term}` - Buscar usuários
- `GET /api/usuario/ativos` - Listar usuários ativos
- `GET /api/usuario/email/{email}` - Buscar por email

### Sessões de Apoio
- `GET /api/sessaoapoio` - Listar todas as sessões
- `GET /api/sessaoapoio/{id}` - Obter sessão por ID
- `POST /api/sessaoapoio` - Criar nova sessão
- `PUT /api/sessaoapoio/{id}` - Atualizar sessão
- `DELETE /api/sessaoapoio/{id}` - Remover sessão
- `GET /api/sessaoapoio/usuario/{usuarioId}` - Sessões por usuário
- `GET /api/sessaoapoio/tipo/{tipo}` - Sessões por tipo
- `GET /api/sessaoapoio/periodo?dataInicio={data}&dataFim={data}` - Sessões por período
- `GET /api/sessaoapoio/realizadas` - Sessões realizadas

### Alertas
- `GET /api/alerta` - Listar todos os alertas
- `GET /api/alerta/{id}` - Obter alerta por ID
- `POST /api/alerta` - Criar novo alerta
- `PUT /api/alerta/{id}` - Atualizar alerta
- `DELETE /api/alerta/{id}` - Remover alerta
- `GET /api/alerta/usuario/{usuarioId}` - Alertas por usuário
- `GET /api/alerta/nao-enviados` - Alertas não enviados
- `GET /api/alerta/tipo/{tipo}` - Alertas por tipo
- `GET /api/alerta/prioridade/{prioridade}` - Alertas por prioridade
- `POST /api/alerta/{id}/marcar-enviado` - Marcar como enviado

### Relatórios de Progresso
- `GET /api/relatorioprogresso` - Listar todos os relatórios
- `GET /api/relatorioprogresso/{id}` - Obter relatório por ID
- `POST /api/relatorioprogresso` - Criar novo relatório
- `PUT /api/relatorioprogresso/{id}` - Atualizar relatório
- `DELETE /api/relatorioprogresso/{id}` - Remover relatório
- `GET /api/relatorioprogresso/usuario/{usuarioId}` - Relatórios por usuário
- `GET /api/relatorioprogresso/tipo/{tipo}` - Relatórios por tipo
- `GET /api/relatorioprogresso/periodo?dataInicio={data}&dataFim={data}` - Relatórios por período
- `GET /api/relatorioprogresso/usuario/{usuarioId}/ultimo` - Último relatório do usuário
- `GET /api/relatorioprogresso/com-progresso` - Relatórios com progresso

### Recursos de Ajuda
- `GET /api/recursoajuda` - Listar todos os recursos
- `GET /api/recursoajuda/{id}` - Obter recurso por ID
- `POST /api/recursoajuda` - Criar novo recurso
- `PUT /api/recursoajuda/{id}` - Atualizar recurso
- `DELETE /api/recursoajuda/{id}` - Remover recurso
- `GET /api/recursoajuda/categoria/{categoria}` - Recursos por categoria
- `GET /api/recursoajuda/ativos` - Recursos ativos
- `GET /api/recursoajuda/search?term={term}` - Buscar recursos
- `GET /api/recursoajuda/prioridade/{prioridade}` - Recursos por prioridade

### APIs Externas
- `POST /api/externalapi/openai` - Consultar OpenAI
- `GET /api/externalapi/cep/{cep}` - Consultar CEP
- `GET /api/externalapi/mensagem-motivacional` - Mensagem motivacional
- `GET /api/externalapi/apoio-crise` - Mensagem de apoio em crise
- `GET /api/externalapi/recursos-saude/{cidade}` - Recursos de saúde por cidade

## 🔍 Consultas LINQ Avançadas

O projeto implementa diversas consultas LINQ complexas:

### Busca e Filtros
- Busca por texto em múltiplos campos
- Filtros por data, status, prioridade
- Ordenação dinâmica
- Paginação (preparado para implementação)

### Agregações
- Contagem de sessões por usuário
- Relatórios de progresso com métricas
- Estatísticas de alertas enviados
- Análise de recursos mais acessados

### Relacionamentos
- Consultas com JOINs otimizados
- Inclusão de entidades relacionadas
- Filtros baseados em relacionamentos

## 🌐 Integração com APIs Externas

### OpenAI
- Geração de mensagens motivacionais
- Suporte em tempo de crise
- Conselhos personalizados

### ViaCEP
- Consulta de endereços por CEP
- Validação de dados de localização

### Recursos de Saúde
- Busca por recursos locais
- Informações de contato de emergência
- Diretório de profissionais especializados

## 📈 Funcionalidades Implementadas

### ✅ CRUD Completo
- Todas as entidades possuem operações Create, Read, Update, Delete
- Validação de dados com Data Annotations
- Tratamento de erros robusto

### ✅ Consultas LINQ
- Busca avançada com múltiplos critérios
- Filtros por período, status, prioridade
- Ordenação e agregações
- Consultas otimizadas com índices

### ✅ Integração Externa
- OpenAI para suporte inteligente
- ViaCEP para dados de localização
- Sistema de mensagens motivacionais
- Recursos de saúde mental

### ✅ Documentação Swagger
- Interface interativa completa
- Documentação de todos os endpoints
- Exemplos de requisições e respostas
- Testes diretos da API

## 🏗️ Arquitetura

### Padrões Utilizados
- **Repository Pattern** (via Services)
- **DTO Pattern** para transferência de dados
- **Dependency Injection** para injeção de dependências
- **Async/Await** para operações assíncronas

### Estrutura em Camadas
1. **Controllers** - Endpoints da API
2. **Services** - Lógica de negócio
3. **Data** - Acesso a dados (Entity Framework)
4. **Models** - Entidades do domínio
5. **DTOs** - Objetos de transferência

## 🔧 Configuração

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SinaisDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  },
  "ExternalApis": {
    "OpenAI": {
      "BaseUrl": "https://api.openai.com/v1",
      "ApiKey": "your-openai-api-key-here"
    },
    "ViaCep": {
      "BaseUrl": "https://viacep.com.br/ws"
    }
  }
}
```

## 📝 Exemplos de Uso

### Criar um Usuário
```http
POST /api/usuario
Content-Type: application/json

{
  "nome": "João Silva",
  "email": "joao@email.com",
  "telefone": "11999999999",
  "dataNascimento": "1990-01-01",
  "genero": "Masculino"
}
```

### Agendar Sessão de Apoio
```http
POST /api/sessaoapoio
Content-Type: application/json

{
  "usuarioId": 1,
  "dataSessao": "2024-01-15T14:00:00",
  "tipoSessao": "Individual",
  "temaSessao": "Controle de impulsos",
  "descricao": "Sessão focada em técnicas de controle de impulsos"
}
```

### Criar Alerta
```http
POST /api/alerta
Content-Type: application/json

{
  "usuarioId": 1,
  "titulo": "Lembrete de Sessão",
  "mensagem": "Você tem uma sessão de apoio agendada para hoje às 14h",
  "tipoAlerta": "Lembrete",
  "prioridade": "Media"
}
```

## 🚀 Próximos Passos

- [ ] Implementar autenticação e autorização
- [ ] Adicionar sistema de notificações push
- [ ] Implementar cache para melhor performance
- [ ] Adicionar testes unitários e de integração
- [ ] Implementar logging estruturado
- [ ] Adicionar métricas e monitoramento

## 📞 Suporte

Para dúvidas ou suporte, entre em contato através do repositório do projeto.

---

**Desenvolvido com ❤️ para ajudar na prevenção de vício em apostas e jogos**
