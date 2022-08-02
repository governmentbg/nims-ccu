using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Guidances
{
    public partial class Guidance : IAggregateRoot
    {
        public Guidance()
        {
        }

        public Guidance(
            Guid blobKey,
            string fileName,
            string description,
            GuidanceModule module,
            int userId)
        {
            this.BlobKey = blobKey;
            this.FileName = fileName;
            this.Description = description;
            this.Module = module;
            this.CreatedByUserId = userId;

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int GuidanceId { get; set; }

        public Guid BlobKey { get; set; }

        public string FileName { get; set; }

        public string Description { get; set; }

        public GuidanceModule Module { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class GuidanceMap : EntityTypeConfiguration<Guidance>
    {
        public GuidanceMap()
        {
            // Primary Key
            this.HasKey(t => t.GuidanceId);

            // Properties
            this.Property(t => t.GuidanceId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Module)
                .IsRequired();

            this.Property(t => t.CreatedByUserId)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Guidances");
            this.Property(t => t.GuidanceId).HasColumnName("GuidanceId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Module).HasColumnName("Module");
            this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
