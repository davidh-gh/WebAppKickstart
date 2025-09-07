namespace Domain.Data.Tables.Base;

public class UniqueId : IUniqueId
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
}
