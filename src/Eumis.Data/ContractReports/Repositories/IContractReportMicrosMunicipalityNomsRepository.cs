using System.Collections.Generic;
using Eumis.Data.ContractReports.ViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.Data.ContractReports.Repositories
{
    public interface IContractReportMicrosMunicipalityNomsRepository : IEntityNomsRepository<ContractReportMicrosMunicipality, ContractReportMicrosMunicipalityNomVO>
    {
        IList<ContractReportMicrosMunicipalityNomVO> GetAllMunicipalityNoms();
    }
}
