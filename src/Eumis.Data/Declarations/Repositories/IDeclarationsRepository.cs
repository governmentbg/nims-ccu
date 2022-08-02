using System;
using System.Collections.Generic;
using Eumis.Data.Declarations.ViewObjects;
using Eumis.Domain;

namespace Eumis.Data.Declarations.Repositories
{
    public interface IDeclarationsRepository : IAggregateRepository<Declaration>
    {
        IList<DeclarationVO> GetDeclarations(DateTime? activationDate = null, DeclarationStatus? status = null);

        bool IsDeclarationFileAccessible(int declarationId, Guid fileKey);
    }
}
