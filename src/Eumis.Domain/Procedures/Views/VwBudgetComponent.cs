using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Procedures.Views
{
    public class VwBudgetComponent
    {
        public int ProcedureId { get; set; }

        public int ProcedureBudgetComponentId { get; set; }

        public int KidCodeId { get; set; }

        public int CompanySizeTypeId { get; set; }

        public ActiveStatus Status { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class VwBudgetComponentMap : EntityTypeConfiguration<VwBudgetComponent>
    {
        public VwBudgetComponentMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProcedureId, t.KidCodeId, t.CompanySizeTypeId });

            // Properties
            this.Property(t => t.ProcedureBudgetComponentId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("vwUniqueProcedureBudgetSizeTypeKidCodeIndexed");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ProcedureBudgetComponentId).HasColumnName("ProcedureBudgetComponentId");
            this.Property(t => t.KidCodeId).HasColumnName("KidCodeId");
            this.Property(t => t.CompanySizeTypeId).HasColumnName("CompanySizeTypeId");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
