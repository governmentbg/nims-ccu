using Eumis.Domain.CertReports.DataObjects;
using Eumis.Domain.Core;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class CertReportCertificationDocumentsVO
    {
        public int CertReportCertificationDocumentId { get; set; }

        public int CertReportId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }

        public CertReportCertificationDocumentDO CertReportCertificationDocument { get; set; }
    }
}
