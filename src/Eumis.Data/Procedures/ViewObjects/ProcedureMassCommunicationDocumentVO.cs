using Eumis.Domain.Core;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureMassCommunicationDocumentVO
    {
        public int DocumentId { get; set; }

        public int ProcedureMassCommunicationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
