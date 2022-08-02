using System;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public class IrregularitySanction
    {
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

        public IrregularitySanctionProcedureType ProcedureType { get; set; }

        public IrregularitySanctionProcedureKind? ProcedureKind { get; set; }

        public DateTime? ProcedureStartDate { get; set; }

        public DateTime? ProcedureExpectedEndDate { get; set; }

        public DateTime? ProcedureEndDate { get; set; }

        public IrregularitySanctionProcedureStatus? ProcedureStatus { get; set; }

        public int? SanctionCategoryId { get; set; }

        public int? SanctionTypeId { get; set; }

        public string Fines { get; set; }
    }

    public class IrregularitySanctionMap : ComplexTypeConfiguration<IrregularitySanction>
    {
        public IrregularitySanctionMap()
        {
        }
    }
}
