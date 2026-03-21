using Microsoft.EntityFrameworkCore;
using sportdesk_backend.Models;

namespace sportdesk_backend.Infra;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Athlete> Athletes { get; set; } = null!;
    public DbSet<AthleteEnrollment> AthleteEnrollments { get; set; } = null!;
    public DbSet<CoachSport> CoachSports { get; set; } = null!;
    public DbSet<Enrollment> Enrollments { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Sport> Sports { get; set; } = null!;
    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Athlete>().ToTable("athletes");
        modelBuilder.Entity<AthleteEnrollment>().ToTable("athlete_enrollments");
        modelBuilder.Entity<CoachSport>().ToTable("coach_sports");
        modelBuilder.Entity<Enrollment>().ToTable("enrollments");
        modelBuilder.Entity<Payment>().ToTable("payments");
        modelBuilder.Entity<Sport>().ToTable("sports");
        modelBuilder.Entity<Tenant>().ToTable("tenants");
        modelBuilder.Entity<User>().ToTable("users");

        // Tenant relationships
        modelBuilder.Entity<Athlete>()
            .HasOne(e => e.Tenant).WithMany().HasForeignKey(e => e.TenantId);
        modelBuilder.Entity<AthleteEnrollment>()
            .HasOne(e => e.Tenant).WithMany().HasForeignKey(e => e.TenantId);
        modelBuilder.Entity<CoachSport>()
            .HasOne(e => e.Tenant).WithMany().HasForeignKey(e => e.TenantId);
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Tenant).WithMany().HasForeignKey(e => e.TenantId);
        modelBuilder.Entity<Payment>()
            .HasOne(e => e.Tenant).WithMany().HasForeignKey(e => e.TenantId);
        modelBuilder.Entity<Sport>()
            .HasOne(e => e.Tenant).WithMany().HasForeignKey(e => e.TenantId);
        modelBuilder.Entity<User>()
            .HasOne(e => e.Tenant).WithMany().HasForeignKey(e => e.TenantId);

        // Entity relationships
        modelBuilder.Entity<CoachSport>()
            .HasOne(e => e.Coach).WithMany().HasForeignKey(e => e.CoachId);
        modelBuilder.Entity<CoachSport>()
            .HasOne(e => e.Sport).WithMany().HasForeignKey(e => e.SportId);
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.CoachSport).WithMany().HasForeignKey(e => e.CoachSportId);
        modelBuilder.Entity<AthleteEnrollment>()
            .HasOne(e => e.Enrollment).WithMany().HasForeignKey(e => e.EnrollmentId);
        modelBuilder.Entity<AthleteEnrollment>()
            .HasOne(e => e.Athlete).WithMany().HasForeignKey(e => e.AthleteId);
        modelBuilder.Entity<Payment>()
            .HasOne(e => e.AthleteEnrollment).WithMany().HasForeignKey(e => e.AthleteEnrollmentId);
    }
}