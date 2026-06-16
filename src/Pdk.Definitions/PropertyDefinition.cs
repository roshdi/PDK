namespace Pdk.Definitions;

public sealed class PropertyDefinition
{
    public string Name { get; set; } = "";

    public Type PropertyType { get; set; } = typeof(object);

    public bool Nullable { get; set; }

    public bool IsPrimaryKey { get; set; }

    public bool IsForeignKey { get; set; }

    public bool IsConcurrencyToken { get; set; }

    public bool IsShadowProperty { get; set; }

    public bool IsComputed { get; set; }

    public bool IsRequired { get; set; }

    public int? MaxLength { get; set; }

    public int? Precision { get; set; }

    public int? Scale { get; set; }

    public object? DefaultValue { get; set; }

    public string? ColumnName { get; set; }

    public string? ColumnType { get; set; }
}
