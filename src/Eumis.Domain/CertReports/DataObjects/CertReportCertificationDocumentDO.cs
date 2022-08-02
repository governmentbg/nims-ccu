using Eumis.Domain.Core;

namespace Eumis.Domain.CertReports.DataObjects
{
    public class CertReportCertificationDocumentDO
    {
        public CertReportCertificationDocumentDO()
        {
        }

        public CertReportCertificationDocumentDO(int certReportId, byte[] version)
        {
            this.CertReportId = certReportId;
            this.Version = version;
        }

        public CertReportCertificationDocumentDO(CertReportCertificationDocument certReportCertificationDocument, byte[] version)
        {
            this.CertReportCertificationDocumentId = certReportCertificationDocument.CertReportCertificationDocumentId;
            this.CertReportId = certReportCertificationDocument.CertReportId;
            this.Name = certReportCertificationDocument.Name;
            this.Description = certReportCertificationDocument.Description;

            if (certReportCertificationDocument.File != null)
            {
                this.File = new FileDO
                {
                    Key = certReportCertificationDocument.File.Key,
                    Name = certReportCertificationDocument.File.FileName,
                };
            }

            this.Version = version;
        }

        public int CertReportCertificationDocumentId { get; set; }

        public int CertReportId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
