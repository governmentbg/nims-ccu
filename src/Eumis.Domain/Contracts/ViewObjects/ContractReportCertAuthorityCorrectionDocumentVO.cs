using Eumis.Domain.Core;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportCertAuthorityCorrectionDocumentVO
    {
        public int DocumentId { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
