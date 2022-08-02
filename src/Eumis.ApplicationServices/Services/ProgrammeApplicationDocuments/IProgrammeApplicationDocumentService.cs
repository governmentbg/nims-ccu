using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ProgrammeApplicationDocuments
{
    public interface IProgrammeApplicationDocumentService
    {
        void LoadProgrammeApplicationDocuments(int programmeId, Guid blobKey);

        IList<string> CanLoadProgrammeApplicationDocuments(int programmeId, Guid blobKey);

        IList<string> CanAddProgrammeApplicationDocuments(int programmeId, string name);

        IList<string> CanDeleteProgrammeApplicationDocument(int programmeApplicationDocumentId);
    }
}
