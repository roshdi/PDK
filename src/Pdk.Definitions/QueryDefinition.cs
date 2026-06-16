using System.Data;

namespace Pdk.Definitions;

public sealed class QueryDefinition : FeatureDefinition
{
    public QueryDefinition(EntityDefinition entityDefinition, QueryModelType queryType, string name)
    {
        this.EntityDefinition = entityDefinition;
        QueryType = queryType;
        Name = name;
    }

    public QueryModelType QueryType { get; set; }

    public List<PropertyDefinition> Properties { get; } = [];

    public EndpointDefinition Endpoint { get; set; }
        = new();
}