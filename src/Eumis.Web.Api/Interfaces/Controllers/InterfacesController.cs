using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Monitoring.Repositories;
using Eumis.Data.Monitoring.ViewObjects;
using Eumis.Domain.NonAggregates;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace Eumis.Web.Api.Interfaces.Controllers
{
    [RoutePrefix("api/interfaces")]
    public class InterfacesController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public InterfacesController(
            IAccessContext accessContext,
            IUnitOfWork unitOfWork,
            IMonitoringReportsRepository monitoringReportsRepository,
            IAuthorizer authorizer)
        {
            this.accessContext = accessContext;
            this.unitOfWork = unitOfWork;
            this.monitoringReportsRepository = monitoringReportsRepository;
            this.authorizer = authorizer;
        }

        [HttpGet]
        [Route("export")]
        public HttpResponseMessage InterfacesExport(int contractId, InformationSystem informationSystem)
        {
            this.authorizer.AssertCanDo(InterfacesActions.Export);

            var results = this.monitoringReportsRepository.GetAnex3Report(contractId);

            XmlSerializer serializer = new XmlSerializer(typeof(Anex3ReportVO));
            StringWriter sww = new StringWriter();
            string serializedResults = null;
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                serializer.Serialize(writer, results);
                serializedResults = sww.ToString();
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(serializedResults);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = string.Format("{0}.xml", Enum.GetName(typeof(InformationSystem), informationSystem).ToLower()),
                };

            return result;
        }
    }
}
