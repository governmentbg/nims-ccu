using Eumis.Domain.Core;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportRevalidationDocumentDO
    {
        public ContractReportRevalidationDocumentDO()
        {
        }

        public ContractReportRevalidationDocumentDO(int contractReportRevalidationId, byte[] version)
        {
            this.ContractReportRevalidationId = contractReportRevalidationId;
            this.Version = version;
        }

        public ContractReportRevalidationDocumentDO(ContractReportRevalidationDocument contractReportRevalidationDocument, byte[] version)
        {
            this.DocumentId = contractReportRevalidationDocument.ContractReportRevalidationDocumentId;
            this.ContractReportRevalidationId = contractReportRevalidationDocument.ContractReportRevalidationId;
            this.Description = contractReportRevalidationDocument.Description;

            this.File = new FileDO
            {
                Key = contractReportRevalidationDocument.FileKey,
                Name = contractReportRevalidationDocument.FileName,
            };

            this.Version = version;
        }

        public int ContractReportRevalidationId { get; set; }

        public int DocumentId { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
