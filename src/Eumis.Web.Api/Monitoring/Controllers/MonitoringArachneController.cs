using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Arachne.Repositories;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.Arachne;

namespace Eumis.Web.Api.Monitoring.Controllers
{
    [RoutePrefix("api/monitoringReports/arachne")]
    public class MonitoringArachneController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IArachneRepository arachneRepository;
        private IProgrammesRepository programmesRepository;
        private IAuthorizer authorizer;

        public MonitoringArachneController(
            IAccessContext accessContext,
            IUnitOfWork unitOfWork,
            IArachneRepository arachneRepository,
            IProgrammesRepository programmesRepository,
            IAuthorizer authorizer)
        {
            this.accessContext = accessContext;
            this.unitOfWork = unitOfWork;
            this.arachneRepository = arachneRepository;
            this.programmesRepository = programmesRepository;
            this.authorizer = authorizer;
        }

        [Route("export")]
        public HttpResponseMessage GetArachneReport(int programmeId)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var arachneResult = this.arachneRepository.GetArachneReport(programmeId);
            var programme = this.programmesRepository.Find(programmeId);

            XmlSerializer serializer = new XmlSerializer(typeof(ECDataExchangeXmlFormat));
            StringWriterWithEncoding sww = new StringWriterWithEncoding(Encoding.UTF8);
            string serializedResult = null;
            using (XmlWriter writer = XmlWriter.Create(sww, new XmlWriterSettings { Encoding = Encoding.UTF8, Indent = true }))
            {
                serializer.Serialize(writer, arachneResult);
                serializedResult = sww.ToString();
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(serializedResult),
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = string.Format("arachne_{0}.xml", programme.Code),
                };

            return result;
        }

        private sealed class StringWriterWithEncoding : StringWriter
        {
            private readonly Encoding encoding;

            public StringWriterWithEncoding(Encoding encoding)
            {
                this.encoding = encoding;
            }

            public override Encoding Encoding
            {
                get { return this.encoding; }
            }
        }
    }
}
