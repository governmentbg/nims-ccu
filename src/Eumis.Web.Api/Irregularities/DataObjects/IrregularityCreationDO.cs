using System;
using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;

namespace Eumis.Web.Api.Irregularities.DataObjects
{
    public class IrregularityCreationDO
    {
        public int? IrregularitySignalId { get; set; }

        public DateTime? IrregularityDateFrom { get; set; }

        public DateTime? IrregularityDateTo { get; set; }

        public Year? ReportYear { get; set; }

        public Quarter? ReportQuarter { get; set; }

        public bool? ShouldReportToOlaf { get; set; }

        public IrregularityReasonNotReportingToOlaf? ReasonNotReportingToOlaf { get; set; }

        public IrregularitySanctionProcedureType? SanctionProcedureType { get; set; }

        public IrregularitySanctionProcedureKind? SanctionProcedureKind { get; set; }

        public DateTime? SanctionProcedureStartDate { get; set; }

        public DateTime? SanctionProcedureExpectedEndDate { get; set; }

        public DateTime? SanctionProcedureEndDate { get; set; }

        public IrregularitySanctionProcedureStatus? SanctionProcedureStatus { get; set; }

        public int? SanctionCategoryId { get; set; }

        public int? SanctionTypeId { get; set; }

        public string Fines { get; set; }
    }
}
