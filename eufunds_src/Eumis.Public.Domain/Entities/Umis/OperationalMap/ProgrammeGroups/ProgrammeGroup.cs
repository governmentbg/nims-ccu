using Eumis.Public.Common.Localization;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.ProgrammeGroups
{
    public partial class ProgrammeGroup : IAggregateRoot
    {
        public ProgrammeGroup()
        {
        }

        public int ProgrammeGroupId { get; set; }

        public int PortalOrderNum { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public virtual string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.NameAlt;
                }
                else
                {
                    return this.Name;
                }
            }
        }

        public string ShortName { get; set; }

        public string ShortNameAlt { get; set; }

        public virtual string TransShortName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.ShortNameAlt;
                }
                else
                {
                    return this.ShortName;
                }
            }
        }

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProgrammeGroupMap : EntityTypeConfiguration<ProgrammeGroup>
    {
        public ProgrammeGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.ProgrammeGroupId);

            // Properties
            this.Property(t => t.ProgrammeGroupId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.PortalOrderNum)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.NameAlt)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ShortName)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ShortNameAlt)
                .IsRequired()
                .HasMaxLength(10);

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
            this.ToTable("ProgrammeGroups");
            this.Property(t => t.ProgrammeGroupId).HasColumnName("ProgrammeGroupId");
            this.Property(t => t.PortalOrderNum).HasColumnName("PortalOrderNum");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.ShortNameAlt).HasColumnName("ShortNameAlt");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
        }
    }
}
