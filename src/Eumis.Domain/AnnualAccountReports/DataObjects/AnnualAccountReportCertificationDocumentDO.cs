using Eumis.Domain.AnnualAccountReports;
using Eumis.Domain.Core;

namespace Eumis.Domain.AnnualAccountReports.DataObjects
{
    public class AnnualAccountReportCertificationDocumentDO
    {
        public AnnualAccountReportCertificationDocumentDO()
        {
        }

        public AnnualAccountReportCertificationDocumentDO(int annualAccounReportId, byte[] version)
        {
            this.AnnualAccountReportId = annualAccounReportId;
            this.Version = version;
        }

        public AnnualAccountReportCertificationDocumentDO(AnnualAccountReportCertificationDocument certificationDocument, byte[] version)
        {
            this.AnnualAccountReportCertificationDocumentId = certificationDocument.AnnualAccountReportCertificationDocumentId;
            this.AnnualAccountReportId = certificationDocument.AnnualAccountReportId;
            this.Name = certificationDocument.Name;
            this.Description = certificationDocument.Description;

            if (certificationDocument.File != null)
            {
                this.File = new FileDO
                {
                    Key = certificationDocument.File.Key,
                    Name = certificationDocument.File.FileName,
                };
            }

            this.Version = version;
        }

        public int AnnualAccountReportCertificationDocumentId { get; set; }

        public int AnnualAccountReportId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
