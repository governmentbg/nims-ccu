using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.SapInterfaces;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.SapInterfaces.Controllers
{
    [RoutePrefix("api/nomenclatures/sapFileTypes")]
    public class SapFileTypeNomsController : EnumNomsController<SapFileType>
    {
        public SapFileTypeNomsController(IEnumNomsRepository<SapFileType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
