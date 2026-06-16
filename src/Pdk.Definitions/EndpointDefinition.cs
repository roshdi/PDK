namespace Pdk.Definitions;

public sealed class EndpointDefinition
{
    public string? Route { get; set; }

    public bool AllowAnonymous { get; set; }

    public string[] Permissions { get; set; }
        = [];
}