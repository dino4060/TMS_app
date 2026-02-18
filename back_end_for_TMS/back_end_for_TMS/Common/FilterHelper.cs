using System.Linq.Expressions;

namespace back_end_for_TMS.Common;

public static class FilterHelper
{
    public static IQueryable<T> WhereHasValue<T>(this IQueryable<T> query, string? value, Expression<Func<T, bool>> predicate)
    {
        return string.IsNullOrWhiteSpace(value) ? query : query.Where(predicate);
    }

    public static IQueryable<T> WhereHasValue<T>(this IQueryable<T> query, double? value, Expression<Func<T, bool>> predicate)
    {
        return !value.HasValue ? query : query.Where(predicate);
    }
}
