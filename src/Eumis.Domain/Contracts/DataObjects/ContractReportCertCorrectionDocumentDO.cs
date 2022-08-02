using Eumis.Domain.Core;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportCertCorrectionDocumentDO
    {
        public ContractReportCertCorrectionDocumentDO()
        {
        }

        public ContractReportCertCorrectionDocumentDO(int contractReportCertCorrectionId, byte[] version)
        {
            this.ContractReportCertCorrectionId = contractReportCertCorrectionId;
            this.Version = version;
        }

        public ContractReportCertCorrectionDocumentDO(ContractReportCertCorrectionDocument contractReportCertCorrectionDocument, byte[] version)
        {
            this.DocumentId = contractReportCertCorrectionDocument.ContractReportCertCorrectionDocumentId;
            this.ContractReportCertCorrectionId = contractReportCertCorrectionDocument.ContractReportCertCorrectionId;
            this.Description = contractReportCertCorrectionDocument.Description;

            this.File = new FileDO
            {
                Key = contractReportCertCorrectionDocument.FileKey,
                Name = contractReportCertCorrectionDocument.FileName,
            };

            this.Version = version;
        }

        public int ContractReportCertCorrectionId { get; set; }

        public int DocumentId { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
