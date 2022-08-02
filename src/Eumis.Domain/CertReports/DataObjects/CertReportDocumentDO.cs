using Eumis.Domain.Core;

namespace Eumis.Domain.CertReports.DataObjects
{
    public class CertReportDocumentDO
    {
        public CertReportDocumentDO()
        {
        }

        public CertReportDocumentDO(int certReportId, byte[] version)
        {
            this.CertReportId = certReportId;
            this.Version = version;
        }

        public CertReportDocumentDO(CertReportDocument certReportDocument, byte[] version)
        {
            this.CertReportDocumentId = certReportDocument.CertReportDocumentId;
            this.CertReportId = certReportDocument.CertReportId;
            this.Name = certReportDocument.Name;
            this.Description = certReportDocument.Description;

            if (certReportDocument.File != null)
            {
                this.File = new FileDO
                {
                    Key = certReportDocument.File.Key,
                    Name = certReportDocument.File.FileName,
                };
            }

            this.Version = version;
        }

        public int CertReportDocumentId { get; set; }

        public int CertReportId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
