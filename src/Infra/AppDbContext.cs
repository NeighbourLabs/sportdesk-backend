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
    }
}