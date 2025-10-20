using System.ComponentModel.DataAnnotations;

namespace SinaisAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Telefone { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }

        [StringLength(10)]
        public string Genero { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public bool Ativo { get; set; } = true;

        [StringLength(500)]
        public string? Observacoes { get; set; }

        // Navegação
        public virtual ICollection<SessaoApoio> SessoesApoio { get; set; } = new List<SessaoApoio>();
        public virtual ICollection<Alerta> Alertas { get; set; } = new List<Alerta>();
        public virtual ICollection<RelatorioProgresso> RelatoriosProgresso { get; set; } = new List<RelatorioProgresso>();
    }
}
