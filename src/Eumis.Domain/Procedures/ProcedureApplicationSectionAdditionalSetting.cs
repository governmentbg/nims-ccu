using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures
{
    public class ProcedureApplicationSectionAdditionalSetting
    {
        public ProcedureApplicationSectionAdditionalSetting()
        {
        }

        public ProcedureApplicationSectionAdditionalSetting(
            int procedureId,
            bool fillMainData = false)
        {
            this.ProcedureId = procedureId;
            this.FillMainData = fillMainData;
        }

        public int ProcedureId { get; set; }

        public bool FillMainData { get; set; }

        public virtual Procedure Procedure { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureApplicationSectionAdditionalSettingMap : EntityTypeConfiguration<ProcedureApplicationSectionAdditionalSetting>
    {
        public ProcedureApplicationSectionAdditionalSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureId);

            // Properties
            this.Property(t => t.ProcedureId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FillMainData)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureApplicationSectionAdditionalSettings");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.FillMainData).HasColumnName("FillMainData");

            // Relationships
            this.HasRequired(t => t.Procedure)
                .WithOptional(t => t.ProcedureApplicationSectionAdditionalSetting);
        }
    }
}
