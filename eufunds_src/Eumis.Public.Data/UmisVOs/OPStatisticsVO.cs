using System;

namespace Eumis.Public.Data.UmisVOs
{
    public class OPStatisticsVO
    {
        public long ProceduresCount { get; set; }

        public long SubmittedProposalsCount { get; set; }

        public decimal ProposalsTotalAmount { get; set; }

        public decimal ProposalsBFPAmount { get; set; }

        public decimal ProposalsSelfAmount { get; set; }

        public long ApprovedProposalsCount { get; set; }

        public long ReserveProposalsCount { get; set; }

        public long RejectedProposalsCount { get; set; }

        public long EvalSessionsCount { get; set; }

        public long ActiveContractsCount { get; set; }

        public long PausedContractsCount { get; set; }

        public long MonitoredContractsCount { get; set; }

        public long CanceledContractsCount { get; set; }

        public long EndedContractsCount { get; set; }

        public long ConcludedContractsCount { get; set; }

        public long SuspendedContractsCount { get; set; }

        public long TotalContractsCount { get; set; }
    }
}
