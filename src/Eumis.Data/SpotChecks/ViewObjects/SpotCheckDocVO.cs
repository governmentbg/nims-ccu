using Eumis.Domain.Core;

namespace Eumis.Data.SpotChecks.ViewObjects
{
    public class SpotCheckDocVO
    {
        public int DocumentId { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
