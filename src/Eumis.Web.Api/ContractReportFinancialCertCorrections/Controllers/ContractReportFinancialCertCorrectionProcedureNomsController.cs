using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialCertCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialCertCorrectionProcedures")]
    public class ContractReportFinancialCertCorrectionProcedureNomsController : EntityNomsController<Procedure, EntityNomVO>
    {
        public ContractReportFinancialCertCorrectionProcedureNomsController(
            IContractReportProcedureNomsRepository contractReportProcedureNomsRepository)
            : base(contractReportProcedureNomsRepository)
        {
        }
    }
}
