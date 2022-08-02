using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public partial class EvalSessionStandpointXml : IAggregateRoot
    {
        public string Xml { get; private set; }

        public string Hash { get; private set; }

        public int EvalSessionStandpointXmlId { get; set; }

        public Guid Gid { get; set; }

        public int EvalSessionId { get; set; }

        public int EvalSessionStandpointId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    public class EvalSessionStandpointXmlMap : EntityTypeConfiguration<EvalSessionStandpointXml>
    {
        public EvalSessionStandpointXmlMap()
        {
            // Primary Key
            this.HasKey(t => t.EvalSessionStandpointXmlId);

            // Properties
            this.Property(t => t.EvalSessionStandpointXmlId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();
            this.Property(t => t.EvalSessionId)
                .IsRequired();
            this.Property(t => t.EvalSessionStandpointId)
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
            this.ToTable("EvalSessionStandpointXmls");
            this.Property(t => t.EvalSessionStandpointXmlId).HasColumnName("EvalSessionStandpointXmlId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionStandpointId).HasColumnName("EvalSessionStandpointId");
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
