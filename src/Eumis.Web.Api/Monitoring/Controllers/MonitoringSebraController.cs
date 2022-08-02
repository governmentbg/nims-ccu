using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;
using Eumis.ApplicationServices.Services.Sebra;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Sebra;

namespace Eumis.Web.Api.Monitoring.Controllers
{
    [RoutePrefix("api/monitoringReports/sebra")]
    public class MonitoringSebraController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private ISebraService sebraService;
        private IAuthorizer authorizer;

        public MonitoringSebraController(
            IAccessContext accessContext,
            IUnitOfWork unitOfWork,
            IProceduresRepository proceduresRepository,
            ISebraService sebraService,
            IAuthorizer authorizer)
        {
            this.accessContext = accessContext;
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.sebraService = sebraService;
            this.authorizer = authorizer;
        }

        [Route("export")]
        public HttpResponseMessage GetSebraReport(
            int programmeId,
            int procedureId,
            DateTime fromDate,
            DateTime toDate,
            int fromNumber,
            int toNumber,
            string sendername,
            string acc,
            string o1,
            SebraPaymentType? type = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var sebraResult = this.sebraService.GetSebraReport(programmeId, procedureId, fromDate, toDate, fromNumber, toNumber, sendername, acc, o1, type);
            var procedure = this.proceduresRepository.Find(procedureId);

            XmlSerializer serializer = new XmlSerializer(typeof(f));
            StringWriterWithEncoding sww = new StringWriterWithEncoding(Encoding.GetEncoding(1251));
            string serializedResult = null;
            using (XmlWriter writer = XmlWriter.Create(sww, new XmlWriterSettings { Indent = true }))
            {
                serializer.Serialize(writer, sebraResult);
                serializedResult = sww.ToString();
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(serializedResult, Encoding.GetEncoding(1251)),
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = string.Format("{0}_{1}_{2}.xml", procedure.Code, fromDate.ToString("dd-MM-yyyy"), toDate.ToString("dd-MM-yyyy")),
                };

            return result;
        }

        [Route("exporByFile")]
        public HttpResponseMessage GetSebraReportByFile(
            Guid fileKey,
            string sendername,
            string acc,
            string o1,
            SebraPaymentType? type = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            var sebraResult = this.sebraService.GetSebraReportByFile(fileKey, sendername, acc, o1, type);

            XmlSerializer serializer = new XmlSerializer(typeof(f));
            StringWriterWithEncoding sww = new StringWriterWithEncoding(Encoding.GetEncoding(1251));
            string serializedResult = null;
            using (XmlWriter writer = XmlWriter.Create(sww, new XmlWriterSettings { Indent = true }))
            {
                serializer.Serialize(writer, sebraResult.xml);
                serializedResult = sww.ToString();
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(serializedResult, Encoding.GetEncoding(1251)),
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = string.Format("{0}_{1}.xml", sebraResult.procedureCode, DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")),
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
