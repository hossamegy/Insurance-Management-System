using InsuranceAdministration.Core.Entities.DailyMissionEntities;
using InsuranceAdministration.Core.Entities.PoliceManEntities;
using InsuranceAdministration.Core.Entities.Settings;
using InsuranceAdministration.Core.Entities.SoldierEntities;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAdministration.Infrastructure.Persistance.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<PoliceMan> Policemen { get; set; }
        public DbSet<Punishment> Punishments { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Mission> Mission { get; set; }
        public DbSet<Soldier> Soldier { get; set; }
        public DbSet<DailyMission> DailyMissions { get; set; }


        // Settings Options tables
        public DbSet<EducationLevelOptions> EducationLevelOptions { get; set; }
        public DbSet<AssignmentOptions> AssignmentOptions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // DailyMission - Mission (One-to-Many)
            modelBuilder.Entity<DailyMission>()
                .HasMany(dm => dm.Missions)
                .WithOne(m => m.DailyMission)
                .HasForeignKey(m => m.DailyMissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // PoliceMan - Leave (One-to-Many)
            modelBuilder.Entity<PoliceMan>()
                .HasMany(p => p.Leaves)
                .WithOne(l => l.PoliceMan)
                .HasForeignKey(l => l.PolicemanId)
                .OnDelete(DeleteBehavior.Cascade);

            // PoliceMan - Punishment (One-to-Many)
            modelBuilder.Entity<PoliceMan>()
                .HasMany(p => p.Punishments)
                .WithOne(pu => pu.PoliceMan)
                .HasForeignKey(pu => pu.PoliceManId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mission - PoliceMan (Many-to-Many)
            modelBuilder.Entity<Mission>()
                .HasMany(m => m.Policemen)
                .WithMany(p => p.Missions)
                .UsingEntity<Dictionary<string, object>>(
                    "MissionPoliceMan",
                    j => j.HasOne<PoliceMan>()
                          .WithMany()
                          .HasForeignKey("PoliceManId")
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Mission>()
                          .WithMany()
                          .HasForeignKey("MissionId")
                          .OnDelete(DeleteBehavior.Cascade)
                );

            // Mission - Soldier (Many-to-Many)
            modelBuilder.Entity<Mission>()
                .HasMany(m => m.Soldiers)
                .WithMany(s => s.Missions)
                .UsingEntity<Dictionary<string, object>>(
                    "MissionSoldier",
                    j => j.HasOne<Soldier>()
                          .WithMany()
                          .HasForeignKey("SoldierId")
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Mission>()
                          .WithMany()
                          .HasForeignKey("MissionId")
                          .OnDelete(DeleteBehavior.Cascade)
                );

            // Optional: Add indexes for better query performance
            modelBuilder.Entity<Mission>()
                .HasIndex(m => m.DailyMissionId);

            modelBuilder.Entity<Leave>()
                .HasIndex(l => l.PolicemanId);

            modelBuilder.Entity<Punishment>()
                .HasIndex(p => p.PoliceManId);

            modelBuilder.Entity<PoliceMan>()
                .HasIndex(p => p.Name);

            modelBuilder.Entity<Soldier>()
                .HasIndex(s => s.NationalId)
                .IsUnique();

            modelBuilder.Entity<Soldier>()
                .HasIndex(s => s.TripleNumber);
        }

    }
}
