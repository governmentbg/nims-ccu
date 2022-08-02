using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.FlatFinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/flatFinancialCorrectionTypes")]
    public class FlatFinancialCorrectionTypeNomsController : EnumNomsController<FlatFinancialCorrectionType>
    {
        public FlatFinancialCorrectionTypeNomsController(IEnumNomsRepository<FlatFinancialCorrectionType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
