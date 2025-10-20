# Exemplos de Uso da API - Sinais

## 🚀 Como Testar a API

1. **Execute a aplicação:**
   ```bash
   cd SinaisAPI
   dotnet run
   ```

2. **Acesse o Swagger UI:**
   - Abra o navegador em `https://localhost:7000`
   - Ou `http://localhost:5000` se preferir HTTP

3. **Teste os endpoints usando a interface Swagger**

## 📝 Exemplos de Requisições

### 1. Criar um Usuário

**POST** `/api/usuario`

```json
{
  "nome": "João Silva",
  "email": "joao.silva@email.com",
  "telefone": "11999999999",
  "dataNascimento": "1990-05-15T00:00:00",
  "genero": "Masculino",
  "observacoes": "Usuário em processo de recuperação"
}
```

### 2. Buscar Usuários

**GET** `/api/usuario`

**GET** `/api/usuario/ativos`

**GET** `/api/usuario/search?term=João`

### 3. Agendar Sessão de Apoio

**POST** `/api/sessaoapoio`

```json
{
  "usuarioId": 1,
  "dataSessao": "2024-01-20T14:00:00",
  "tipoSessao": "Individual",
  "temaSessao": "Controle de Impulsos",
  "descricao": "Sessão focada em técnicas de controle de impulsos e prevenção de recaídas",
  "observacoes": "Primeira sessão do usuário",
  "duracaoMinutos": 60,
  "facilitador": "Dr. Maria Santos"
}
```

### 4. Criar Alerta

**POST** `/api/alerta`

```json
{
  "usuarioId": 1,
  "titulo": "Lembrete de Sessão",
  "mensagem": "Você tem uma sessão de apoio agendada para hoje às 14h. Não se esqueça!",
  "tipoAlerta": "Lembrete",
  "dataAgendamento": "2024-01-20T13:30:00",
  "prioridade": "Media",
  "canalEnvio": "Email"
}
```

### 5. Criar Relatório de Progresso

**POST** `/api/relatorioprogresso`

```json
{
  "usuarioId": 1,
  "tipoRelatorio": "Semanal",
  "diasSobriedade": 7,
  "sessoesRealizadas": 2,
  "alertasEnviados": 5,
  "observacoes": "Usuário demonstrou boa evolução esta semana",
  "statusGeral": "Bom",
  "pontuacaoProgresso": 8.5,
  "metasAlcancadas": "Participou de todas as sessões agendadas",
  "desafiosIdentificados": "Ainda sente dificuldade em momentos de estresse"
}
```

### 6. Buscar Recursos de Ajuda

**GET** `/api/recursoajuda`

**GET** `/api/recursoajuda/ativos`

**GET** `/api/recursoajuda/categoria/Contato`

**GET** `/api/recursoajuda/search?term=cvv`

### 7. Testar APIs Externas

**POST** `/api/externalapi/openai`

```json
"Preciso de uma mensagem motivacional para alguém que está tentando parar de apostar"
```

**GET** `/api/externalapi/cep/01310100`

**GET** `/api/externalapi/mensagem-motivacional`

**GET** `/api/externalapi/apoio-crise`

**GET** `/api/externalapi/recursos-saude/São Paulo`

## 🔍 Consultas LINQ Avançadas

### Buscar Sessões por Período

**GET** `/api/sessaoapoio/periodo?dataInicio=2024-01-01&dataFim=2024-01-31`

### Buscar Alertas por Prioridade

**GET** `/api/alerta/prioridade/Alta`

### Buscar Relatórios com Progresso

**GET** `/api/relatorioprogresso/com-progresso`

### Buscar Último Relatório de um Usuário

**GET** `/api/relatorioprogresso/usuario/1/ultimo`

## 📊 Fluxo Completo de Uso

### 1. Cadastrar Usuário
```bash
curl -X POST "https://localhost:7000/api/usuario" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Maria Santos",
    "email": "maria@email.com",
    "telefone": "11988888888",
    "dataNascimento": "1985-03-20T00:00:00",
    "genero": "Feminino"
  }'
```

### 2. Agendar Primeira Sessão
```bash
curl -X POST "https://localhost:7000/api/sessaoapoio" \
  -H "Content-Type: application/json" \
  -d '{
    "usuarioId": 1,
    "dataSessao": "2024-01-22T10:00:00",
    "tipoSessao": "Individual",
    "temaSessao": "Avaliação Inicial",
    "descricao": "Primeira sessão de avaliação e estabelecimento de metas"
  }'
```

### 3. Criar Alerta de Lembrete
```bash
curl -X POST "https://localhost:7000/api/alerta" \
  -H "Content-Type: application/json" \
  -d '{
    "usuarioId": 1,
    "titulo": "Lembrete de Sessão",
    "mensagem": "Sua sessão de apoio está agendada para amanhã às 10h",
    "tipoAlerta": "Lembrete",
    "prioridade": "Media"
  }'
```

### 4. Marcar Sessão como Realizada
```bash
curl -X PUT "https://localhost:7000/api/sessaoapoio/1" \
  -H "Content-Type: application/json" \
  -d '{
    "status": "Realizada",
    "observacoes": "Sessão realizada com sucesso. Usuário demonstrou engajamento."
  }'
```

### 5. Gerar Relatório de Progresso
```bash
curl -X POST "https://localhost:7000/api/relatorioprogresso" \
  -H "Content-Type: application/json" \
  -d '{
    "usuarioId": 1,
    "tipoRelatorio": "Semanal",
    "diasSobriedade": 7,
    "sessoesRealizadas": 1,
    "alertasEnviados": 3,
    "statusGeral": "Bom",
    "pontuacaoProgresso": 7.5
  }'
```

## 🧪 Testes de Validação

### Teste de Validação de Email
```json
{
  "nome": "Teste",
  "email": "email-invalido",
  "telefone": "11999999999",
  "dataNascimento": "1990-01-01"
}
```
**Resultado esperado:** 400 Bad Request

### Teste de Usuário Duplicado
```json
{
  "nome": "João Silva",
  "email": "joao.silva@email.com",
  "telefone": "11999999999",
  "dataNascimento": "1990-01-01"
}
```
**Resultado esperado:** 400 Bad Request (email já existe)

## 📈 Monitoramento e Logs

A API gera logs detalhados para:
- Todas as operações CRUD
- Erros e exceções
- Chamadas para APIs externas
- Consultas ao banco de dados

## 🔧 Configurações Importantes

### String de Conexão
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SinaisDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### CORS
A API está configurada para aceitar requisições de qualquer origem em desenvolvimento.

### Swagger
- URL: `https://localhost:7000` (HTTPS)
- URL: `http://localhost:5000` (HTTP)
- Documentação interativa completa
