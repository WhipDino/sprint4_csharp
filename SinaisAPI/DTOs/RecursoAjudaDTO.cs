using System.ComponentModel.DataAnnotations;

namespace SinaisAPI.DTOs
{
    public class RecursoAjudaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Prioridade { get; set; } = string.Empty;
        public string? Tags { get; set; }
        public int Visualizacoes { get; set; }
    }

    public class CreateRecursoAjudaDTO
    {
        [Required]
        [StringLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Categoria { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Descricao { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Url { get; set; }

        [StringLength(20)]
        public string? Telefone { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(50)]
        public string Prioridade { get; set; } = "Media";

        [StringLength(100)]
        public string? Tags { get; set; }
    }

    public class UpdateRecursoAjudaDTO
    {
        [StringLength(100)]
        public string? Titulo { get; set; }

        [StringLength(50)]
        public string? Categoria { get; set; }

        [StringLength(1000)]
        public string? Descricao { get; set; }

        [StringLength(500)]
        public string? Url { get; set; }

        [StringLength(20)]
        public string? Telefone { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        public bool? Ativo { get; set; }

        [StringLength(50)]
        public string? Prioridade { get; set; }

        [StringLength(100)]
        public string? Tags { get; set; }
    }
}
