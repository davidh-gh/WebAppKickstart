namespace Domain.Data.Tables;

public class AdminExternalLoginDb
{
    public Guid Id { get; set; }

    public Guid AdminUserId { get; set; }

    public required string Provider { get; set; }

    public required string ProviderKey { get; set; }

    public required AdminUserDb AdminUser { get; set; }
}