using System.ComponentModel.DataAnnotations;

namespace SinaisAPI.Models
{
    public class SessaoApoio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public DateTime DataSessao { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoSessao { get; set; } = string.Empty; // Individual, Grupo, Online

        [Required]
        [StringLength(100)]
        public string TemaSessao { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Descricao { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Agendada"; // Agendada, Realizada, Cancelada

        [StringLength(500)]
        public string? Observacoes { get; set; }

        public int DuracaoMinutos { get; set; } = 60;

        [StringLength(100)]
        public string? Facilitador { get; set; }

        // Navegação
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
