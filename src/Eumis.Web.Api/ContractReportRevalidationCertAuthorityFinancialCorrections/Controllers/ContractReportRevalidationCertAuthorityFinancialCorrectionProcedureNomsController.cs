using Eumis.Data.ContractReportRevalidationCertAuthorityFinancialCorrections.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportRevalidationCertAuthorityFinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportRevalidationCertAuthorityFinancialCorrectionProcedures")]
    public class ContractReportRevalidationCertAuthorityFinancialCorrectionProcedureNomsController : EntityNomsController<Procedure, EntityNomVO>
    {
        public ContractReportRevalidationCertAuthorityFinancialCorrectionProcedureNomsController(
            IContractReportRevalidationCertAuthorityFinancialCorrectionProcedureNomsRepository contractReportProcedureNomsRepository)
            : base(contractReportProcedureNomsRepository)
        {
        }
    }
}
