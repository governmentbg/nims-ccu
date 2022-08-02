using System.Web.Http;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/nomenclatures/contractProcedures")]
    public class ContractProcedureNomsController : EntityNomsController<Procedure, EntityNomVO>
    {
        public ContractProcedureNomsController(
            IContractProcedureNomsRepository contractProcedureNomsRepository)
            : base(contractProcedureNomsRepository)
        {
        }
    }
}
