using Microsoft.EntityFrameworkCore;
using SinaisAPI.Models;

namespace SinaisAPI.Data
{
    public class SinaisDbContext : DbContext
    {
        public SinaisDbContext(DbContextOptions<SinaisDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<SessaoApoio> SessoesApoio { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<RelatorioProgresso> RelatoriosProgresso { get; set; }
        public DbSet<RecursoAjuda> RecursosAjuda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações de relacionamento
            modelBuilder.Entity<SessaoApoio>()
                .HasOne(s => s.Usuario)
                .WithMany(u => u.SessoesApoio)
                .HasForeignKey(s => s.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Alerta>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Alertas)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RelatorioProgresso>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.RelatoriosProgresso)
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurações de índices
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Telefone)
                .IsUnique();

            modelBuilder.Entity<Alerta>()
                .HasIndex(a => new { a.UsuarioId, a.DataCriacao });

            modelBuilder.Entity<RelatorioProgresso>()
                .HasIndex(r => new { r.UsuarioId, r.DataRelatorio });

            // Configurações de strings
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nome)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Telefone)
                .HasMaxLength(20)
                .IsRequired();

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Dados iniciais para recursos de ajuda
            modelBuilder.Entity<RecursoAjuda>().HasData(
                new RecursoAjuda
                {
                    Id = 1,
                    Titulo = "Centro de Valorização da Vida (CVV)",
                    Categoria = "Contato",
                    Descricao = "Serviço de prevenção do suicídio e apoio emocional",
                    Telefone = "188",
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    Prioridade = "Alta",
                    Tags = "emergencia,suicidio,apoio"
                },
                new RecursoAjuda
                {
                    Id = 2,
                    Titulo = "Alcoólicos Anônimos (AA)",
                    Categoria = "Link",
                    Descricao = "Programa de recuperação para dependentes de álcool",
                    Url = "https://www.aa.org.br",
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    Prioridade = "Alta",
                    Tags = "alcool,recuperacao,grupo"
                },
                new RecursoAjuda
                {
                    Id = 3,
                    Titulo = "Jogadores Anônimos (JA)",
                    Categoria = "Link",
                    Descricao = "Programa de recuperação para dependentes de jogos",
                    Url = "https://www.jogadoresanonimos.org.br",
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    Prioridade = "Alta",
                    Tags = "jogos,recuperacao,grupo"
                },
                new RecursoAjuda
                {
                    Id = 4,
                    Titulo = "Dicas para Evitar Recaídas",
                    Categoria = "Artigo",
                    Descricao = "Estratégias práticas para manter a sobriedade",
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    Prioridade = "Media",
                    Tags = "dicas,prevencao,recaida"
                }
            );
        }
    }
}
