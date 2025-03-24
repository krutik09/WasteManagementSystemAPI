using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using System.Reflection;

namespace WasteManagementSystem.Data.Builders;

public static class ExpressionBuilder<T>
{
    public static Expression<Func<T, bool>> Build(string propertyName, object value, string comparisonType = "Equals")
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var constant = Expression.Constant(value);

        Expression comparison = comparisonType switch
        {
            "Equals" => Expression.Equal(property, constant),
            "NotEquals" => Expression.NotEqual(property, constant),
            "GreaterThan" => Expression.GreaterThan(property, constant),
            "GreaterThanOrEqual" => Expression.GreaterThanOrEqual(property, constant),
            "LessThan" => Expression.LessThan(property, constant),
            "LessThanOrEqual" => Expression.LessThanOrEqual(property, constant),
            _ => throw new ArgumentException("Invalid comparison type")
        };

        return Expression.Lambda<Func<T, bool>>(comparison, parameter);
    }
}

