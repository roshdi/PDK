using System.Data;
using System.Data.Common;
using System.Reflection.Emit;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Pdk.Definitions;

public abstract class FeatureProfile<TEntity> : IFeatureProfile
{
    private readonly EntityDefinition _entity;

    protected FeatureProfile()
    {
    }

    public Type GetEntityType()
    {
        return typeof(TEntity);
    }
    public void Initialize(EntityDefinition entity)
    {
        Entity = entity;
    }

    public EntityDefinition Entity { get; private set; }

    public abstract void Configure();

    protected CommandBuilder<TEntity> HasCreateCommand(string? name = null)
    {

        return AddCommandBuilder(CommandModelType.Create, (name ?? "Create") + typeof(TEntity).Name);
    }
    protected CommandBuilder<TEntity> HasUpdateCommand(string? name = null)
    {
        return AddCommandBuilder(CommandModelType.Update, (name ?? "Update") + typeof(TEntity).Name);
    }
    protected CommandBuilder<TEntity> HasDeleteCommand(string? name = null)
    {
        return AddCommandBuilder(CommandModelType.Delete, (name ?? "Delete") + typeof(TEntity).Name);
    }
    

    public void Build()
    {
        foreach (var commandBuilder in commandBuilders)
            commandBuilder.Build();

        foreach (var queryBuilder in queryBuilders)
            queryBuilder.Build();
    }



    protected QueryBuilder<TEntity> HasGetByIdQuery(string? name = null)
    {
        return AddQueryBuilder(QueryModelType.Item,  typeof(TEntity).Name+ (name ?? "GetById"));
    }
    protected QueryBuilder<TEntity>HasGetListQuery(string? name = null)
    {
        return AddQueryBuilder(QueryModelType.Item, typeof(TEntity).Name + (name ?? "GetList"));
    }

    //// Events
    //protected EventBuilder HasEvent(string eventName);

    List<CommandBuilder<TEntity>> commandBuilders = [];
    private CommandBuilder<TEntity> AddCommandBuilder(CommandModelType commandType, string name)
    {
        var cb = new CommandBuilder<TEntity>(this.Entity.AddCommandDefinition(commandType, name));
        commandBuilders.Add(cb);
        return cb;
    }
    List<QueryBuilder<TEntity>> queryBuilders = [];
    private QueryBuilder<TEntity> AddQueryBuilder(QueryModelType queryType, string name)
    {
        var qb = new QueryBuilder<TEntity>(this.Entity.AddQueryDefinition(queryType, name));
        queryBuilders.Add(qb);
        return qb;
    }
}


