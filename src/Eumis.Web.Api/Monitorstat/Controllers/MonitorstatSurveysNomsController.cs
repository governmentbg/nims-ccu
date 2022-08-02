using Eumis.Common.Auth;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Monitorstat.Repositories;
using Eumis.Data.Notifications.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Monitorstat;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;

namespace Eumis.Web.Api.Monitorstat.Controllers
{
    [RoutePrefix("api/nomenclatures/monitorstatSurveys")]
    public class MonitorstatSurveysNomsController : ApiController
    {
        private IMonitorstatSurveyNomsRepository monitorstatSurveyNomsRepository;

        public MonitorstatSurveysNomsController(IMonitorstatSurveyNomsRepository monitorstatSurveyNomsRepository)
        {
            this.monitorstatSurveyNomsRepository = monitorstatSurveyNomsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.monitorstatSurveyNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(MonitorstatYear? year, string term = null, int offset = 0, int? limit = null)
        {
            return this.monitorstatSurveyNomsRepository.GetNoms(year, term, offset, limit);
        }
    }
}
