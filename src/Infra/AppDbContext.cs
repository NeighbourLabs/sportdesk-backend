using Microsoft.EntityFrameworkCore;
using sportdesk_backend.Models;

namespace Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Athlete> Athletes { get; set; } = null!;
    public DbSet<AthleteEnrollments> AthleteEnrollments { get; set; } = null!;
    public DbSet<CoachSport> CoachSports { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<Sport> Sports { get; set; } = null!;
    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly
        );
    }
}