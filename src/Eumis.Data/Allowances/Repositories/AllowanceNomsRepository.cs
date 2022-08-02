using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Allowances;

namespace Eumis.Data.Allowances.Repositories
{
    internal class AllowanceNomsRepository : EntityNomsRepository<Allowance, EntityNomVO>
    {
        public AllowanceNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                q => q.AllowanceId,
                q => q.Name,
                q => new EntityNomVO
                {
                    NomValueId = q.AllowanceId,
                    Name = q.Name,
                })
        {
        }
    }
}