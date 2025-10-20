using System.ComponentModel.DataAnnotations;

namespace SinaisAPI.DTOs
{
    public class AlertaDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
        public string TipoAlerta { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAgendamento { get; set; }
        public bool Enviado { get; set; }
        public DateTime? DataEnvio { get; set; }
        public string Prioridade { get; set; } = string.Empty;
        public string? CanalEnvio { get; set; }
    }

    public class CreateAlertaDTO
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Mensagem { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string TipoAlerta { get; set; } = string.Empty;

        public DateTime? DataAgendamento { get; set; }

        [StringLength(50)]
        public string Prioridade { get; set; } = "Media";

        [StringLength(100)]
        public string? CanalEnvio { get; set; }
    }

    public class UpdateAlertaDTO
    {
        [StringLength(100)]
        public string? Titulo { get; set; }

        [StringLength(500)]
        public string? Mensagem { get; set; }

        [StringLength(50)]
        public string? TipoAlerta { get; set; }

        public DateTime? DataAgendamento { get; set; }

        [StringLength(50)]
        public string? Prioridade { get; set; }

        [StringLength(100)]
        public string? CanalEnvio { get; set; }

        public bool? Enviado { get; set; }
    }
}
