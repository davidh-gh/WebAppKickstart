using System.Diagnostics.CodeAnalysis;

namespace Domain.Data.Tables;

[SuppressMessage("Usage", "CA2227:Collection properties should be read only")]
public class AdminUserRoleDb
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public ICollection<AdminUserRolePermissionDb> Permissions { get; set; } = new List<AdminUserRolePermissionDb>();
}