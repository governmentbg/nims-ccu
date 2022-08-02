using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/nomenclatures/procedureKinds")]
    public class ProcedureKindNomsController : EnumNomsController<ProcedureKind>
    {
        public ProcedureKindNomsController(IEnumNomsRepository<ProcedureKind> enumNomsRepository)
            : base(enumNomsRepository)
        {
        }
    }
}
