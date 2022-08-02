using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportProcedures")]
    public class ContractReportProcedureNomsController : EntityNomsController<Procedure, EntityNomVO>
    {
        public ContractReportProcedureNomsController(
            IContractReportProcedureNomsRepository contractReportProcedureNomsRepository)
            : base(contractReportProcedureNomsRepository)
        {
        }
    }
}
