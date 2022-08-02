using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ProgrammeDeclaration
{
    public interface IProgrammeDeclarationService
    {
        IList<string> LoadProgrammeDeclarationItems(int programmeDeclarationId, Guid blobKey);
    }
}
