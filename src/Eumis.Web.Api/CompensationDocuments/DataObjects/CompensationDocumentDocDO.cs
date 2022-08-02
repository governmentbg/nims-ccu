using Eumis.Domain.Core;
using Eumis.Domain.MonitoringFinancialControl.CompensationDocuments;

namespace Eumis.Web.Api.CompensationDocuments.DataObjects
{
    public class CompensationDocumentDocDO
    {
        public CompensationDocumentDocDO()
        {
        }

        public CompensationDocumentDocDO(int compensationDocumentId, byte[] version)
        {
            this.CompensationDocumentId = compensationDocumentId;
            this.Version = version;
        }

        public CompensationDocumentDocDO(CompensationDocumentDoc compensationDocumentDoc, byte[] version)
        {
            this.DocumentId = compensationDocumentDoc.CompensationDocumentDocId;
            this.CompensationDocumentId = compensationDocumentDoc.CompensationDocumentId;
            this.Description = compensationDocumentDoc.Description;

            this.File = new FileDO
            {
                Key = compensationDocumentDoc.FileKey,
                Name = compensationDocumentDoc.FileName,
            };

            this.Version = version;
        }

        public int CompensationDocumentId { get; set; }

        public int DocumentId { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
