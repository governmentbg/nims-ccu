using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;

namespace Eumis.Data.FinancialCorrections.Repositories
{
    internal class FinancialCorrectionNomsRepository : Repository, IFinancialCorrectionNomsRepository
    {
        public FinancialCorrectionNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            return (from fc in this.unitOfWork.DbContext.Set<FinancialCorrection>()
                    where fc.FinancialCorrectionId == nomValueId
                    select new EntityNomVO
                    {
                        NomValueId = fc.FinancialCorrectionId,
                        Name = "Финансова корекция " + fc.OrderNum + (fc.Status == FinancialCorrectionStatus.Removed ? "(Анулирана)" : string.Empty),
                    })
                    .SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<EntityNomVO> GetNoms(int contractId, string term, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<FinancialCorrection>()
                .AndStringContains(fc => "Финансова корекция " + fc.OrderNum, term);

            return (from fc in this.unitOfWork.DbContext.Set<FinancialCorrection>().Where(predicate)
                    where fc.ContractId == contractId && fc.Status == FinancialCorrectionStatus.Entered
                    select new EntityNomVO
                    {
                        NomValueId = fc.FinancialCorrectionId,
                        Name = "Финансова корекция " + fc.OrderNum,
                    })
                    .OrderBy(p => p.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}
