using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Procedures
{
    public partial class ProcedureEvalTableXml : RioXmlDocumentWithFiles<Rio.EvalTable, ProcedureEvalTableXmlFile>, IAggregateRoot
    {
        public ProcedureEvalTableXml()
        {
        }

        public ProcedureEvalTableXml(int procedureId, int procedureEvalTableId, string xml)
        {
            this.ProcedureId = procedureId;
            this.ProcedureEvalTableId = procedureEvalTableId;
            this.Gid = Guid.NewGuid();

            base.SetXml(xml);

            DateTime currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ProcedureEvalTableXmlId { get; set; }

        public Guid Gid { get; set; }

        public int ProcedureId { get; set; }

        public int ProcedureEvalTableId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public override IList<ProcedureEvalTableXmlFile> XmlFiles
        {
            get
            {
                return this.GetDocument()
                    .GetFiles(d => d.AttachedDocumentCollection)
                    .Select(ad => new ProcedureEvalTableXmlFile(ad))
                    .ToList();
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureEvalTableXmlMap : EntityTypeConfiguration<ProcedureEvalTableXml>
    {
        public ProcedureEvalTableXmlMap()
            : base()
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

            // RioXmlDocument Mapping
            this.Property(t => t.Xml)
                .IsRequired();

            this.Property(t => t.Hash)
                .IsFixedLength()
                .HasMaxLength(10)
                .IsRequired();

            this.Property(t => t.Xml).HasColumnName("Xml");
            this.Property(t => t.Hash).HasColumnName("Hash");

            this.Ignore(t => t.XmlFiles);
        }
    }
}
