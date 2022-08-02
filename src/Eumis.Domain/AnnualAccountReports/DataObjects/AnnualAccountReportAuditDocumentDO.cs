using Eumis.Domain.AnnualAccountReports;
using Eumis.Domain.Core;

namespace Eumis.Domain.AnnualAccountReports.DataObjects
{
    public class AnnualAccountReportAuditDocumentDO
    {
        public AnnualAccountReportAuditDocumentDO()
        {
        }

        public AnnualAccountReportAuditDocumentDO(int annualAccounReportId, byte[] version)
        {
            this.AnnualAccountReportId = annualAccounReportId;
            this.Version = version;
        }

        public AnnualAccountReportAuditDocumentDO(AnnualAccountReportAuditDocument auditDocument, byte[] version)
        {
            this.AnnualAccountReportAuditDocumentId = auditDocument.AnnualAccountReportAuditDocumentId;
            this.AnnualAccountReportId = auditDocument.AnnualAccountReportId;
            this.Name = auditDocument.Name;
            this.Description = auditDocument.Description;

            if (auditDocument.File != null)
            {
                this.File = new FileDO
                {
                    Key = auditDocument.File.Key,
                    Name = auditDocument.File.FileName,
                };
            }

            this.Version = version;
        }

        public int AnnualAccountReportAuditDocumentId { get; set; }

        public int AnnualAccountReportId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
