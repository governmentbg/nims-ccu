using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Common.Extensions;
using Eumis.Common.Helpers;
using Eumis.Components;
using Eumis.Portal.Web.Helpers;

namespace Eumis.Portal.Web.Helplers
{
    public class IsunFileManager
    {
        public static IsunFile Create(string xml, string hash)
        {
            string fileName = hash;
            
            var _documentSerializer = System.Web.Mvc.DependencyResolver.Current.GetService<IDocumentSerializer>();
            var project = _documentSerializer.XmlDeserializeFromString<R_10019.Project>(xml);

            if (project != null
                && project.ProjectBasicData != null
                && project.ProjectBasicData.Procedure != null)
            {
                fileName = string.Format("{0}-{1}", project.ProjectBasicData.Procedure.Code, fileName);
            }

            return CreateFile(xml, hash, IsunFileType.ProjectFile, fileName);
        }

        public static IsunFile CreateMessageFile(string xml, string hash, string regNumber)
        {
            return CreateFile(xml, hash, IsunFileType.AnswerFile, regNumber);
        }

        public static IsunFile CreateTechnicalReportFile(string xml, string hash)
        {
            var _documentSerializer = System.Web.Mvc.DependencyResolver.Current.GetService<IDocumentSerializer>();
            var technical = _documentSerializer.XmlDeserializeFromString<R_10044.TechnicalReport>(xml);

            string fileName = string.Empty;

            if (technical != null
                && technical.contractNumber != null
                && technical.docNumber != null
                && technical.docSubNumber != null)
            {
                fileName = string.Format("{0}_{1}.{2}", technical.contractNumber, technical.docNumber, technical.docSubNumber);
            }

            return CreateFile(xml, hash, IsunFileType.TechnicalReportFile, fileName);
        }

        public static IsunFile CreateFinanceReportFile(string xml, string hash)
        {
            var _documentSerializer = System.Web.Mvc.DependencyResolver.Current.GetService<IDocumentSerializer>();
            var finance = _documentSerializer.XmlDeserializeFromString<R_10043.FinanceReport>(xml);

            string fileName = string.Empty;

            if (finance != null
                && finance.contractNumber != null
                && finance.docNumber != null
                && finance.docSubNumber != null)
            {
                fileName = string.Format("{0}_{1}.{2}", finance.contractNumber, finance.docNumber, finance.docSubNumber);
            }

            return CreateFile(xml, hash, IsunFileType.FinanceReportFile, fileName);
        }

        public static IsunFile CreatePaymentRequestFile(string xml, string hash)
        {
            var _documentSerializer = System.Web.Mvc.DependencyResolver.Current.GetService<IDocumentSerializer>();
            var payment = _documentSerializer.XmlDeserializeFromString<R_10045.PaymentRequest>(xml);

            string fileName = string.Empty;

            if (payment != null
                && payment.contractNumber != null
                && payment.docNumber != null
                && payment.docSubNumber != null)
            {
                fileName = string.Format("{0}_{1}.{2}", payment.contractNumber, payment.docNumber, payment.docSubNumber);
            }

            return CreateFile(xml, hash, IsunFileType.PaymentRequstFile, fileName);
        }

        private static IsunFile CreateFile(string xml, string hash, IsunFileType fileType, string fileName = null)
        {
            var name = fileName ?? hash;

            return new IsunFile()
            {
                Content = ZipManager.ZipProject(xml, hash),
                MimeType = MimeTypeFileExtension.MIME_APPLICATION_OCTET_STREAM,
                Filename = string.Format("{0}.{1}", name, fileType.GetEnumDescription())
            };
        }
    }
}
