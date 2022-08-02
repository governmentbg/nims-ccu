using Eumis.Common.Db;
using Eumis.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.ContractReportFinancialCSDs.Repositories
{
    internal class ContractReportFinancialCSDsRepository : AggregateRepository<ContractReportFinancialCSD>, IContractReportFinancialCSDsRepository
    {
        public ContractReportFinancialCSDsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportFinancialCSD, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportFinancialCSD, object>>[]
                {
                    c => c.Files,
                };
            }
        }

        public IList<ContractReportFinancialCSD> FindAll(int contractReportFinancialId)
        {
            return this.Set().Where(csd => csd.ContractReportFinancialId == contractReportFinancialId).ToList();
        }

        public IList<ContractReportFinancialCSDFile> GetContractReportFinancialCSDFiles(int contractReportFinancialId)
        {
            return this.unitOfWork.DbContext.Set<ContractReportFinancialCSDFile>().Where(csdf => csdf.ContractReportFinancialId == contractReportFinancialId).ToList();
        }

        public IList<int> GetContractReportFinancialCSDsInDraft(int contractReportFinancialId)
        {
            return this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                    .Where(crfbi => crfbi.ContractReportFinancialId == contractReportFinancialId && crfbi.Status == ContractReportFinancialCSDBudgetItemStatus.Draft)
                    .Select(crfbi => crfbi.ContractReportFinancialCSDId)
                    .Distinct()
                    .ToList();
        }
    }
}
