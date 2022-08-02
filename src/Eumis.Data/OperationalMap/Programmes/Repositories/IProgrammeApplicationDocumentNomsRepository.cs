using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using Eumis.Domain.OperationalMap.Programmes;
using System.Collections.Generic;

namespace Eumis.Data.OperationalMap.Programmes.Repositories
{
    public interface IProgrammeApplicationDocumentNomsRepository : IEntityNomsRepository<ProgrammeApplicationDocument, EntityNomVO>
    {
        IEnumerable<ProgrammeApplicationDocumentNomVO> GetProgrammeApplicationDocuments(int procedureId, string term, int offset = 0, int? limit = null);
    }
}
