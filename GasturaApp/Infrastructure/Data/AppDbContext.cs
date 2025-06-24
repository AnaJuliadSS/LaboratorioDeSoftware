using GasturaApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GasturaApp.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Gasto>(entity =>
            {
                entity.Property(g => g.ModalidadePagamento)
                      .HasConversion<string>();
            });

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Categoria>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.Usuario)
                .WithMany()
                .HasForeignKey(g => g.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.Categoria)
                .WithMany()
                .HasForeignKey(g => g.CategoriaId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Orcamento>()
                .HasOne(o => o.Usuario)
                .WithMany()
                .HasForeignKey(o => o.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Orcamento>()
                .HasOne(o => o.Categoria)
                .WithMany()
                .HasForeignKey(o => o.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 2, // chave primária fixa
                    Nome = "Aninha",
                    Email = "user@example.com",
                    DataCadastro = new DateTime(2025, 5, 12, 22, 52, 1, 634),
                    Senha = "12345665"
                }
            );

            modelBuilder.Entity<Categoria>().HasData(
            new Categoria
            {
                Id = 1,
                Descricao = "Alimentação",
                Cor = "#FF5733",
                UsuarioId = 2
            },
            new Categoria
            {
                Id = 2,
                Descricao = "Transporte",
                Cor = "#33C1FF",
                UsuarioId = 2
            },
            new Categoria
            {
                Id = 3,
                Descricao = "Educação",
                Cor = "#8D33FF",
                UsuarioId = 2
            }
        );
        }
    }
}
