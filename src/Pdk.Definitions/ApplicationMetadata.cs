using System.Collections.Concurrent;

namespace Pdk.Definitions;

public sealed class ApplicationMetadata
{
    private Lazy<Dictionary<Type, EntityDefinition>> LazyMetadata;
    public List<EntityDefinition> Entities { get; } = [];

    public ApplicationMetadata()
    {
        this.LazyMetadata = new(this.ToDictionary);
    }


    private Dictionary<Type, EntityDefinition> ToDictionary()
    {
        return this.Entities.ToDictionary(e => e.EntityType);
    }

    public EntityDefinition? GetEntityDefinition(Type entityType)
    {
        if (LazyMetadata.Value.TryGetValue(entityType, out var entity))
            return entity;
        return null;
    }
}
