using Pdk.Definitions.Extensions;
using System.Linq.Expressions;

namespace Pdk.Definitions;

public sealed class QueryBuilder<TEntity>
{
    private readonly QueryDefinition _query;

    internal QueryBuilder(QueryDefinition query)
    {
        _query = query;
    }

    List<string> ExcludedFields = new();
    public QueryBuilder<TEntity> Exclude<TProperty>(
        Expression<Func<TEntity, TProperty>> selector)
    {
        var name = selector.GetMemberName();

        ExcludedFields.Add(name);

        return this;
    }

    internal void Build()
    {
        foreach (var property in _query.EntityDefinition.Properties)
        {
            if (ExcludedFields.Contains(property.Name))
                continue;
            switch (_query.QueryType)
            {
                case QueryModelType.Item:
                    if (property.IsPrimaryKey)
                        continue;
                    if (property.IsConcurrencyToken)
                        continue;
                    break;
                case QueryModelType.List:
                    break;
            }
            _query.Properties.Add(property);
        }
    }
}

