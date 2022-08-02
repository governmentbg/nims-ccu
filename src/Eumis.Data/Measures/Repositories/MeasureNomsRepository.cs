using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Measures;

namespace Eumis.Data.Measures.Repositories
{
    internal class MeasureNomsRepository : EntityNomsRepository<Measure, EntityNomVO>
    {
        public MeasureNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                q => q.MeasureId,
                q => q.Name,
                q => new EntityNomVO
                {
                    NomValueId = q.MeasureId,
                    Name = q.Name,
                })
        {
        }
    }
}