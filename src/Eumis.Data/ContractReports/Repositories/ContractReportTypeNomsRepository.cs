using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReports.Repositories
{
    internal class ContractReportTypeNomsRepository : Repository, IContractReportTypeNomsRepository
    {
        public ContractReportTypeNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EnumNomVO<ContractReportType> GetNom(ContractReportType e)
        {
            return new EnumNomVO<ContractReportType>(e);
        }

        public IList<EnumNomVO<ContractReportType>> GetNoms(int contractId, string term, int offset = 0, int? limit = null)
        {
            var availableContractReportTypes = new List<ContractReportType>() { ContractReportType.AdvancePayment, ContractReportType.PaymentFinancial, ContractReportType.Financial };

            var procedureApplicationSections = (from c in this.unitOfWork.DbContext.Set<Contract>().Where(x => x.ContractId == contractId)
                                                join pac in this.unitOfWork.DbContext.Set<ProcedureApplicationSection>() on c.ProcedureId equals pac.ProcedureId
                                                where pac.IsSelected
                                                select pac.Section)
                                                .ToList();

            if (procedureApplicationSections.Contains(ApplicationSectionType.Team) || procedureApplicationSections.Contains(ApplicationSectionType.Indicators) || procedureApplicationSections.Contains(ApplicationSectionType.Activities))
            {
                availableContractReportTypes.Add(ContractReportType.Technical);
                availableContractReportTypes.Add(ContractReportType.PaymentTechnicalFinancial);
            }

            return availableContractReportTypes
                .Select(e => new EnumNomVO<ContractReportType>(e))
                .OrderBy(e => e.OrderNum)
                .ToList();
        }

        public IList<EnumNomVO<ContractReportType>> GetNoms(string term)
        {
            return Enum.GetValues(typeof(ContractReportType))
               .Cast<ContractReportType>()
               .Select(e => new EnumNomVO<ContractReportType>(e))
               .OrderBy(e => e.OrderNum)
               .ToList();
        }
    }
}
