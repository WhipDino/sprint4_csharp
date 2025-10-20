using System.ComponentModel.DataAnnotations;

namespace SinaisAPI.Models
{
    public class RelatorioProgresso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public DateTime DataRelatorio { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string TipoRelatorio { get; set; } = string.Empty; // Semanal, Mensal, Trimestral

        public int DiasSobriedade { get; set; } = 0;

        public int SessoesRealizadas { get; set; } = 0;

        public int AlertasEnviados { get; set; } = 0;

        [StringLength(1000)]
        public string? Observacoes { get; set; }

        [StringLength(50)]
        public string StatusGeral { get; set; } = "Bom"; // Excelente, Bom, Regular, Preocupante

        public decimal PontuacaoProgresso { get; set; } = 0;

        [StringLength(500)]
        public string? MetasAlcancadas { get; set; }

        [StringLength(500)]
        public string? DesafiosIdentificados { get; set; }

        // Navegação
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
