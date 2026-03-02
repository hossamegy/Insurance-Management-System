using InsuranceAdministration.Core.Entities.MissionEntities;
using InsuranceAdministration.Core.Entities.Settings;
using InsuranceAdministration.Core.Entities.SoldierEntities;
using InsuranceAdministration.Core.Entities.SoldierEntities.Acquaintance;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAdministration.Infrastructure.Persistance.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // PoliceMan related tables
        public DbSet<Mission> Mission { get; set; }

        // Soldier related tables
        public DbSet<Soldier> Soldier { get; set; }
        public DbSet<SoldierMission> SoldierMission { get; set; }
        public DbSet<AcquaintanceDocument> AcquaintanceDocument { get; set; }
        public DbSet<BaseFamily> BaseFamily { get; set; }
        public DbSet<Family> Family { get; set; }
        public DbSet<SoldierLeaveOptions> SoldierLeaveOptions { get; set; }
        public DbSet<DailyMission> DailyMissions { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<PoliticalAndCriminal> PoliticalAndCriminal { get; set; }

        // Settings Options tables
        public DbSet<MainSettings> MainSettings { get; set; }
        public DbSet<EducationLevelOptions> EducationLevelOptions { get; set; }
        public DbSet<AssignmentOptions> AssignmentOptions { get; set; }
        public DbSet<DailyMealOptions> dailyMealOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
  
            modelBuilder.Entity<SoldierMission>()
                    .HasOne(sm => sm.DailyMission)
                    .WithMany(dm => dm.SoldierMissions)
                    .HasForeignKey(sm => sm.DailyMissionId)
                    .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
            // SoldierMission configuration - allows same soldier to have same mission multiple times
            modelBuilder.Entity<SoldierMission>(entity =>
            {
                entity.HasKey(sm => sm.Id);

                entity.HasOne(sm => sm.Soldier)
                    .WithMany(s => s.SoldierMissions)
                    .HasForeignKey(sm => sm.SoldierId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(sm => sm.Mission)
                    .WithMany()
                    .HasForeignKey(sm => sm.MissionId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Non-unique index for performance (allows duplicate soldier-mission combinations)
                entity.HasIndex(sm => new { sm.SoldierId, sm.MissionId });

                // Index on AssignedAt for date queries
                entity.HasIndex(sm => sm.AssignedAt);
            });

            // Soldier - AcquaintanceDocument (One-to-One)
            modelBuilder.Entity<Soldier>()
                .HasOne(s => s.AcquaintanceDocument)
                .WithOne(a => a.Soldier)
                .HasForeignKey<AcquaintanceDocument>(a => a.SoldierId)
                .OnDelete(DeleteBehavior.Cascade);

            // AcquaintanceDocument - BaseFamily (One-to-Many)
            modelBuilder.Entity<BaseFamily>()
                .HasOne(b => b.AcquaintanceDocument)
                .WithMany(a => a.BaseFamily)
                .HasForeignKey(b => b.AcquaintanceDocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            // AcquaintanceDocument - Family (One-to-Many)
            modelBuilder.Entity<Family>()
                .HasOne(f => f.AcquaintanceDocument)
                .WithMany(a => a.Family)
                .HasForeignKey(f => f.AcquaintanceDocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Soldier - SoldierLeave (One-to-Many)
            modelBuilder.Entity<Soldier>()
                .HasMany(s => s.Leaves)
                .WithOne(l => l.Soldier)
                .HasForeignKey(l => l.SoldierId)
                .OnDelete(DeleteBehavior.Cascade);  

            // Unique constraint on Soldier NationalId
            modelBuilder.Entity<Soldier>()
                .HasIndex(s => s.NationalId)
                .IsUnique();

            // Index on Soldier TripleNumber
            modelBuilder.Entity<Soldier>()
                .HasIndex(s => s.TripleNumber);
        }
    }
}