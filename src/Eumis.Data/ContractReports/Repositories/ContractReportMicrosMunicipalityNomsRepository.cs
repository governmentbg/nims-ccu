using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.ViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.Data.ContractReports.Repositories
{
    internal class ContractReportMicrosMunicipalityNomsRepository : EntityNomsRepository<ContractReportMicrosMunicipality, ContractReportMicrosMunicipalityNomVO>, IContractReportMicrosMunicipalityNomsRepository
    {
        public ContractReportMicrosMunicipalityNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.ContractReportMicrosMunicipalityId,
                t => t.Name,
                t => new ContractReportMicrosMunicipalityNomVO
                {
                    NomValueId = t.ContractReportMicrosMunicipalityId,
                    DistrictId = t.ContractReportMicrosDistrictId,
                    Name = t.Name,
                })
        {
        }

        public IList<ContractReportMicrosMunicipalityNomVO> GetAllMunicipalityNoms()
        {
            return (from m in this.unitOfWork.DbContext.Set<ContractReportMicrosMunicipality>()
                    select new ContractReportMicrosMunicipalityNomVO
                    {
                        NomValueId = m.ContractReportMicrosMunicipalityId,
                        DistrictId = m.ContractReportMicrosDistrictId,
                        Name = m.Name,
                    }).ToList();
        }
    }
}
