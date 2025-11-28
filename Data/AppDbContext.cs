using Microsoft.EntityFrameworkCore;
using ImpulseClub.Entities;

namespace ImpulseClub.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Club> Clubes => Set<Club>();
        public DbSet<Entrenamiento> Entrenamientos => Set<Entrenamiento>();
        public DbSet<Recurso> Recursos => Set<Recurso>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Email).IsUnique();

                // Relación 1:1 Usuario <-> Club (fundador)
                entity.HasOne(u => u.ClubFundado)
                    .WithOne(c => c.Fundador)
                    .HasForeignKey<Usuario>(u => u.ClubFundadoId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configurar Club
            modelBuilder.Entity<Club>(entity =>
            {
                entity.HasKey(c => c.Id);

                // Relación N:M Club <-> Usuario (miembros)
                entity.HasMany(c => c.Miembros)
                    .WithMany(u => u.Clubes)
                    .UsingEntity<Dictionary<string, object>>(
                        "ClubMiembros",
                        j => j.HasOne<Usuario>().WithMany().HasForeignKey("UsuarioId"),
                        j => j.HasOne<Club>().WithMany().HasForeignKey("ClubId")
                    );

                // Relación 1:N Club -> Entrenamientos
                entity.HasMany(c => c.Entrenamientos)
                    .WithOne(e => e.Club)
                    .HasForeignKey(e => e.ClubId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relación 1:N Club -> Recursos
                entity.HasMany(c => c.Recursos)
                    .WithOne(r => r.Club)
                    .HasForeignKey(r => r.ClubId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configurar Entrenamiento
            modelBuilder.Entity<Entrenamiento>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Relación N:M Entrenamiento <-> Usuario (participantes)
                entity.HasMany(e => e.Participantes)
                    .WithMany(u => u.Entrenamientos)
                    .UsingEntity<Dictionary<string, object>>(
                        "EntrenamientoParticipantes",
                        j => j.HasOne<Usuario>().WithMany().HasForeignKey("UsuarioId"),
                        j => j.HasOne<Entrenamiento>().WithMany().HasForeignKey("EntrenamientoId")
                    );
            });

            // Configurar Recurso
            modelBuilder.Entity<Recurso>(entity =>
            {
                entity.HasKey(r => r.Id);

                // Relación N:M Recurso <-> Usuario (reservas)
                entity.HasMany(r => r.UsuariosQueReservaron)
                    .WithMany(u => u.RecursosReservados)
                    .UsingEntity<Dictionary<string, object>>(
                        "RecursoReservas",
                        j => j.HasOne<Usuario>().WithMany().HasForeignKey("UsuarioId"),
                        j => j.HasOne<Recurso>().WithMany().HasForeignKey("RecursoId")
                    );
            });
        }
    }
}
