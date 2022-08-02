using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.Monitorstat;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Messages.Repositories;
using Eumis.Data.Messages.ViewObjects;
using Eumis.Data.Monitorstat.Repositories;
using Eumis.Data.Monitorstat.ViewObjects;
using Eumis.Domain.Messages;
using Eumis.Domain.Monitorstat;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Messages.DataObjects;

namespace Eumis.Web.Api.Monitorstat.Controllers
{
    [RoutePrefix("api/monitorstat")]
    public class MonitorstatSurveysController : ApiController
    {
        private IAuthorizer authorizer;
        private IMonitorstatSurveysRepository monitorstatSurveyRepository;
        private IMonitorstatService monitorstatService;

        public MonitorstatSurveysController(
            IMonitorstatSurveysRepository monitorstatSurveyRepository,
            IMonitorstatService monitorstatService,
            IAuthorizer authorizer)
        {
            this.monitorstatSurveyRepository = monitorstatSurveyRepository;
            this.authorizer = authorizer;
            this.monitorstatService = monitorstatService;
        }

        [Route("")]
        public IList<MonitorstatSurveyVO> GetSurveys()
        {
            return this.monitorstatSurveyRepository.GetSurveys();
        }

        [HttpGet]
        [Route("{id:int}")]
        public MonitorstatSurveyVO GetSurvey(int id)
        {
            return this.monitorstatSurveyRepository.GetSurvey(id);
        }

        [HttpPost]
        [Route("{year}/canLoadSurveys")]
        [Transaction]
        public ErrorsDO CanLoadSurveys(MonitorstatYear year)
        {
            this.authorizer.AssertCanDo(MonitorstatListActions.Import);

            return new ErrorsDO(this.monitorstatService.CanLoadExternalSurveys(year));
        }

        [HttpPost]
        [Route("{year}/loadSurveys")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.MonitorstatSurvey.Import))]
        public void LoadSurveys(MonitorstatYear year)
        {
            this.authorizer.AssertCanDo(MonitorstatListActions.Import);

            this.monitorstatService.LoadExternalSurveys(year);
        }
    }
}
