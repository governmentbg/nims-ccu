using System;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Irregularities
{
    public class IrregularitySanction
    {
        public IrregularitySanctionProcedureType ProcedureType { get; set; }

        public IrregularitySanctionProcedureKind? ProcedureKind { get; set; }

        public DateTime? ProcedureStartDate { get; set; }

        public DateTime? ProcedureExpectedEndDate { get; set; }

        public DateTime? ProcedureEndDate { get; set; }

        public IrregularitySanctionProcedureStatus? ProcedureStatus { get; set; }

        public int? SanctionCategoryId { get; set; }

        public int? SanctionTypeId { get; set; }

        public string Fines { get; set; }

        public void SetAttributes(
            IrregularitySanctionProcedureType sanctionProcedureType,
            IrregularitySanctionProcedureKind? sanctionProcedureKind,
            DateTime? sanctionProcedureStartDate,
            DateTime? sanctionProcedureExpectedEndDate,
            DateTime? sanctionProcedureEndDate,
            IrregularitySanctionProcedureStatus? sanctionProcedureStatus,
            int? sanctionCategoryId,
            int? sanctionTypeId,
            string fines)
        {
            this.ProcedureType = sanctionProcedureType;
            this.ProcedureKind = sanctionProcedureKind;
            this.ProcedureStartDate = sanctionProcedureStartDate;
            this.ProcedureExpectedEndDate = sanctionProcedureExpectedEndDate;
            this.ProcedureEndDate = sanctionProcedureEndDate;
            this.ProcedureStatus = sanctionProcedureStatus;
            this.SanctionCategoryId = sanctionCategoryId;
            this.SanctionTypeId = sanctionTypeId;
            this.Fines = fines;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class IrregularitySanctionMap : ComplexTypeConfiguration<IrregularitySanction>
    {
        public IrregularitySanctionMap()
        {
        }
    }
}
