namespace Domain.Data.Tables;

public class UserModuleSubscriptionDb
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ModuleId { get; set; }

    public DateTime SubscribedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public required UserDb AppUser { get; set; }

    public required ModuleDb Module { get; set; }
}