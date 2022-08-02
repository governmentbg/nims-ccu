using Eumis.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Linq
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> WithOffsetAndLimit<T>(this IEnumerable<T> query, int offset, int? limit)
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

        public static IEnumerable<T> WithSort<T>(this IEnumerable<T> query, string sortBy, SortOrder? sortOrder)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (sortBy is null)
            {
                return query;
            }

            var order = sortOrder ?? SortOrder.Ascending;

            return query.AsQueryable().OrderBy(sortBy, order);
        }
    }
}
