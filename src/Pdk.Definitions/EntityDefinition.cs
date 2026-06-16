using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pdk.Definitions;

public class EntityDefinition
{
    public EntityDefinition(Type entityType)
    {
        EntityType = entityType;
        Name = entityType.Name;
    }

    public Type EntityType { get; }

    public string Name { get; }

    public string? TableName { get; set; }

    public string? Schema { get; set; }

    public bool IsAggregateRoot { get; set; }

    public List<PropertyDefinition> Properties { get; } = [];

    public List<KeyDefinition> Keys { get; } = [];

    public List<ForeignKeyDefinition> ForeignKeys { get; } = [];

    public List<NavigationDefinition> Navigations { get; } = [];

    public List<EntityCapabilityDefinition> Capabilities { get; } = [];

    public List<CommandDefinition> Commands { get; } = [];

    public List<QueryDefinition> Queries { get; } = [];

    public static EntityDefinition Initialize(
        IEntityType entityType)
    {
        var definition = new EntityDefinition(entityType.ClrType);

        definition.LoadEntity(entityType);
        definition.LoadProperties(entityType);
        definition.LoadKeys(entityType);
        definition.LoadForeignKeys(entityType);
        definition.LoadNavigations(entityType);
        definition.LoadCapabilities(entityType.ClrType);

        return definition;
    }

    public override string ToString()
    {
        return "EntityDefinition : " + Name;
    }
    private void LoadEntity(IEntityType entityType)
    {
        TableName = entityType.GetTableName();

        Schema = entityType.GetSchema();
    }
    private void LoadProperties(IEntityType entityType)
    {
        foreach (var property in entityType.GetProperties())
        {
            Properties.Add(
                new PropertyDefinition
                {
                    Name = property.Name,

                    PropertyType =
                        property.ClrType,

                    Nullable =
                        property.IsNullable,

                    IsPrimaryKey =
                        property.IsPrimaryKey(),

                    IsForeignKey =
                        property.IsForeignKey(),

                    IsConcurrencyToken =
                        property.IsConcurrencyToken,

                    IsShadowProperty =
                        property.IsShadowProperty(),

                    IsRequired =
                        !property.IsNullable,

                    MaxLength =
                        property.GetMaxLength(),

                    Precision =
                        property.GetPrecision(),

                    Scale =
                        property.GetScale(),

                    ColumnName =
                        property.GetColumnName(),

                    ColumnType =
                        property.GetColumnType()
                });
        }
    }
    private void LoadKeys(IEntityType entityType)
    {
        foreach (var key in entityType.GetKeys())
        {
            var definition =
                new KeyDefinition(key.IsPrimaryKey());

            foreach (var property in key.Properties)
            {
                definition.Properties.Add(
                    property.Name);
            }

            Keys.Add(definition);
        }
    }
    private void LoadForeignKeys(IEntityType entityType)
    {
        foreach (var fk in entityType.GetForeignKeys())
        {
            var definition =
                new ForeignKeyDefinition
                {
                    Name =
                        fk.GetConstraintName() ??
                        "",

                    PrincipalEntity =
                        fk.PrincipalEntityType
                            .ClrType
                            .Name,

                    NavigationProperty =
                        fk.DependentToPrincipal?.Name,

                    DeleteBehavior =
                        fk.DeleteBehavior
                };

            foreach (var property in fk.Properties)
            {
                definition.Properties.Add(
                    property.Name);
            }

            ForeignKeys.Add(definition);
        }
    }
    private void LoadNavigations(IEntityType entityType)
    {
        foreach (var navigation in entityType.GetNavigations())
        {
            Navigations.Add(
                new NavigationDefinition
                {
                    Name =
                        navigation.Name,

                    TargetEntityType =
                        navigation.TargetEntityType
                            .ClrType,

                    Kind =
                        navigation.IsCollection
                            ? NavigationKind.Collection
                            : NavigationKind.Reference
                });
        }
    }
    private void LoadCapabilities(
    Type entityType)
    {
        //if (typeof(ICreatedOnEntity)
        //    .IsAssignableFrom(entityType))
        //{
        //    Capabilities.Add(
        //        new EntityCapabilityDefinition(
        //            "CreatedOn"));
        //}

        //if (typeof(IModifiedOnEntity)
        //    .IsAssignableFrom(entityType))
        //{
        //    Capabilities.Add(
        //        new EntityCapabilityDefinition(
        //            "ModifiedOn"));
        //}

        //if (typeof(ISoftDeleteEntity)
        //    .IsAssignableFrom(entityType))
        //{
        //    Capabilities.Add(
        //        new EntityCapabilityDefinition(
        //            "SoftDelete"));
        //}

        //if (typeof(ITenantEntity)
        //    .IsAssignableFrom(entityType))
        //{
        //    Capabilities.Add(
        //        new EntityCapabilityDefinition(
        //            "Tenant"));
        //}
    }

    public CommandDefinition AddCommandDefinition(CommandModelType commandType, string name)
    {
        var commandDefinition = new CommandDefinition(this, commandType, name);
        
        this.Commands.Add(commandDefinition);
        return commandDefinition;
    }

    public QueryDefinition AddQueryDefinition(QueryModelType queryType, string name)
    {
        var queryDefinition = new QueryDefinition(this, queryType, name);

        this.Queries.Add(queryDefinition);
        return queryDefinition;
    }
}
