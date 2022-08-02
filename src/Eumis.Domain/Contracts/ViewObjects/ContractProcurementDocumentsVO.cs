using Eumis.Domain.Core;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractProcurementDocumentsVO
    {
        public int ContractProcurementDocumentId { get; set; }

        public int ContractId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
