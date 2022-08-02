using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.Irregularity
{
    public interface IIrregularityVersionService
    {
        bool CanEditVersion(int versionId);

        void ActivateVersion(int versionId, byte[] version);

        IList<string> CanCreateVersion(int irregularityId);
    }
}
