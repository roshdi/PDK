namespace Pdk.Definitions;

public sealed class NavigationDefinition
{
    public string Name { get; set; } = "";

    public Type TargetEntityType { get; set; }
        = typeof(object);

    public NavigationKind Kind { get; set; }

    public bool Owned { get; set; }
}
public enum NavigationKind
{
    Reference=1,
    Collection =2
}
