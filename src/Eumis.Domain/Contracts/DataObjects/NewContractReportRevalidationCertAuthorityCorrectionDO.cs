using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class NewContractReportRevalidationCertAuthorityCorrectionDO
    {
        public ContractReportRevalidationCertAuthorityCorrectionType? Type { get; set; }

        public Sign? Sign { get; set; }

        public DateTime? Date { get; set; }

        public int? ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ContractId { get; set; }

        public int? ContractReportPaymentId { get; set; }
    }
}
