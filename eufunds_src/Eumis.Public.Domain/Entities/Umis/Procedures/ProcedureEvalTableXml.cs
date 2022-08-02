using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public partial class ProcedureEvalTableXml : IAggregateRoot
    {
        public string Xml { get; private set; }

        public string Hash { get; private set; }

        public int ProcedureEvalTableXmlId { get; set; }

        public Guid Gid { get; set; }

        public int ProcedureId { get; set; }

        public int ProcedureEvalTableId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    public class ProcedureEvalTableXmlMap : EntityTypeConfiguration<ProcedureEvalTableXml>
    {
        public ProcedureEvalTableXmlMap()
            :base()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureEvalTableXmlId);

            // Properties
            this.Property(t => t.ProcedureEvalTableXmlId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.ProcedureEvalTableId)
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
            this.ToTable("ProcedureEvalTableXmls");
            this.Property(t => t.ProcedureEvalTableXmlId).HasColumnName("ProcedureEvalTableXmlId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ProcedureEvalTableId).HasColumnName("ProcedureEvalTableId");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            //RioXmlDocument Mapping
            this.Property(t => t.Xml)
                .IsRequired();

            this.Property(t => t.Hash)
                .IsFixedLength()
                .HasMaxLength(10)
                .IsRequired();

            this.Property(t => t.Xml).HasColumnName("Xml");
            this.Property(t => t.Hash).HasColumnName("Hash");
        }
    }
}
