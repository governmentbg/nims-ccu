using Eumis.Common.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Linq
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WithOffsetAndLimit<T>(this IQueryable<T> query, int offset, int? limit)
        {
            if (offset > 0)
            {
                query = query.Skip(offset);
            }

            if (limit.HasValue)
            {
                query = query.Take(limit.Value);
            }

            return query;
        }

        public static IQueryable<T> Include<T>(this IQueryable<T> query, Expression<Func<T, object>>[] includes)
        {
            var includeQuery = query;

            foreach (var include in includes)
            {
                includeQuery = includeQuery.Include(include);
            }

            return includeQuery;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string property, SortOrder sortOrder)
        {
            return (IQueryable<T>)OrderBy((IQueryable)source, property, sortOrder);
        }

        public static IQueryable OrderBy(this IQueryable source, string property, SortOrder sortOrder)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            var method = sortOrder != SortOrder.Descending ? "OrderBy" : "OrderByDescending";

            var parameter = Expression.Parameter(source.ElementType);
            var queryExpr = source.Expression;
            var selector = Expression.PropertyOrField(parameter, property);
            var selectorExpr = Expression.Lambda(selector, parameter);

            queryExpr = Expression.Call(
                typeof(Queryable),
                method,
                new Type[]
                {
                    source.ElementType,
                    selector.Type,
                },
                queryExpr,
                Expression.Quote(selectorExpr));

            return source.Provider.CreateQuery(queryExpr);
        }
    }
}
