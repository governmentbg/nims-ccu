using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/debtReimbursedAmountCreationType")]
    public class DebtReimbursedAmountCreationTypeNomsController : EnumNomsController<DebtReimbursedAmountCreationType>
    {
        public DebtReimbursedAmountCreationTypeNomsController(IEnumNomsRepository<DebtReimbursedAmountCreationType> debtReimbursedAmountCreationTypeEnumNomsRepository)
            : base(debtReimbursedAmountCreationTypeEnumNomsRepository)
        {
        }
    }
}
