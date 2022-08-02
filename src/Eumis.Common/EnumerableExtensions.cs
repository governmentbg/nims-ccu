using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Common
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] sequences)
        {
            return sequences.SelectMany(x => x);
        }
    }
}
