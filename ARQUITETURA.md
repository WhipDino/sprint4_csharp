# Diagrama de Arquitetura - Sinais API

## Arquitetura Geral

```mermaid
graph TB
    subgraph "Cliente"
        A[App Mobile/Web]
        B[Swagger UI]
    end
    
    subgraph "API Layer"
        C[Controllers]
        D[DTOs]
    end
    
    subgraph "Business Layer"
        E[Services]
        F[Interfaces]
    end
    
    subgraph "Data Layer"
        G[Entity Framework]
        H[SinaisDbContext]
    end
    
    subgraph "Database"
        I[(SQL Server LocalDB)]
    end
    
    subgraph "External APIs"
        J[OpenAI API]
        K[ViaCEP API]
    end
    
    A --> C
    B --> C
    C --> D
    C --> E
    E --> F
    E --> G
    G --> H
    H --> I
    E --> J
    E --> K
```

## Fluxo de Dados

```mermaid
sequenceDiagram
    participant C as Cliente
    participant API as Controller
    participant S as Service
    participant EF as Entity Framework
    participant DB as Database
    participant EXT as External API
    
    C->>API: HTTP Request
    API->>S: Service Call
    S->>EF: Database Query
    EF->>DB: SQL Query
    DB-->>EF: Result Set
    EF-->>S: Entity Objects
    S->>EXT: External API Call
    EXT-->>S: External Response
    S-->>API: DTO Response
    API-->>C: JSON Response
```

## Estrutura de Entidades

```mermaid
erDiagram
    USUARIO ||--o{ SESSAO_APOIO : tem
    USUARIO ||--o{ ALERTA : recebe
    USUARIO ||--o{ RELATORIO_PROGRESSO : possui
    
    USUARIO {
        int Id PK
        string Nome
        string Email UK
        string Telefone UK
        datetime DataNascimento
        string Genero
        datetime DataCadastro
        boolean Ativo
        string Observacoes
    }
    
    SESSAO_APOIO {
        int Id PK
        int UsuarioId FK
        datetime DataSessao
        string TipoSessao
        string TemaSessao
        string Descricao
        string Status
        string Observacoes
        int DuracaoMinutos
        string Facilitador
    }
    
    ALERTA {
        int Id PK
        int UsuarioId FK
        string Titulo
        string Mensagem
        string TipoAlerta
        datetime DataCriacao
        datetime DataAgendamento
        boolean Enviado
        datetime DataEnvio
        string Prioridade
        string CanalEnvio
    }
    
    RELATORIO_PROGRESSO {
        int Id PK
        int UsuarioId FK
        datetime DataRelatorio
        string TipoRelatorio
        int DiasSobriedade
        int SessoesRealizadas
        int AlertasEnviados
        string Observacoes
        string StatusGeral
        decimal PontuacaoProgresso
        string MetasAlcancadas
        string DesafiosIdentificados
    }
    
    RECURSO_AJUDA {
        int Id PK
        string Titulo
        string Categoria
        string Descricao
        string Url
        string Telefone
        string Email
        boolean Ativo
        datetime DataCriacao
        string Prioridade
        string Tags
        int Visualizacoes
    }
```

## Padrões de Design Utilizados

### 1. Repository Pattern (via Services)
```mermaid
classDiagram
    class IUsuarioService {
        <<interface>>
        +GetAllUsuariosAsync()
        +GetUsuarioByIdAsync(id)
        +CreateUsuarioAsync(dto)
        +UpdateUsuarioAsync(id, dto)
        +DeleteUsuarioAsync(id)
    }
    
    class UsuarioService {
        -_context: SinaisDbContext
        +GetAllUsuariosAsync()
        +GetUsuarioByIdAsync(id)
        +CreateUsuarioAsync(dto)
        +UpdateUsuarioAsync(id, dto)
        +DeleteUsuarioAsync(id)
    }
    
    IUsuarioService <|.. UsuarioService
```

### 2. DTO Pattern
```mermaid
classDiagram
    class Usuario {
        +Id: int
        +Nome: string
        +Email: string
        +Telefone: string
        +DataNascimento: DateTime
        +Genero: string
        +DataCadastro: DateTime
        +Ativo: bool
        +Observacoes: string
    }
    
    class UsuarioDTO {
        +Id: int
        +Nome: string
        +Email: string
        +Telefone: string
        +DataNascimento: DateTime
        +Genero: string
        +DataCadastro: DateTime
        +Ativo: bool
        +Observacoes: string
    }
    
    class CreateUsuarioDTO {
        +Nome: string
        +Email: string
        +Telefone: string
        +DataNascimento: DateTime
        +Genero: string
        +Observacoes: string
    }
    
    Usuario --> UsuarioDTO : maps to
    CreateUsuarioDTO --> Usuario : creates
```

### 3. Dependency Injection
```mermaid
graph LR
    A[Program.cs] --> B[Service Registration]
    B --> C[Controller Constructor]
    C --> D[Service Implementation]
    D --> E[Database Context]
```

## Fluxo de Requisição HTTP

```mermaid
flowchart TD
    A[HTTP Request] --> B{Controller}
    B --> C[Validation]
    C --> D{Valid?}
    D -->|No| E[Return BadRequest]
    D -->|Yes| F[Service Layer]
    F --> G[Business Logic]
    G --> H[Entity Framework]
    H --> I[Database Query]
    I --> J[Database Response]
    J --> K[Entity Mapping]
    K --> L[DTO Mapping]
    L --> M[HTTP Response]
    M --> N[Client]
    
    F --> O[External API Call]
    O --> P[External Response]
    P --> L
```

## Camadas da Aplicação

```mermaid
graph TB
    subgraph "Presentation Layer"
        A[Controllers]
        B[DTOs]
        C[Swagger UI]
    end
    
    subgraph "Business Layer"
        D[Services]
        E[Interfaces]
        F[Business Logic]
    end
    
    subgraph "Data Access Layer"
        G[Entity Framework]
        H[DbContext]
        I[Models]
    end
    
    subgraph "Database Layer"
        J[SQL Server LocalDB]
    end
    
    subgraph "External Services"
        K[OpenAI API]
        L[ViaCEP API]
    end
    
    A --> D
    B --> A
    C --> A
    D --> E
    D --> F
    D --> G
    G --> H
    H --> I
    I --> J
    D --> K
    D --> L
```

## Tecnologias e Ferramentas

```mermaid
mindmap
  root((Sinais API))
    Backend
      .NET 8.0
      ASP.NET Core Web API
      Entity Framework Core
      SQL Server LocalDB
    Frontend
      Swagger UI
      JSON API
    External
      OpenAI API
      ViaCEP API
    Development
      Visual Studio 2022
      Git
      NuGet Packages
    Patterns
      Repository Pattern
      DTO Pattern
      Dependency Injection
      Async/Await
```

## Métricas de Qualidade

- **Cobertura de Código**: 100% dos endpoints implementados
- **Documentação**: Swagger UI completo
- **Validação**: Data Annotations em todos os DTOs
- **Tratamento de Erros**: Try-catch em todos os controllers
- **Logging**: ILogger implementado
- **Performance**: Consultas LINQ otimizadas
- **Segurança**: CORS configurado
- **Escalabilidade**: Arquitetura em camadas
