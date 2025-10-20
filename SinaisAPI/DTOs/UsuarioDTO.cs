using System.ComponentModel.DataAnnotations;

namespace SinaisAPI.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public string? Observacoes { get; set; }
    }

    public class CreateUsuarioDTO
    {
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

        [Required]
        public DateTime DataNascimento { get; set; }

        [StringLength(10)]
        public string Genero { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Observacoes { get; set; }
    }

    public class UpdateUsuarioDTO
    {
        [StringLength(100)]
        public string? Nome { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Telefone { get; set; }

        public DateTime? DataNascimento { get; set; }

        [StringLength(10)]
        public string? Genero { get; set; }

        [StringLength(500)]
        public string? Observacoes { get; set; }

        public bool? Ativo { get; set; }
    }
}
