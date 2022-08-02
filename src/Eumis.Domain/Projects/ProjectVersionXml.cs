using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Eumis.Common;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Projects
{
    public partial class ProjectVersionXml : RioXmlDocumentWithFiles<Eumis.Rio.Project, ProjectVersionXmlFile>, IAggregateRoot, IEventEmitter
    {
        private ProjectVersionXml()
        {
            ((IEventEmitter)this).Events = new List<IDomainEvent>();
        }

        // first version for empty registered project
        public ProjectVersionXml(int projectId, Rio.Project versionDoc, int createdByUserId, string createNote, string createNoteAlt)
            : this()
        {
            this.Gid = Guid.NewGuid();
            this.ProjectId = projectId;
            this.OrderNum = 1;
            this.Status = ProjectVersionXmlStatus.Draft;

            var currentDate = DateTime.Now;
            this.CreatedByUserId = createdByUserId;
            this.CreateNote = createNote;
            this.CreateNoteAlt = createNoteAlt;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;

            this.SetXmlInt(versionDoc);
        }

        // first version for a registered project
        public ProjectVersionXml(int projectId, string xml, int createdByUserId, string createNote, string createNoteAlt, DateTime createDate)
            : this()
        {
            this.Gid = Guid.NewGuid();
            this.ProjectId = projectId;
            this.OrderNum = 1;
            this.Status = ProjectVersionXmlStatus.Actual;

            this.SetAttributes(createNote);
            this.CreateNoteAlt = createNoteAlt;
            this.CreatedByUserId = createdByUserId;
            this.CreateDate = createDate;
            this.ModifyDate = createDate;

            this.SetXmlInt(xml);
        }

        // make a new draft
        public ProjectVersionXml(ProjectVersionXml projectVersionXml, int createdByUserId, string createNote, string createNoteAlt, int orderNum)
            : this()
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ProjectId = projectVersionXml.ProjectId;
            this.OrderNum = orderNum;
            this.Status = ProjectVersionXmlStatus.Draft;
            this.CreatedByUserId = createdByUserId;
            this.CreateNote = createNote;
            this.CreateNoteAlt = createNoteAlt;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;

            var versionDoc = projectVersionXml.GetDocument();
            versionDoc.modificationDate = currentDate;
            this.SetXmlInt(versionDoc);
        }

        // make a new draft from communication
        public ProjectVersionXml(int projectId, int orderNum, string createNote, string createNoteAlt, string xml)
            : this()
        {
            this.Gid = Guid.NewGuid();
            this.ProjectId = projectId;
            this.OrderNum = orderNum;
            this.Status = ProjectVersionXmlStatus.Actual;

            this.SetXmlInt(xml);

            var currentDate = DateTime.Now;
            this.CreateNote = createNote;
            this.CreateNoteAlt = createNoteAlt;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ProjectVersionXmlId { get; set; }

        public Guid Gid { get; set; }

        public int ProjectId { get; set; }

        public int OrderNum { get; set; }

        public ProjectVersionXmlStatus Status { get; set; }

        public int? CreatedByUserId { get; set; }

        public string CreateNote { get; set; }

        public string CreateNoteAlt { get; set; }

        public decimal? TotalBfpAmount { get; set; }

        public decimal? CoFinancingAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }

        public override IList<ProjectVersionXmlFile> XmlFiles
        {
            get
            {
                var projectDocument = this.GetDocument();

                return EnumerableExtensions.Concat(
                    projectDocument.GetFiles(d => d.AttachedDocuments.AttachedDocumentCollection)
                    .Select(ad =>
                        new ProjectVersionXmlFile(ad)
                        {
                            Type = ProjectVersionXmlFileType.AttachedDoc,
                        }),
                    projectDocument.GetFiles(
                        d => d.AttachedDocuments.AttachedDocumentCollection,
                        d => d.SignatureContentCollection.Select(adc => new AttachedDocument() { AttachedDocumentContent = adc }))
                    .Select(ad =>
                        new ProjectVersionXmlFile(ad)
                        {
                            Type = ProjectVersionXmlFileType.Signature,
                        })).ToList();
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectVersionXmlMap : EntityTypeConfiguration<ProjectVersionXml>
    {
        public ProjectVersionXmlMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectVersionXmlId);

            // Properties
            this.Property(t => t.ProjectVersionXmlId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.ProjectId)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.CreateNote)
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
            this.ToTable("ProjectVersionXmls");
            this.Property(t => t.ProjectVersionXmlId).HasColumnName("ProjectVersionXmlId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            this.Property(t => t.CreateNote).HasColumnName("CreateNote");
            this.Property(t => t.CreateNoteAlt).HasColumnName("CreateNoteAlt");
            this.Property(t => t.TotalBfpAmount).HasColumnName("TotalBfpAmount");
            this.Property(t => t.CoFinancingAmount).HasColumnName("CoFinancingAmount");
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
