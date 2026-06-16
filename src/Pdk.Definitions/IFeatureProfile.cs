namespace Pdk.Definitions;

public interface IFeatureProfile
{
    Type GetEntityType();
    void Initialize(EntityDefinition entity);
    void Configure();
    void Build();
}
