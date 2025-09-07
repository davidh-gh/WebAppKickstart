using System.Diagnostics.CodeAnalysis;

namespace Domain.Data.Tables;

[SuppressMessage("Usage", "CA2227:Collection properties should be read only")]
public class ModuleDb
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public ICollection<SubModuleDb> SubModules { get; set; } = new List<SubModuleDb>();
}
