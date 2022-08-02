using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Procedures;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public partial class EvalSessionSheetXml : IAggregateRoot
    {
        public EvalSessionSheetXml()
        {
        }

        public int EvalSessionSheetXmlId { get; set; }

        public Guid Gid { get; set; }

        public int EvalSessionId { get; set; }

        public int EvalSessionSheetId { get; set; }

        public ProcedureEvalType EvalType { get; set; }

        public ProcedureEvalTableType EvalTableType { get; set; }

        public bool? EvalIsPassed { get; set; }

        public decimal? EvalPoints { get; set; }

        public string EvalNote { get; set; }

        public string Xml { get; private set; }

        public string Hash { get; private set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    public class EvalSessionSheetXmlMap : EntityTypeConfiguration<EvalSessionSheetXml>
    {
        public EvalSessionSheetXmlMap()
        {
            // Primary Key
            this.HasKey(t => t.EvalSessionSheetXmlId);

            // Properties
            this.Property(t => t.EvalSessionSheetXmlId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EvalSessionSheetXmls");
            this.Property(t => t.EvalSessionSheetXmlId).HasColumnName("EvalSessionSheetXmlId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionSheetId).HasColumnName("EvalSessionSheetId");
            this.Property(t => t.EvalType).HasColumnName("EvalType");
            this.Property(t => t.EvalTableType).HasColumnName("EvalTableType");
            this.Property(t => t.EvalIsPassed).HasColumnName("EvalIsPassed");
            this.Property(t => t.EvalPoints).HasColumnName("EvalPoints");
            this.Property(t => t.EvalNote).HasColumnName("EvalNote");
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
