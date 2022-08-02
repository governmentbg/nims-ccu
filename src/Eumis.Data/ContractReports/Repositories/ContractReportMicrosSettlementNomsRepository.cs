using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.ViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.Data.ContractReports.Repositories
{
    internal class ContractReportMicrosSettlementNomsRepository : EntityNomsRepository<ContractReportMicrosSettlement, ContractReportMicrosSettlementNomVO>, IContractReportMicrosSettlementNomsRepository
    {
        public ContractReportMicrosSettlementNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.ContractReportMicrosSettlementId,
                t => t.Name,
                t => new ContractReportMicrosSettlementNomVO
                {
                    NomValueId = t.ContractReportMicrosSettlementId,
                    MunicipalityId = t.ContractReportMicrosMunicipalityId,
                    Name = t.Name,
                })
        {
        }

        public IList<ContractReportMicrosSettlementNomVO> GetAllSettlementNoms()
        {
            return (from s in this.unitOfWork.DbContext.Set<ContractReportMicrosSettlement>()
                    join m in this.unitOfWork.DbContext.Set<ContractReportMicrosMunicipality>() on s.ContractReportMicrosMunicipalityId equals m.ContractReportMicrosMunicipalityId
                    select new ContractReportMicrosSettlementNomVO
                    {
                        NomValueId = s.ContractReportMicrosSettlementId,
                        DistrictId = m.ContractReportMicrosDistrictId,
                        MunicipalityId = s.ContractReportMicrosMunicipalityId,
                        Name = s.Name,
                    }).ToList();
        }
    }
}
