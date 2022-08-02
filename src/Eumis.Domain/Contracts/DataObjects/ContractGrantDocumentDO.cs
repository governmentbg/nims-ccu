using Eumis.Domain.Core;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractGrantDocumentDO
    {
        public ContractGrantDocumentDO()
        {
        }

        public ContractGrantDocumentDO(int contractId, byte[] version)
        {
            this.ContractId = contractId;
            this.Version = version;
        }

        public ContractGrantDocumentDO(ContractGrantDocument contractGrantDocument, byte[] version)
        {
            this.ContractGrantDocumentId = contractGrantDocument.ContractGrantDocumentId;
            this.ContractId = contractGrantDocument.ContractId;
            this.Name = contractGrantDocument.Name;
            this.Description = contractGrantDocument.Description;

            if (contractGrantDocument.File != null)
            {
                this.File = new FileDO
                {
                    Key = contractGrantDocument.File.Key,
                    Name = contractGrantDocument.File.FileName,
                };
            }

            this.Version = version;
        }

        public int ContractGrantDocumentId { get; set; }

        public int ContractId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
