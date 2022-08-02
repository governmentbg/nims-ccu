using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Measures
{
    public partial class Measure : IAggregateRoot
    {
        public Measure()
        {
        }

        public Measure(
            string shortName,
            string name,
            string nameAlt)
        {
            var currentDate = DateTime.Now;

            this.ShortName = shortName;
            this.Name = name;
            this.NameAlt = nameAlt;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int MeasureId { get; set; }

        public string ShortName { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class MeasureMap : EntityTypeConfiguration<Measure>
    {
        public MeasureMap()
        {
            // Primary Key
            this.HasKey(t => t.MeasureId);

            // Properties
            this.Property(t => t.MeasureId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ShortName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.NameAlt)
                .HasMaxLength(100);

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
            this.ToTable("Measures");
            this.Property(t => t.MeasureId).HasColumnName("MeasureId");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
