using System.Collections.Generic;

namespace Eumis.Public.Data.UmisVOs
{
    public class PageVO<T>
    {
        public IList<T> Results { get; set; }

        public int Count { get; set; }
    }
}
