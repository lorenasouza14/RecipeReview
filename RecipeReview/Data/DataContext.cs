using Microsoft.EntityFrameworkCore;
using RecipeReview.Classes;
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
            // HERANÇA — TPH (tudo em uma tabela só)
            modelBuilder.Entity<Receita>()
                .HasDiscriminator<string>("Tipo")
                .HasValue<ReceitaDoce>("Doce")
                .HasValue<ReceitaSalgada>("Salgada")
                .HasValue<Bebida>("Bebida");

            // RELAÇÃO N:N COM DADOS (ingredientes)
            modelBuilder.Entity<ReceitaIngrediente>(entity =>
            {
                entity.HasKey(ri => new { ri.ReceitaId, ri.IngredienteId });

                entity.HasOne(ri => ri.Receita)
                    .WithMany(r => r.Ingredientes)
                    .HasForeignKey(ri => ri.ReceitaId);

                entity.HasOne(ri => ri.Ingrediente)
                    .WithMany(i => i.Receitas)
                    .HasForeignKey(ri => ri.IngredienteId);
            });

            // Relação Usuario -> Receitas criadas
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.ReceitasCriadas)
                .WithOne(r => r.Usuario)
                .HasForeignKey(r => r.UsuarioId);

            // Relação Usuario -> Avaliações
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Avaliacoes)
                .WithOne(a => a.Usuario)
                .HasForeignKey(a => a.UsuarioId);

            // Relação Receita -> Avaliações
            modelBuilder.Entity<Receita>()
                .HasMany<Avaliacao>()
                .WithOne(a => a.Receita)
                .HasForeignKey(a => a.ReceitaId);
        }
    }
}
