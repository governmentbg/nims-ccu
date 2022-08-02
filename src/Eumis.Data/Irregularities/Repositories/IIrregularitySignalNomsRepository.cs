using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;

namespace Eumis.Data.Irregularities.Repositories
{
    public interface IIrregularitySignalNomsRepository : IEntityNomsRepository<IrregularitySignal, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetSignalNoms(string term, int offset = 0, int? limit = null, int[] programmeIds = null, IrregularitySignalStatus? status = null, bool freeOnly = false);

        IEnumerable<EntityNomVO> GetRegisterSignalNoms(string term, int offset = 0, int? limit = null, int[] programmeIds = null);
    }
}