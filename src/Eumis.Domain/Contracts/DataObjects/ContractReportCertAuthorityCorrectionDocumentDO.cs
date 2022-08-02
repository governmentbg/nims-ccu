using Eumis.Domain.Core;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportCertAuthorityCorrectionDocumentDO
    {
        public ContractReportCertAuthorityCorrectionDocumentDO()
        {
        }

        public ContractReportCertAuthorityCorrectionDocumentDO(int contractReportCertAuthorityCorrectionId, byte[] version)
        {
            this.ContractReportCertAuthorityCorrectionId = contractReportCertAuthorityCorrectionId;
            this.Version = version;
        }

        public ContractReportCertAuthorityCorrectionDocumentDO(ContractReportCertAuthorityCorrectionDocument contractReportCertAuthorityCorrectionDocument, byte[] version)
        {
            this.DocumentId = contractReportCertAuthorityCorrectionDocument.ContractReportCertAuthorityCorrectionDocumentId;
            this.ContractReportCertAuthorityCorrectionId = contractReportCertAuthorityCorrectionDocument.ContractReportCertAuthorityCorrectionId;
            this.Description = contractReportCertAuthorityCorrectionDocument.Description;

            this.File = new FileDO
            {
                Key = contractReportCertAuthorityCorrectionDocument.FileKey,
                Name = contractReportCertAuthorityCorrectionDocument.FileName,
            };

            this.Version = version;
        }

        public int ContractReportCertAuthorityCorrectionId { get; set; }

        public int DocumentId { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
