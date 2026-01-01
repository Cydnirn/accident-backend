using AccidentBackend.Models;
using Microsoft.EntityFrameworkCore;
namespace AccidentBackend.Data
{
    public class AccidentDbContext : DbContext
    {
        public AccidentDbContext(DbContextOptions<AccidentDbContext> options)
            : base(options)
        {
        }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<HazardType> HazardTypes { get; set; }
        public DbSet<AccidentCause> AccidentCauses { get; set; }
        public DbSet<SafetyEquipment> SafetyEquipments { get; set; }
        public DbSet<Accident> Accidents { get; set; }
        public DbSet<AccidentParticipant> AccidentParticipants { get; set; }
        public DbSet<AccidentEquipment> AccidentEquipments { get; set; }
        public DbSet<Witness> Witnesses { get; set; }
        public DbSet<ActionTaken> ActionsTaken { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Site>(entity =>
            {
                entity.HasIndex(e => e.SiteCode).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");
            });
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasOne(d => d.ManagerWorker)
                    .WithMany()
                    .HasForeignKey(d => d.ManagerWorkerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasIndex(e => e.EmployeeNumber).IsUnique();
                entity.HasOne(w => w.Department)
                    .WithMany(d => d.Workers)
                    .HasForeignKey(w => w.DepartmentId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(w => w.CurrentSite)
                    .WithMany(s => s.Workers)
                    .HasForeignKey(w => w.CurrentSiteId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<HazardType>(entity =>
            {
                entity.HasIndex(e => e.Code).IsUnique();
            });
            modelBuilder.Entity<AccidentCause>(entity =>
            {
                entity.HasIndex(e => e.Code).IsUnique();
            });
            modelBuilder.Entity<SafetyEquipment>(entity =>
            {
                entity.HasIndex(e => e.TagNumber).IsUnique();
            });
            modelBuilder.Entity<Accident>(entity =>
            {
                entity.HasIndex(e => e.AccidentNumber).IsUnique();
                entity.HasIndex(e => e.SiteId);
                entity.HasIndex(e => e.OccurredAt);
                entity.HasIndex(e => e.SeverityLevel);
                entity.HasIndex(e => e.Status);
                entity.Property(e => e.ReportedAt).HasDefaultValueSql("datetime('now')");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");
                entity.HasOne(a => a.Site)
                    .WithMany(s => s.Accidents)
                    .HasForeignKey(a => a.SiteId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(a => a.ReportedByWorker)
                    .WithMany(w => w.ReportedAccidents)
                    .HasForeignKey(a => a.ReportedByWorkerId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(a => a.Shift)
                    .WithMany(s => s.Accidents)
                    .HasForeignKey(a => a.ShiftId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(a => a.HazardType)
                    .WithMany(h => h.Accidents)
                    .HasForeignKey(a => a.HazardTypeId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(a => a.Cause)
                    .WithMany(c => c.Accidents)
                    .HasForeignKey(a => a.CauseId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<AccidentParticipant>(entity =>
            {
                entity.HasOne(ap => ap.Accident)
                    .WithMany(a => a.Participants)
                    .HasForeignKey(ap => ap.AccidentId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ap => ap.Worker)
                    .WithMany(w => w.AccidentParticipations)
                    .HasForeignKey(ap => ap.WorkerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<AccidentEquipment>(entity =>
            {
                entity.HasOne(ae => ae.Accident)
                    .WithMany(a => a.AccidentEquipments)
                    .HasForeignKey(ae => ae.AccidentId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ae => ae.Equipment)
                    .WithMany(e => e.AccidentEquipments)
                    .HasForeignKey(ae => ae.EquipmentId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Witness>(entity =>
            {
                entity.HasOne(w => w.Accident)
                    .WithMany(a => a.Witnesses)
                    .HasForeignKey(w => w.AccidentId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(w => w.Worker)
                    .WithMany(wk => wk.WitnessStatements)
                    .HasForeignKey(w => w.WorkerId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.Property(w => w.RecordedAt).HasDefaultValueSql("datetime('now')");
            });
            modelBuilder.Entity<ActionTaken>(entity =>
            {
                entity.HasOne(at => at.Accident)
                    .WithMany(a => a.ActionsTaken)
                    .HasForeignKey(at => at.AccidentId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(at => at.PerformedByWorker)
                    .WithMany(w => w.ActionsTaken)
                    .HasForeignKey(at => at.PerformedByWorkerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasOne(a => a.Accident)
                    .WithMany(ac => ac.Attachments)
                    .HasForeignKey(a => a.AccidentId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.UploadedByWorker)
                    .WithMany(w => w.UploadedAttachments)
                    .HasForeignKey(a => a.UploadedByWorkerId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.Property(a => a.UploadedAt).HasDefaultValueSql("datetime('now')");
            });
        }
    }
}
