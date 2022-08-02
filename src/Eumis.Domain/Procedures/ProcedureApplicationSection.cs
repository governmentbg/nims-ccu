using Eumis.Domain.Procedures.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Procedures
{
    public class ProcedureApplicationSection
    {
        public ProcedureApplicationSection()
        {
        }

        public ProcedureApplicationSection(ApplicationSectionType section, bool isSelected, int orderNum)
        {
            this.Section = section;
            this.IsSelected = isSelected;
            this.OrderNum = orderNum;
        }

        public int ProcedureId { get; set; }

        public ApplicationSectionType Section { get; set; }

        public int OrderNum { get; set; }

        public bool IsSelected { get; set; }

        public virtual Procedure Procedure { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureApplicationSectionMap : EntityTypeConfiguration<ProcedureApplicationSection>
    {
        public ProcedureApplicationSectionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProcedureId, t.Section });

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.Section)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureApplicationSections");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Section).HasColumnName("Section");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.IsSelected).HasColumnName("IsSelected");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureApplicationSections)
                .HasForeignKey(t => t.ProcedureId)
                .WillCascadeOnDelete();
        }
    }
}
