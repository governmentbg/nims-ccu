using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain.Irregularities;

namespace Eumis.Data.Irregularities.Repositories
{
    internal class IrregularityTypeNomsRepository : EntityCodeNomsRepository<IrregularityType, IrregularityTypeVO>, IIrregularityTypeNomsRepository
    {
        public IrregularityTypeNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                it => it.IrregularityTypeId,
                it => it.Name,
                it => it.Code,
                it => new IrregularityTypeVO
                {
                    NomValueId = it.IrregularityTypeId,
                    CategoryId = it.IrregularityCategoryId,
                    Name = it.Name,
                    Code = it.Code,
                })
        {
        }

        public IEnumerable<IrregularityTypeVO> GetIrregularityTypeNoms(int irregularityCategoryId, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<IrregularityType>()
                .And(it => it.IrregularityCategoryId == irregularityCategoryId)
                .AndStringContains(it => it.Name, term);

            return (from it in this.unitOfWork.DbContext.Set<IrregularityType>().Where(predicate)
                    select new IrregularityTypeVO
                    {
                        NomValueId = it.IrregularityTypeId,
                        CategoryId = it.IrregularityCategoryId,
                        Name = it.Name,
                        Code = it.Code,
                    })
                    .OrderBy(t => t.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}
