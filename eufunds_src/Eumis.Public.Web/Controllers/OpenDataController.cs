using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Web.Models.OpenData;

namespace Eumis.Public.Web.Controllers
{
    public partial class OpenDataController : BaseController
    {
        private IUmisRepository umisRepository;

        public OpenDataController(
            IMapsRepository mapsRepository,
            IInfrastructureRepository infrastructureRepository,
            IUmisRepository umisRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.umisRepository = umisRepository;
        }

        [HttpGet]
        public virtual ActionResult Index(string programmeId = "")
        {
            this.ModelState.Clear();
            OpenDataModel view = new OpenDataModel()
            {
                ProgrammeId = programmeId,
                Programs = this.InfrastructureRepository
                .GetAllOps()
                .OrderBy(e => e.PortalOrderNum)
                .Select(e => new SelectListItem() { Value = e.MapNodeId.ToString(), Text = e.TransName }),
            };
            return this.View(view);
        }

        [HttpPost]
        public virtual ActionResult Index(OpenDataModel em)
        {
            if (em == null)
            {
                throw new ArgumentNullException(nameof(em));
            }

            return this.ExportToXml(int.Parse(em.ProgrammeId));
        }

        public virtual FileResult ExportToXml(int programmeId)
        {
            string xml;
            var template = this.RenderTemplate(programmeId);

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (System.Xml.XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(ms, UTF8Encoding.UTF8))
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(OpenDataXmlContainer));
                    serializer.Serialize(xmlWriter, template.GenerateOpenDataXmlContainer());
                }

                byte[] bytes = ms.ToArray();

                using (System.IO.MemoryStream bms = new System.IO.MemoryStream(bytes))
                using (System.IO.StreamReader sr = new System.IO.StreamReader(bms, UTF8Encoding.UTF8))
                {
                    xml = sr.ReadToEnd();
                }
            }

            return this.File(
                UTF8Encoding.UTF8.GetBytes(xml),
                MimeTypeHelper.MIME_APPLICATION_XML,
                template.Name + MimeTypeHelper.GetFileExtenstionByMimeType(MimeTypeHelper.MIME_APPLICATION_XML));
        }

        public ExportOpenDataTemplate RenderTemplate(int programmeId)
        {
            var template = new ExportOpenDataTemplate("openData");

            var result = this.umisRepository.GetOpenDataResult(programmeId);

            template.Projects = result.Projects;
            template.Contracts = result.Contracts;
            template.Entities = result.Entities;

            return template;
        }
    }
}
