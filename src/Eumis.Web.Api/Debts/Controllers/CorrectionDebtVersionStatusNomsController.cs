using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Debts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Debts.Controllers
{
    [RoutePrefix("api/nomenclatures/correctionDebtVersionStatuses")]
    public class CorrectionDebtVersionStatusNomsController : EnumNomsController<CorrectionDebtVersionStatus>
    {
        public CorrectionDebtVersionStatusNomsController(IEnumNomsRepository<CorrectionDebtVersionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
