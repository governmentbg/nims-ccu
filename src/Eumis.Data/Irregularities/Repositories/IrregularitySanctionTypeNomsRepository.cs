using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain.Irregularities;

namespace Eumis.Data.Irregularities.Repositories
{
    internal class IrregularitySanctionTypeNomsRepository : EntityCodeNomsRepository<IrregularitySanctionType, IrregularitySanctionTypeVO>, IIrregularitySanctionTypeNomsRepository
    {
        public IrregularitySanctionTypeNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                it => it.IrregularitySanctionTypeId,
                it => it.Name,
                it => it.Code,
                it => new IrregularitySanctionTypeVO
                {
                    NomValueId = it.IrregularitySanctionTypeId,
                    SanctionCategoryId = it.IrregularitySanctionCategoryId,
                    Name = "(" + it.Code + ") " + it.Name,
                    Code = it.Code,
                })
        {
        }

        public IEnumerable<IrregularitySanctionTypeVO> GetSanctionTypeNoms(int sanctionCategoryId, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<IrregularitySanctionTypeVO>()
                .And(it => it.SanctionCategoryId == sanctionCategoryId)
                .AndStringContains(it => it.Name, term);

            return (from it in this.unitOfWork.DbContext.Set<IrregularitySanctionType>()
                    select new IrregularitySanctionTypeVO
                    {
                        NomValueId = it.IrregularitySanctionTypeId,
                        SanctionCategoryId = it.IrregularitySanctionCategoryId,
                        Name = "(" + it.Code + ") " + it.Name,
                        Code = it.Code,
                    })
                    .Where(predicate)
                    .OrderBy(t => t.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}
