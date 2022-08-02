using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Indicators
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public partial class IndicatorItemType : IAggregateRoot
    {
        public IndicatorItemType()
        {
        }

        public IndicatorItemType(string name, string nameAlt)
            : this()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateAttributes(name, nameAlt);
        }

        public int IndicatorItemTypeId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class IndicatorItemTypeMap : EntityTypeConfiguration<IndicatorItemType>
    {
        public IndicatorItemTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.IndicatorItemTypeId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.NameAlt)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("IndicatorItemTypes");
            this.Property(t => t.IndicatorItemTypeId).HasColumnName("IndicatorItemTypeId");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
