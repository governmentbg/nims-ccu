using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.FinancialExecution;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/financialExecutionTables")]
    public class FinancialExecutionTableNomsController : EnumNomsController<FinancialExecutionTables>
    {
        public FinancialExecutionTableNomsController(IEnumNomsRepository<FinancialExecutionTables> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
