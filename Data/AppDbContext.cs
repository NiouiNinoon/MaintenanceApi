using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MaintenanceApi.Models;

namespace MaintenanceApi.Data;
public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<ServiceType> ServiceTypes => Set<ServiceType>();
    public DbSet<Material> Materials => Set<Material>();
    public DbSet<Intervention> Interventions => Set<Intervention>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Intervention>()
            .HasMany(i => i.Technicians)
            .WithMany(u => u.Interventions);

        builder.Entity<Intervention>()
            .HasMany(i => i.Materials);
    }
}
