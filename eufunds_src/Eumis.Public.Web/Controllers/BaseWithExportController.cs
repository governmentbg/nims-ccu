using System.IO;
using System.Text;
using System.Web.Mvc;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Repositories;

namespace Eumis.Public.Web.Controllers
{
    public abstract partial class BaseWithExportController : BaseController
    {
        public BaseWithExportController(
            IMapsRepository mapsRepository,
            IInfrastructureRepository infrastructureRepository)
            : base(mapsRepository, infrastructureRepository)
        {
        }

        public abstract ExportTemplate RenderTemplate();

        [HttpGet]
        public virtual ActionResult Print()
        {
            return this.View(MVC.Shared.Views.Print, this.RenderTemplate());
        }

        [HttpGet]
        public virtual FileResult ExportToExcel()
        {
            FileResult result;

            MemoryStream ms = new MemoryStream();
            try
            {
                var template = this.RenderTemplate();

                template.GenererateExcel().SaveAs(ms);

                ms.Position = 0;
                byte[] content = ms.ToArray();

                result = this.File(
                    content,
                    MimeTypeHelper.MIME_APPLICATION_OPEN_MSEXCEL,
                    template.Name + MimeTypeHelper.GetFileExtenstionByMimeType(MimeTypeHelper.MIME_APPLICATION_OPEN_MSEXCEL));
            }
            finally
            {
                if (ms != null)
                {
                    ms.Dispose();
                }
            }

            return result;
        }

        [HttpGet]
        public virtual FileResult ExportToHtml()
        {
            var template = this.RenderTemplate();
            var html = this.RenderView(MVC.Shared.Views.ExportHtml, template);

            return this.File(
                UTF8Encoding.UTF8.GetBytes(html),
                MimeTypeHelper.MIME_TEXT_HTML,
                template.Name + MimeTypeHelper.GetFileExtenstionByMimeType(MimeTypeHelper.MIME_TEXT_HTML));
        }

        [HttpGet]
        public virtual FileResult ExportToXml()
        {
            string xml;
            var template = this.RenderTemplate();

            using (MemoryStream ms = new MemoryStream())
            {
                using (System.Xml.XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(ms, UTF8Encoding.UTF8))
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(GenericXmlContainer));
                    serializer.Serialize(xmlWriter, template.GenerateXmlContainer());
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
    }
}