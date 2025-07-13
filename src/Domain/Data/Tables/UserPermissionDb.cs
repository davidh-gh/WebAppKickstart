namespace Domain.Data.Tables;

public class UserPermissionDb
{
    public Guid Id { get; set; }

    public Guid AppUserId { get; set; }

    // Can be used for fine-grained control, e.g., "module.site.read", "module.calendar.manage" etc.
    public required string PermissionKey { get; set; }

    public required UserDb AppUser { get; set; }
}