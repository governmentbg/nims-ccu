using Autofac.Extras.NLog;
using Eumis.Integration.Monitorstat.Communicators;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Integration.Monitorstat.Api
{
    [Authorize]
    [RoutePrefix("monitorstat/surveys")]
    public class SurveyController : ApiController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private IMonitorstatCommunicator client;

        public SurveyController(IMonitorstatCommunicator client)
        {
            Logger.Info($"Ctor {nameof(SurveyController)}");
            this.client = client;
        }

        [Route("")]
        public IDictionary<string, string> GetSurveys()
        {
            return this.client.GetSurveys(DateTime.Now.Year);
        }

        [Route("{year:int}")]
        public IDictionary<string, string> GetSurveys(int year)
        {
            Logger.Info(nameof(this.GetSurveys));
            return this.client.GetSurveys(year);
        }

        [Route("reports")]
        public IDictionary<string, string> GetReports(int year, string surveyCode)
        {
            return this.client.GetReports(year, surveyCode);
        }
    }
}
