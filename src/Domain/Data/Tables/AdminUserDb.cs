using Domain.Data.Tables.Base;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Data.Tables;

[SuppressMessage("Usage", "CA2227:Collection properties should be read only")]
public class AdminUserDb : IdentityUser, IUniqueId
{
    public new Guid Id { get; init; } = Guid.CreateVersion7();

    public ICollection<AdminUserRoleDb> Roles { get; set; } = new List<AdminUserRoleDb>();

    public ICollection<AdminExternalLoginDb> ExternalLogins { get; set; } = new List<AdminExternalLoginDb>();
}
