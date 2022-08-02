using System.Web.Http;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportTechnicalCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportTechnicalCorrectionProcedures")]
    public class ContractReportTechnicalCorrectionProcedureNomsController : EntityNomsController<Procedure, EntityNomVO>
    {
        public ContractReportTechnicalCorrectionProcedureNomsController(
            IContractReportProcedureNomsRepository contractReportProcedureNomsRepository)
            : base(contractReportProcedureNomsRepository)
        {
        }
    }
}
