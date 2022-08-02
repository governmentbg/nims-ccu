using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/procedureEvalTableTypes")]
    public class ProcedureEvalTableTypeNomsController : EnumNomsController<ProcedureEvalTableType>
    {
        private IProcedureEvalTableTypeEnumNomsRepository procedureEvalTableTypeEnumNomsRepository;

        public ProcedureEvalTableTypeNomsController(IProcedureEvalTableTypeEnumNomsRepository procedureEvalTableTypeEnumNomsRepository)
            : base(procedureEvalTableTypeEnumNomsRepository)
        {
            this.procedureEvalTableTypeEnumNomsRepository = procedureEvalTableTypeEnumNomsRepository;
        }

        [Route("")]
        public IList<EnumNomVO<ProcedureEvalTableType>> GetEvalSessionSheetTypes(int evalSessionId, string term = null)
        {
            return this.procedureEvalTableTypeEnumNomsRepository.GetEvalSessionSheetTypes(evalSessionId, term);
        }
    }
}
