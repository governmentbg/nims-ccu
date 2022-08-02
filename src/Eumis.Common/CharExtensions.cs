using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Common
{
    public static class CharExtensions
    {
        public static Func<char, bool> NonASCIICharacterPredicate { get; } = (c) => (int)c > 127;
    }
}
