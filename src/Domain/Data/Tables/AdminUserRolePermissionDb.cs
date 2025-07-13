namespace Domain.Data.Tables;

public class AdminUserRolePermissionDb
{
    public Guid Id { get; set; }

    public Guid AdminUserRoleId { get; set; }

    public required string PermissionKey { get; set; }

    public required AdminUserRoleDb Role { get; set; }
}