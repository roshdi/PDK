namespace Pdk.Core.Abstractions;

public interface ILogicalDelete
{
    void SetDeleted();
    void SetUndeleted();
}