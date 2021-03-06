using Eumis.Domain.CertReports.DataObjects;
using Eumis.Domain.Core;

namespace Eumis.Domain.AnnualAccountReports.ViewObjects
{
    public class AnnualAccountReportAuditDocumentVO
    {
        public int AnnualAccountReportAuditDocumentId { get; set; }

        public int AnnualAccountReportId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
