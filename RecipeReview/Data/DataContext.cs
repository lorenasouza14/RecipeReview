using Microsoft.EntityFrameworkCore;
using RecipeReview.Models;
namespace RecipeReview.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Usuario> UsuarioTable { get; set; } 
        public DbSet<Receita> ReceitaTable { get; set; }
        public DbSet<ReceitaDoce> ReceitaDoceTable { get; set; }
        public DbSet<ReceitaSalgada> ReceitaSalgadaTable { get; set; }
        public DbSet<Bebida> BebidaTable { get; set; }

        public DbSet<Ingrediente> IngredienteTable { get; set; }
        public DbSet<ReceitaIngrediente> ReceitasIngredientesTable { get; set; }
        
        public DbSet<Avaliacao> AvaliacaoTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Receita>()
                .HasDiscriminator<string>("Tipo")
                .HasValue<ReceitaDoce>("Doce")
                .HasValue<ReceitaSalgada>("Salgada")
                .HasValue<Bebida>("Bebida");

            modelBuilder.Entity<ReceitaIngrediente>(entity =>
            {
                entity.HasKey(ri => new { ri.ReceitaId, ri.IngredienteId });

                entity.HasOne(ri => ri.Receita)
                    .WithMany(r => r.Ingredientes)
                    .HasForeignKey(ri => ri.ReceitaId);

                entity.HasOne(ri => ri.Ingrediente)
                    .WithMany(i => i.Receitas)
                    .HasForeignKey(ri => ri.IngredienteId);

                entity.Property(ri => ri.Quantidade)
                    .HasPrecision(10, 3);
            });

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.ReceitasCriadas)
                .WithOne(r => r.Usuario)
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Avaliacoes)
                .WithOne(a => a.Usuario)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receita>()
                .HasMany(r => r.Avaliacoes)
                .WithOne(a => a.Receita)
                .HasForeignKey(a => a.ReceitaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
