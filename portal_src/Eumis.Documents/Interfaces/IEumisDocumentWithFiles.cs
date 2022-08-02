using System.Collections.Generic;
using R_10018;

namespace Eumis.Documents.Interfaces
{
    public interface IEumisDocumentWithFiles
    {
        IEnumerable<AttachedDocument> Files { get;  }
    }
}
