using System.Linq.Expressions;

namespace Pdk.Definitions.Extensions;

public static class ExpressionExtensions
{
    public static string GetMemberName<T, TProperty>(
        this Expression<Func<T, TProperty>> expression)
    {
        return expression.Body switch
        {
            MemberExpression member =>
                member.Member.Name,

            UnaryExpression
            {
                Operand: MemberExpression member
            } =>
                member.Member.Name,

            _ => throw new InvalidOperationException(
                $"Expression '{expression}' does not reference a property.")
        };
    }
}