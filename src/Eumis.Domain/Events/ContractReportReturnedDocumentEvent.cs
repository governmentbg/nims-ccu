using Eumis.Domain.Contracts;
using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ContractReportReturnedDocumentEvent : IDomainEvent
    {
        public int ContractReportId { get; set; }

        public ContractReportDocumentType ContractReportDocumentType { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }
    }
}
