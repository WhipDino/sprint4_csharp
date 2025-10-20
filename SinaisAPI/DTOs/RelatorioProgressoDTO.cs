using System.ComponentModel.DataAnnotations;

namespace SinaisAPI.DTOs
{
    public class RelatorioProgressoDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataRelatorio { get; set; }
        public string TipoRelatorio { get; set; } = string.Empty;
        public int DiasSobriedade { get; set; }
        public int SessoesRealizadas { get; set; }
        public int AlertasEnviados { get; set; }
        public string? Observacoes { get; set; }
        public string StatusGeral { get; set; } = string.Empty;
        public decimal PontuacaoProgresso { get; set; }
        public string? MetasAlcancadas { get; set; }
        public string? DesafiosIdentificados { get; set; }
    }

    public class CreateRelatorioProgressoDTO
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoRelatorio { get; set; } = string.Empty;

        public int DiasSobriedade { get; set; } = 0;

        public int SessoesRealizadas { get; set; } = 0;

        public int AlertasEnviados { get; set; } = 0;

        [StringLength(1000)]
        public string? Observacoes { get; set; }

        [StringLength(50)]
        public string StatusGeral { get; set; } = "Bom";

        public decimal PontuacaoProgresso { get; set; } = 0;

        [StringLength(500)]
        public string? MetasAlcancadas { get; set; }

        [StringLength(500)]
        public string? DesafiosIdentificados { get; set; }
    }

    public class UpdateRelatorioProgressoDTO
    {
        [StringLength(50)]
        public string? TipoRelatorio { get; set; }

        public int? DiasSobriedade { get; set; }

        public int? SessoesRealizadas { get; set; }

        public int? AlertasEnviados { get; set; }

        [StringLength(1000)]
        public string? Observacoes { get; set; }

        [StringLength(50)]
        public string? StatusGeral { get; set; }

        public decimal? PontuacaoProgresso { get; set; }

        [StringLength(500)]
        public string? MetasAlcancadas { get; set; }

        [StringLength(500)]
        public string? DesafiosIdentificados { get; set; }
    }
}
