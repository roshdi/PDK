using Microsoft.EntityFrameworkCore;

namespace Pdk.Definitions;

public sealed class ForeignKeyDefinition
{
    public string Name { get; set; } = "";

    public string PrincipalEntity { get; set; } = "";

    public string? NavigationProperty { get; set; }

    public DeleteBehavior DeleteBehavior { get; set; }

    public List<string> Properties { get; } = [];
}
