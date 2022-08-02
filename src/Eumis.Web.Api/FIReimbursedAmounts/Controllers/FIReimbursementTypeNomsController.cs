using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.FIReimbursedAmounts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ReimbursedAmounts.Controllers
{
    [RoutePrefix("api/nomenclatures/fiReimbursementTypes")]
    public class FIReimbursementTypeNomsController : EnumNomsController<FIReimbursementType>
    {
        public FIReimbursementTypeNomsController(IEnumNomsRepository<FIReimbursementType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
