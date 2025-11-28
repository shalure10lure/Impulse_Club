using Microsoft.EntityFrameworkCore;
using ImpulseClub.Entities;

namespace ImpulseClub.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Club> Clubs => Set<Club>();
        public DbSet<Training> Trainings => Set<Training>();
        public DbSet<Resource> Resources => Set<Resource>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Email).IsUnique();

                // 1:1 Relationship User <-> Club (founder)
                entity.HasOne(u => u.FoundedClub)
                    .WithOne(c => c.Founder)
                    .HasForeignKey<User>(u => u.FoundedClubId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure Club
            modelBuilder.Entity<Club>(entity =>
            {
                entity.HasKey(c => c.Id);

                // N:M Relationship Club <-> User (members)
                entity.HasMany(c => c.Members)
                    .WithMany(u => u.Clubs)
                    .UsingEntity<Dictionary<string, object>>(
                        "ClubMembers",
                        j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                        j => j.HasOne<Club>().WithMany().HasForeignKey("ClubId").OnDelete(DeleteBehavior.Cascade)
                    );

                // 1:N Relationship Club -> Trainings
                entity.HasMany(c => c.Trainings)
                    .WithOne(t => t.Club)
                    .HasForeignKey(t => t.ClubId)
                    .OnDelete(DeleteBehavior.Cascade);

                // 1:N Relationship Club -> Resources
                entity.HasMany(c => c.Resources)
                    .WithOne(r => r.Club)
                    .HasForeignKey(r => r.ClubId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Training
            modelBuilder.Entity<Training>(entity =>
            {
                entity.HasKey(t => t.Id);

                // N:M Relationship Training <-> User (participants)
                entity.HasMany(t => t.Participants)
                    .WithMany(u => u.Trainings)
                    .UsingEntity<Dictionary<string, object>>(
                        "TrainingParticipants",
                        j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                        j => j.HasOne<Training>().WithMany().HasForeignKey("TrainingId").OnDelete(DeleteBehavior.Cascade)
                    );
            });

            // Configure Resource
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(r => r.Id);

                // N:M Relationship Resource <-> User (reservations)
                entity.HasMany(r => r.UsersWhoReserved)
                    .WithMany(u => u.ReservedResources)
                    .UsingEntity<Dictionary<string, object>>(
                        "ResourceReservations",
                        j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                        j => j.HasOne<Resource>().WithMany().HasForeignKey("ResourceId").OnDelete(DeleteBehavior.Cascade)
                    );
            });
        }
    }
}
