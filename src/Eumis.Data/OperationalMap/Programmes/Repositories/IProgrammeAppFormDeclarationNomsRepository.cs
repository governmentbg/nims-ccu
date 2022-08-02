using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.OperationalMap.ProgrammeDeclarations;
using System.Collections.Generic;

namespace Eumis.Data.OperationalMap.Programmes.Repositories
{
    public interface IProgrammeAppFormDeclarationNomsRepository : IEntityNomsRepository<ProgrammeAppFormDeclaration, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetDeclarations(int programmeId, int procedureId, string term, int offset = 0, int? limit = null);

        IEnumerable<EntityNomVO> GetNSIProgrammeDeclarations(int procedureId, string term, int offset = 0, int? limit = null);

        IEnumerable<EntityNomVO> GetNSIProgrammeDeclarationsFromProjectVersionXML(int projectVersionXmlId, string term, int offset = 0, int? limit = null);
    }
}
