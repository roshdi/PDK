using System.Reflection.Metadata;

namespace Pdk.Definitions;

public sealed class CommandDefinition : FeatureDefinition
{
    public CommandDefinition(EntityDefinition entityDefinition, CommandModelType commandType, string name)
    {
        this.EntityDefinition = entityDefinition;
        CommandType = commandType;
        Name = name;
    }

    public CommandModelType CommandType { get; init; }

    public List<PropertyDefinition> Properties { get; } = [];

    public override string ToString()
    {
        return "CommandDefinition : " + CommandType + " - " + Name;
    }

    public List<DetailDefinition> Details { get; } = [];

    public EndpointDefinition Endpoint { get; set; }
        = new();
}
