namespace Pdk.Definitions;

public abstract class FeatureDefinition
{
    public string Name { get; init; } = string.Empty;

    public string? StableId { get; set; }

    public EntityDefinition? EntityDefinition { get; init; }
}
