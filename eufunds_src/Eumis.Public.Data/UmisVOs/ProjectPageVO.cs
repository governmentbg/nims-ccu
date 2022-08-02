using System.Collections.Generic;

namespace Eumis.Public.Data.UmisVOs
{
    public class ProjectPageVO<T> : PageVO<T>
    {
        public ProjectsSummarizedDataVO ProjectsSummarizedData { get; set; }
    }
}
