using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Eumis.Common;
using Eumis.Domain.Core;
using Eumis.Domain.Procedures;
using Eumis.Rio;

namespace Eumis.Domain.EvalSessions
{
    public partial class EvalSessionSheetXml : RioXmlDocumentWithFiles<EvalSheet, EvalSessionSheetXmlFile>, IAggregateRoot
    {
        public EvalSessionSheetXml()
        {
        }

        public EvalSessionSheetXml(
            int evalSessionId,
            int evalSessionSheetId,
            EvalSessionSheetXml evalSessionSheetXml)
        {
            var currentDate = DateTime.Now;

            this.EvalSessionId = evalSessionId;
            this.EvalSessionSheetId = evalSessionSheetId;
            this.Gid = Guid.NewGuid();

            this.EvalTableType = evalSessionSheetXml.EvalTableType;
            this.EvalType = evalSessionSheetXml.EvalType;

            var sheetDoc = evalSessionSheetXml.GetDocument();
            sheetDoc.modificationDate = currentDate;
            this.SetXml(sheetDoc);

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public EvalSessionSheetXml(
            int evalSessionId,
            int evalSessionSheetId,
            ProcedureEvalType evalType,
            ProcedureEvalTableType evalTableType,
            string xml)
            : this()
        {
            this.EvalSessionId = evalSessionId;
            this.EvalSessionSheetId = evalSessionSheetId;
            this.Gid = Guid.NewGuid();

            this.EvalTableType = evalTableType;
            this.EvalType = evalType;

            base.SetXml(xml);

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
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

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public override IList<EvalSessionSheetXmlFile> XmlFiles
        {
            get
            {
                var evalSessionSheetDoc = this.GetDocument();

                return EnumerableExtensions.Concat(
                    evalSessionSheetDoc.GetFiles(d => d.AttachedDocumentCollection)
                    .Select(ad =>
                        new EvalSessionSheetXmlFile(ad)
                        {
                            Type = EvalSessionSheetXmlFileType.AttachedDoc,
                        }),
                    evalSessionSheetDoc.GetFiles(d => d.EvalTableAttachedDocumentCollection)
                    .Select(ad =>
                        new EvalSessionSheetXmlFile(ad)
                        {
                            Type = EvalSessionSheetXmlFileType.EvalTableAttachedDoc,
                        })).ToList();
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
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
