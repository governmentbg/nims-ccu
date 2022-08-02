using Eumis.Domain.Core;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractGrantDocumentsVO
    {
        public int ContractGrantDocumentId { get; set; }

        public int ContractId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
