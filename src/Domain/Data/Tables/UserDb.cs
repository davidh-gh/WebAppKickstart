using Domain.Data.Tables.Base;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Data.Tables;

[SuppressMessage("Usage", "CA2227:Collection properties should be read only")]
public class UserDb : IdentityUser, IUniqueId
{
    public new Guid Id { get; init; } = Guid.CreateVersion7();

    public string? Name { get; set; }

    public ICollection<UserModuleSubscriptionDb> ModuleSubscriptions { get; set; } = new List<UserModuleSubscriptionDb>();

    public ICollection<UserSubModuleSubscriptionDb> SubModuleSubscriptions { get; set; } = new List<UserSubModuleSubscriptionDb>();

    public ICollection<UserPermissionDb> Permissions { get; set; } = new List<UserPermissionDb>();
}
