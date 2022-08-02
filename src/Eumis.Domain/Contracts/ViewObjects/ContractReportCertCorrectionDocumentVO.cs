using Eumis.Domain.Core;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportCertCorrectionDocumentVO
    {
        public int DocumentId { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
