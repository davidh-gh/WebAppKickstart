namespace Domain.Data.Tables;

public class UserSubModuleSubscriptionDb
{
    public Guid Id { get; set; }

    public Guid AppUserId { get; set; }

    public Guid SubModuleId { get; set; }

    public DateTime SubscribedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public required UserDb AppUser { get; set; }

    public required SubModuleDb SubModule { get; set; }
}