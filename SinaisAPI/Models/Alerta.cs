using System.ComponentModel.DataAnnotations;

namespace SinaisAPI.Models
{
    public class Alerta
    {
        [Key]
        public int Id { get; set; }

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
        public string TipoAlerta { get; set; } = string.Empty; // Lembrete, Aviso, Emergencia

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public DateTime? DataAgendamento { get; set; }

        public bool Enviado { get; set; } = false;

        public DateTime? DataEnvio { get; set; }

        [StringLength(50)]
        public string Prioridade { get; set; } = "Media"; // Baixa, Media, Alta

        [StringLength(100)]
        public string? CanalEnvio { get; set; } // Email, SMS, Push

        // Navegação
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
