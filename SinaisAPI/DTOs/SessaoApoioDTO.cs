using System.ComponentModel.DataAnnotations;

namespace SinaisAPI.DTOs
{
    public class SessaoApoioDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataSessao { get; set; }
        public string TipoSessao { get; set; } = string.Empty;
        public string TemaSessao { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Observacoes { get; set; }
        public int DuracaoMinutos { get; set; }
        public string? Facilitador { get; set; }
    }

    public class CreateSessaoApoioDTO
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public DateTime DataSessao { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoSessao { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string TemaSessao { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Descricao { get; set; }

        [StringLength(500)]
        public string? Observacoes { get; set; }

        public int DuracaoMinutos { get; set; } = 60;

        [StringLength(100)]
        public string? Facilitador { get; set; }
    }

    public class UpdateSessaoApoioDTO
    {
        public DateTime? DataSessao { get; set; }

        [StringLength(50)]
        public string? TipoSessao { get; set; }

        [StringLength(100)]
        public string? TemaSessao { get; set; }

        [StringLength(1000)]
        public string? Descricao { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(500)]
        public string? Observacoes { get; set; }

        public int? DuracaoMinutos { get; set; }

        [StringLength(100)]
        public string? Facilitador { get; set; }
    }
}
