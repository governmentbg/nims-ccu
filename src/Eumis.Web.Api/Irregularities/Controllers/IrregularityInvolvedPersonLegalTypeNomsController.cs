using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularityInvolvedPersonLegalTypes")]
    public class IrregularityInvolvedPersonLegalTypeNomsController : EnumNomsController<InvolvedPersonLegalType>
    {
        public IrregularityInvolvedPersonLegalTypeNomsController(IEnumNomsRepository<InvolvedPersonLegalType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
