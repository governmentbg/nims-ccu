using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.SapInterfaces
{
    public partial class SapSchema : IAggregateRoot
    {
        public SapSchema()
        {
        }

        public int SapSchemaId { get; set; }

        public string Content { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    public class SapSchemaMap : EntityTypeConfiguration<SapSchema>
    {
        public SapSchemaMap()
        {
            // Primary Key
            this.HasKey(t => t.SapSchemaId);

            // Properties
            this.Property(t => t.SapSchemaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Content)
                .IsRequired();
            this.Property(t => t.IsActive)
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
            this.ToTable("SapSchemas");
            this.Property(t => t.SapSchemaId).HasColumnName("SapSchemaId");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
