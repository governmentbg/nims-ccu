using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;

namespace Eumis.Data.ContractReports.Repositories
{
    internal class ContractReportPaymentNomsRepository : Repository, IContractReportPaymentNomsRepository
    {
        public ContractReportPaymentNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            return (from crp in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId
                    where crp.ContractReportPaymentId == nomValueId &&
                        crp.Status == ContractReportPaymentStatus.Actual &&
                        cr.Status == ContractReportStatus.Accepted
                    select new EntityNomVO
                    {
                        NomValueId = crp.ContractReportPaymentId,
                        Name = "ИП " + crp.VersionNum,
                    })
                    .SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<EntityNomVO> GetNoms(int contractId, int[] ids, string term, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<ContractReportPayment>()
                .AndStringContains(crp => "ИП " + crp.VersionNum, term);

            if (ids.Length != 0)
            {
                predicate = predicate.And(t => ids.Contains(t.ContractReportPaymentId));
            }

            return (from crp in this.unitOfWork.DbContext.Set<ContractReportPayment>().Where(predicate)
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId
                    where cr.ContractId == contractId &&
                        crp.Status == ContractReportPaymentStatus.Actual &&
                        cr.Status == ContractReportStatus.Accepted
                    select new EntityNomVO
                    {
                        NomValueId = crp.ContractReportPaymentId,
                        Name = "ИП " + crp.VersionNum,
                    })
                    .OrderBy(p => p.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}
