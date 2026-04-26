using Microsoft.EntityFrameworkCore;
using Licensing.Domain.Entities;
using Licensing.Application.Interfaces;

namespace Licensing.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<LicenseApplication> Applications => Set<LicenseApplication>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<Feedback> Feedbacks => Set<Feedback>();
    public DbSet<ApplicationSnapshot> ApplicationSnapshots => Set<ApplicationSnapshot>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configuration for Snapshot relationship
        modelBuilder.Entity<ApplicationSnapshot>()
            .HasOne(s => s.Application)
            .WithMany(a => a.Snapshots)
            .HasForeignKey(s => s.ApplicationId);

        // Configuration for Document relationship
        modelBuilder.Entity<Document>()
            .HasOne(d => d.Application)
            .WithMany(a => a.Documents)
            .HasForeignKey(d => d.ApplicationId);

        // Configuration for Feedback relationship
        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.Application)
            .WithMany(a => a.Feedbacks)
            .HasForeignKey(f => f.ApplicationId);
    }
}
