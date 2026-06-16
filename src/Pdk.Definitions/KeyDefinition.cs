namespace Pdk.Definitions;

public sealed record KeyDefinition(bool IsPrimaryKey)
{
    public List<string> Properties { get; } = [];
}
