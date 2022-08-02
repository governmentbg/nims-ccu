using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.ProgrammePriorities.Controllers
{
    [RoutePrefix("api/nomenclatures/programmePriorityTypes")]
    public class ProgrammePriorityTypeNomsController : EnumNomsController<ProgrammePriorityType>
    {
        public ProgrammePriorityTypeNomsController(IEnumNomsRepository<ProgrammePriorityType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
