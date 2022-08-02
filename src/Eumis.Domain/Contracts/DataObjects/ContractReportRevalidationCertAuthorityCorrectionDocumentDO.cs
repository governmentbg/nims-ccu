using Eumis.Domain.Core;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportRevalidationCertAuthorityCorrectionDocumentDO
    {
        public ContractReportRevalidationCertAuthorityCorrectionDocumentDO()
        {
        }

        public ContractReportRevalidationCertAuthorityCorrectionDocumentDO(int contractReportRevalidationCertAuthorityCorrectionId, byte[] version)
        {
            this.ContractReportRevalidationCertAuthorityCorrectionId = contractReportRevalidationCertAuthorityCorrectionId;
            this.Version = version;
        }

        public ContractReportRevalidationCertAuthorityCorrectionDocumentDO(ContractReportRevalidationCertAuthorityCorrectionDocument contractReportRevalidationCertAuthorityCorrectionDocument, byte[] version)
        {
            this.DocumentId = contractReportRevalidationCertAuthorityCorrectionDocument.ContractReportRevalidationCertAuthorityCorrectionDocumentId;
            this.ContractReportRevalidationCertAuthorityCorrectionId = contractReportRevalidationCertAuthorityCorrectionDocument.ContractReportRevalidationCertAuthorityCorrectionId;
            this.Description = contractReportRevalidationCertAuthorityCorrectionDocument.Description;

            this.File = new FileDO
            {
                Key = contractReportRevalidationCertAuthorityCorrectionDocument.FileKey,
                Name = contractReportRevalidationCertAuthorityCorrectionDocument.FileName,
            };

            this.Version = version;
        }

        public int ContractReportRevalidationCertAuthorityCorrectionId { get; set; }

        public int DocumentId { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
