using Eumis.Domain.Core;

namespace Eumis.Data.ActuallyPaidAmounts.ViewObjects
{
    public class ActuallyPaidAmountDocumentVO
    {
        public int DocumentId { get; set; }

        public int ActuallyPaidAmountId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
