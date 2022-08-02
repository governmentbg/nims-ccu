using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Blob.Host.Auth
{
    public interface IBlobAccessContext
    {
        bool IsUploader { get; }

        bool CanAccess(Guid blobKey);

        void AssertCanAccess(Guid blobKey);
    }
}
