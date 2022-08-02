using Eumis.Domain.Core;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportCorrectionDocumentDO
    {
        public ContractReportCorrectionDocumentDO()
        {
        }

        public ContractReportCorrectionDocumentDO(int contractReportCorrectionId, byte[] version)
        {
            this.ContractReportCorrectionId = contractReportCorrectionId;
            this.Version = version;
        }

        public ContractReportCorrectionDocumentDO(ContractReportCorrectionDocument contractReportCorrectionDocument, byte[] version)
        {
            this.DocumentId = contractReportCorrectionDocument.ContractReportCorrectionDocumentId;
            this.ContractReportCorrectionId = contractReportCorrectionDocument.ContractReportCorrectionId;
            this.Description = contractReportCorrectionDocument.Description;

            this.File = new FileDO
            {
                Key = contractReportCorrectionDocument.FileKey,
                Name = contractReportCorrectionDocument.FileName,
            };

            this.Version = version;
        }

        public int ContractReportCorrectionId { get; set; }

        public int DocumentId { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
