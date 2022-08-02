using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.OperationalMap.ProgrammeDeclarations;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/fieldTypes")]
    public class FieldTypeNomsController : EnumNomsController<FieldType>
    {
        public FieldTypeNomsController(IEnumNomsRepository<FieldType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
