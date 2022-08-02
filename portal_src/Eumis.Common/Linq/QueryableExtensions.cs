﻿using System.Linq;

namespace Eumis.Common.Linq
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
    }
}