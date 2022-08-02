using Eumis.Domain.Core;

namespace Eumis.Domain.Contracts
{
    public class ContractProcurementDocumentDO
    {
        public ContractProcurementDocumentDO()
        {
        }

        public ContractProcurementDocumentDO(int contractId, byte[] version)
        {
            this.ContractId = contractId;
            this.Version = version;
        }

        public ContractProcurementDocumentDO(ContractProcurementDocument contractProcurementDocument, byte[] version)
        {
            this.ContractProcurementDocumentId = contractProcurementDocument.ContractProcurementDocumentId;
            this.ContractId = contractProcurementDocument.ContractId;
            this.Name = contractProcurementDocument.Name;
            this.Description = contractProcurementDocument.Description;

            if (contractProcurementDocument.File != null)
            {
                this.File = new FileDO
                {
                    Key = contractProcurementDocument.File.Key,
                    Name = contractProcurementDocument.File.FileName,
                };
            }

            this.Version = version;
        }

        public int ContractProcurementDocumentId { get; set; }

        public int ContractId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
