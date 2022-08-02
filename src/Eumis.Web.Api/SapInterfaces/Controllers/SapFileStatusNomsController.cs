using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.SapInterfaces;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.SapInterfaces.Controllers
{
    [RoutePrefix("api/nomenclatures/sapFileStatuses")]
    public class SapFileStatusNomsController : EnumNomsController<SapFileStatus>
    {
        public SapFileStatusNomsController(IEnumNomsRepository<SapFileStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
