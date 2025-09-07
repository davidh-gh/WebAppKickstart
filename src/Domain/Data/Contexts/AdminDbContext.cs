using Domain.Data.Tables;

namespace Domain.Data.Contexts;

using Microsoft.EntityFrameworkCore;

public class AdminDbContext(DbContextOptions<AdminDbContext> options) : DbContext(options)
{
    public DbSet<AdminUserDb>? AdminUsers { get; set; }

    public DbSet<AdminUserRoleDb>? AdminUserRoles { get; set; }

    public DbSet<AdminUserRolePermissionDb>? AdminUserRolePermissions { get; set; }

    public DbSet<AdminExternalLoginDb>? ExternalLogins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AdminUserDb>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.PasswordHash).IsRequired();
        });

        modelBuilder.Entity<AdminExternalLoginDb>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Provider).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ProviderKey).IsRequired().HasMaxLength(200);

            entity.HasOne(e => e.AdminUser)
                .WithMany(u => u.ExternalLogins)
                .HasForeignKey(e => e.AdminUserId);
        });

        modelBuilder.Entity<AdminUserRoleDb>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<AdminUserRolePermissionDb>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PermissionKey).IsRequired().HasMaxLength(200);

            entity.HasOne(e => e.Role)
                .WithMany(r => r.Permissions)
                .HasForeignKey(e => e.AdminUserRoleId);
        });
    }
}
