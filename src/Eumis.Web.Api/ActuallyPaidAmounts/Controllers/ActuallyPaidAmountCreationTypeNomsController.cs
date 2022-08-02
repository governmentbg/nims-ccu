using Eumis.Data.ActuallyPaidAmounts.ViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/actuallyPaidAmountCreationType")]
    public class ActuallyPaidAmountCreationTypeNomsController : EnumNomsController<ActuallyPaidAmountCreationType>
    {
        public ActuallyPaidAmountCreationTypeNomsController(IEnumNomsRepository<ActuallyPaidAmountCreationType> actuallyPaidAmountCreationTypeEnumNomsRepository)
            : base(actuallyPaidAmountCreationTypeEnumNomsRepository)
        {
        }
    }
}
