using Domain.Data.Tables.Base;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Data.Tables;

public class SiteDb : UniqueId
{
    public Guid Slug { get; init; } = Guid.CreateVersion7();

    public required string Name { get; set; }

    public string? Description { get; set; }

    [SuppressMessage("Design", "CA1056:URI-like properties should not be strings")]
    public string? BaseUrl { get; set; }

    public string? OwnerId { get; set; }

    public bool IsActive { get; set; } = true;
}
