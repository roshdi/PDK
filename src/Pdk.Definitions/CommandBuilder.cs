using Pdk.Definitions.Extensions;
using System.Linq.Expressions;

namespace Pdk.Definitions;

public sealed class CommandBuilder<TEntity>
{
    private readonly CommandDefinition _command;

    internal CommandBuilder(CommandDefinition command)
    {
        _command = command;
    }

    List<string> ExcludedFields = new();
    public CommandBuilder<TEntity> Exclude<TProperty>(
        Expression<Func<TEntity, TProperty>> selector)
    {
        var name = selector.GetMemberName();

        ExcludedFields.Add(name);

        return this;
    }

    internal void Build()
    {
        foreach (var property in _command.EntityDefinition.Properties)
        {
            if (ExcludedFields.Contains(property.Name))
                continue;
            switch (_command.CommandType)
            {
                case CommandModelType.Create:
                    if (property.IsPrimaryKey)
                        continue;
                    if (property.IsConcurrencyToken)
                        continue;
                    break;
                case CommandModelType.Update:
                    break;
                case CommandModelType.Delete:
                    if (!(property.IsPrimaryKey || property.IsConcurrencyToken))
                        continue;
                    break;
            }
            _command.Properties.Add(property);
        }
    }

    //public CommandBuilder<TEntity> WithRoute(
    //    string route)
    //{
    //    _command.Endpoint.Route = route;

    //    return this;
    //}

    //public CommandBuilder<TEntity> AllowAnonymous()
    //{
    //    _command.Endpoint.AllowAnonymous = true;

    //    return this;
    //}
}
