using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Eumis.Common;
using Eumis.Domain.Core;
using Eumis.Domain.Services;
using Eumis.Rio;

namespace Eumis.Domain.Registrations
{
    public partial class RegProjectXml : RioXmlDocumentWithFiles<Eumis.Rio.Project, RegProjectXmlFile>, IAggregateRoot, IEventEmitter, INotificationEventEmitter
    {
        private RegProjectXml()
        {
            ((IEventEmitter)this).Events = new List<IDomainEvent>();
            ((INotificationEventEmitter)this).NotificationEvents = new List<INotificationEvent>();
        }

        private RegProjectXml(string xml)
        {
            base.SetXml(xml);

            ((IEventEmitter)this).Events = null;
            ((INotificationEventEmitter)this).NotificationEvents = null;
        }

        public RegProjectXml(int registrationId, string xml, IProcedureDomainService procedureDomainService)
        {
            this.Gid = Guid.NewGuid();

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;

            this.RegistrationId = registrationId;
            this.Status = RegProjectXmlStatus.Draft;

            this.SetXmlInt(xml, procedureDomainService);

            ((IEventEmitter)this).Events = new List<IDomainEvent>();
            ((INotificationEventEmitter)this).NotificationEvents = new List<INotificationEvent>();
        }

        public int RegProjectXmlId { get; set; }

        public Guid Gid { get; set; }

        public int RegistrationId { get; set; }

        public int ProcedureId { get; set; }

        public RegProjectXmlStatus Status { get; set; }

        public string ProjectName { get; set; }

        public string ProjectNameAlt { get; set; }

        public string CompanyName { get; set; }

        public string CompanyNameAlt { get; set; }

        public RegProjectXmlRegType? RegistrationType { get; set; }

        public int? ProjectId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }

        ICollection<INotificationEvent> INotificationEventEmitter.NotificationEvents { get; set; }

        public override IList<RegProjectXmlFile> XmlFiles
        {
            get
            {
                var projectDocument = this.GetDocument();

                return EnumerableExtensions.Concat(
                    projectDocument.GetFiles(d => d.AttachedDocuments.AttachedDocumentCollection)
                    .Select(ad =>
                        new RegProjectXmlFile(ad)
                        {
                            Type = RegProjectXmlFileType.AttachedDoc,
                        }),
                    projectDocument.GetFiles(
                        d => d.AttachedDocuments.AttachedDocumentCollection,
                        d => d.SignatureContentCollection.Select(adc => new AttachedDocument() { AttachedDocumentContent = adc }))
                    .Select(ad =>
                        new RegProjectXmlFile(ad)
                        {
                            Type = RegProjectXmlFileType.Signature,
                        })).ToList();
            }
        }

        public static IEnumerable<RegProjectXmlFile> GetFilesFromXml(string xml)
        {
            return new RegProjectXml(xml).XmlFiles;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class RegProjectXmlMap : EntityTypeConfiguration<RegProjectXml>
    {
        public RegProjectXmlMap()
        {
            // Primary Key
            this.HasKey(t => t.RegProjectXmlId);

            // Properties
            this.Property(t => t.RegProjectXmlId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.RegistrationId)
                .IsRequired();

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.ProjectName)
                .HasMaxLength(400)
                .IsOptional();

            this.Property(t => t.ProjectNameAlt)
                .HasMaxLength(400)
                .IsOptional();

            this.Property(t => t.CompanyName)
                .HasMaxLength(200)
                .IsOptional();

            this.Property(t => t.CompanyNameAlt)
                .HasMaxLength(200)
                .IsOptional();

            this.Property(t => t.RegistrationType)
                .IsOptional();

            this.Property(t => t.ProjectId)
                .IsOptional();

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
            this.ToTable("RegProjectXmls");
            this.Property(t => t.RegProjectXmlId).HasColumnName("RegProjectXmlId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.RegistrationId).HasColumnName("RegistrationId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ProjectName).HasColumnName("ProjectName");
            this.Property(t => t.ProjectNameAlt).HasColumnName("ProjectNameAlt");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.CompanyNameAlt).HasColumnName("CompanyNameAlt");
            this.Property(t => t.RegistrationType).HasColumnName("RegistrationType");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
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
