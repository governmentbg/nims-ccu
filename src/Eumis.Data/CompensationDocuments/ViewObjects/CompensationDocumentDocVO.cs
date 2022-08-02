using Eumis.Domain.Core;

namespace Eumis.Data.CompensationDocuments.ViewObjects
{
    public class CompensationDocumentDocVO
    {
        public int DocumentId { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
