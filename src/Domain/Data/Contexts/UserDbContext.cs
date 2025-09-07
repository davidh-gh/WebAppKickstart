using Domain.Data.Tables;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data.Contexts;

public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
    public DbSet<UserDb>? AppUsers { get; set; }

    public DbSet<ModuleDb>? Modules { get; set; }

    public DbSet<SubModuleDb>? SubModules { get; set; }

    public DbSet<UserModuleSubscriptionDb>? ModuleSubscriptions { get; set; }

    public DbSet<UserSubModuleSubscriptionDb>? SubModuleSubscriptions { get; set; }

    public DbSet<UserPermissionDb>? UserPermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserDb>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.PasswordHash).IsRequired();
        });

        modelBuilder.Entity<ModuleDb>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<SubModuleDb>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

            entity.HasOne(e => e.Module)
                .WithMany(m => m.SubModules)
                .HasForeignKey(e => e.ModuleId);
        });

        modelBuilder.Entity<UserModuleSubscriptionDb>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.AppUser)
                .WithMany(u => u.ModuleSubscriptions)
                .HasForeignKey(e => e.UserId);

            entity.HasOne(e => e.Module)
                .WithMany()
                .HasForeignKey(e => e.ModuleId);
        });

        modelBuilder.Entity<UserSubModuleSubscriptionDb>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.AppUser)
                .WithMany(u => u.SubModuleSubscriptions)
                .HasForeignKey(e => e.AppUserId);

            entity.HasOne(e => e.SubModule)
                .WithMany()
                .HasForeignKey(e => e.SubModuleId);
        });

        modelBuilder.Entity<UserPermissionDb>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PermissionKey).IsRequired().HasMaxLength(200);

            entity.HasOne(e => e.AppUser)
                .WithMany(u => u.Permissions)
                .HasForeignKey(e => e.AppUserId);
        });
    }
}
