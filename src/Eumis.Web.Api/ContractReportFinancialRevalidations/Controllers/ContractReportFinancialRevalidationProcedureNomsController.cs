using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialRevalidations.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialRevalidationProcedures")]
    public class ContractReportFinancialRevalidationProcedureNomsController : EntityNomsController<Procedure, EntityNomVO>
    {
        public ContractReportFinancialRevalidationProcedureNomsController(
            IContractReportProcedureNomsRepository contractReportProcedureNomsRepository)
            : base(contractReportProcedureNomsRepository)
        {
        }
    }
}
