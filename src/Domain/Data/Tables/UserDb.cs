using Domain.Data.Tables.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Data.Tables;

public class UserDb : IdentityUser, IUniqueId
{
    public new Guid Id { get; init; } = Guid.CreateVersion7();

    public string? Name { get; init; }
}