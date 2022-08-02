using Eumis.Data.ContractReportCertAuthorityFinancialCorrections.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportCertAuthorityFinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportCertAuthorityFinancialCorrectionProcedures")]
    public class ContractReportCertAuthorityFinancialCorrectionProcedureNomsController : EntityNomsController<Procedure, EntityNomVO>
    {
        public ContractReportCertAuthorityFinancialCorrectionProcedureNomsController(
            IContractReportCertAuthorityFinancialCorrectionProcedureNomsRepository contractReportProcedureNomsRepository)
            : base(contractReportProcedureNomsRepository)
        {
        }
    }
}
