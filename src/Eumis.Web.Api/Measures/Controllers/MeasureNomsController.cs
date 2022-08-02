using Eumis.Data;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Measures.Repositories;
using Eumis.Data.Measures.ViewObjects;
using Eumis.Domain.Measures;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Measures.DataObjects;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace Eumis.Web.Api.Measures.Controllers
{
    [RoutePrefix("api/nomenclatures/measures")]
    public class MeasureNomsController : EntityNomsController<Measure, EntityNomVO>
    {
        public MeasureNomsController(IEntityNomsRepository<Measure, EntityNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
