using Eumis.Domain.Core;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrregularityDocVO
    {
        public int DocumentId { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
