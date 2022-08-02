using System;
using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrregularityDataVO
    {
        public int IrregularityId { get; set; }

        public string RegNumber { get; set; }

        public string ProgrammePeriod { get; set; }

        public IrregularityCaseState? CaseState { get; set; }

        public IrregularityStatus Status { get; set; }

        public bool IsRemoved { get; set; }

        public DateTime? EndDate { get; set; }

        public int ProgrammeId { get; set; }

        public int ProcedureId { get; set; }

        public string ProjectRegNumber { get; set; }

        public string ProjectName { get; set; }

        public Year? FirstReportYear { get; set; }

        public Quarter? FirstReportQuarter { get; set; }

        public Year? LastReportYear { get; set; }

        public Quarter? LastReportQuarter { get; set; }

        public string SignalNumber { get; set; }

        public DateTime? SignalRegDate { get; set; }

        public string SignalSource { get; set; }

        public string SignalActRegNum { get; set; }

        public DateTime? SignalActRegDate { get; set; }

        public string DeleteNote { get; set; }

        public byte[] Version { get; set; }
    }
}
