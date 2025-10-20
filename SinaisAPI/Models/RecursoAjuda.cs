using System.ComponentModel.DataAnnotations;

namespace SinaisAPI.Models
{
    public class RecursoAjuda
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Categoria { get; set; } = string.Empty; // Artigo, Video, Contato, Link

        [Required]
        [StringLength(1000)]
        public string Descricao { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Url { get; set; }

        [StringLength(20)]
        public string? Telefone { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string Prioridade { get; set; } = "Media"; // Baixa, Media, Alta

        [StringLength(100)]
        public string? Tags { get; set; } // Separadas por vírgula

        public int Visualizacoes { get; set; } = 0;
    }
}
