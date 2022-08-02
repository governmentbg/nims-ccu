using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Monitorstat;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eumis.Web.Api.Monitorstat.Controllers
{
    [RoutePrefix("api/nomenclatures/monitorstatAvailableYears")]
    public class MonitorstatYearNomsController : EnumNomsController<MonitorstatYear>
    {
        public MonitorstatYearNomsController(IEnumNomsRepository<MonitorstatYear> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
