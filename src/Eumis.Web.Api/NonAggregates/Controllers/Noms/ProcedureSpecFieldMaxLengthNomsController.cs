using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/procedureSpecFieldMaxLengths")]
    public class ProcedureSpecFieldMaxLengthNomsController : EnumNomsController<ProcedureSpecFieldMaxLength>
    {
        private IProcedureSpecFieldMaxLengthNomsRepository procedureSpecFieldMaxLengthNomsRepository;

        public ProcedureSpecFieldMaxLengthNomsController(IProcedureSpecFieldMaxLengthNomsRepository procedureSpecFieldMaxLengthNomsRepository)
            : base(procedureSpecFieldMaxLengthNomsRepository)
        {
            this.procedureSpecFieldMaxLengthNomsRepository = procedureSpecFieldMaxLengthNomsRepository;
        }
    }
}
