using System.Collections.Generic;

namespace Eumis.Data.Core
{
    public class PagePVO<T>
    {
        public IList<T> Results { get; set; }

        public int Count { get; set; }
    }
}
