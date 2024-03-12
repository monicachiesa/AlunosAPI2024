using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlunosAPI.Models
{
    public class Contexto : DbContext
    {
        readonly IConfiguration _configuration;
        public DbSet<Aluno> Alunos { get; set; }

        public Contexto(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure PostgreSQL connection
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>(entity =>
            {
                // Fluent API configuration for Aluno entity
                entity.ToTable("alunos"); // Define table name
                entity.HasKey(e => e.Id); // Define primary key

                entity.Property(e => e.Nome)
                    .HasMaxLength(40)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Idade);
            });
        }
    }
}
