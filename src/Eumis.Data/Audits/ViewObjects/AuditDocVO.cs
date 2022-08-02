using Eumis.Domain.Core;

namespace Eumis.Data.Audits.ViewObjects
{
    public class AuditDocVO
    {
        public int DocumentId { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
