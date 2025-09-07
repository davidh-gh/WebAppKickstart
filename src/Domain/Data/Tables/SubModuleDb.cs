using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Tables;

public class SubModuleDb
{
    public Guid Id { get; set; }

    public Guid ModuleId { get; set; }

    [Required]
    public required string Name { get; set; }

    public required ModuleDb Module { get; set; }
}
