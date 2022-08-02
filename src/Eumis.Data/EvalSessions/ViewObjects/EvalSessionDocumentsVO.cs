using Eumis.Domain.Core;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionDocumentsVO
    {
        public int? EvalSessionId { get; set; }

        public int? EvalSessionDocumentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }
    }
}
