using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Eumis.Common;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportTechnical : RioXmlDocumentWithFiles<Rio.TechnicalReport, ContractReportTechnicalFile>, IAggregateRoot, INotificationEventEmitter, IEventEmitter
    {
        private ContractReportTechnical()
        {
            this.ContractReportTechnicalMembers = new List<ContractReportTechnicalMember>();
            ((INotificationEventEmitter)this).NotificationEvents = new List<INotificationEvent>();
            ((IEventEmitter)this).Events = new List<IDomainEvent>();
        }

        public ContractReportTechnical(
            int contractId,
            int contractReportId,
            int versionSubNum,
            ContractReportTechnical oldTechnical)
            : this()
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractId = contractId;
            this.ContractReportId = contractReportId;
            this.VersionNum = oldTechnical.VersionNum;
            this.VersionSubNum = versionSubNum;
            this.Status = ContractReportTechnicalStatus.Draft;

            // change the document modificationDate so that a unique hash is produced
            var technicalDoc = oldTechnical.GetDocument();
            technicalDoc.modificationDate = currentDate;
            technicalDoc.docSubNumber = versionSubNum.ToString();
            this.SetXml(technicalDoc);

            this.RegDate = currentDate;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public ContractReportTechnical(
            int contractId,
            int contractReportId,
            int versionNum,
            int versionSubNum,
            string xml)
            : this()
        {
            var currentDate = DateTime.Now;

            this.SetXml(xml);

            this.Gid = Guid.NewGuid();
            this.ContractId = contractId;
            this.ContractReportId = contractReportId;
            this.VersionNum = versionNum;
            this.VersionSubNum = versionSubNum;
            this.Status = ContractReportTechnicalStatus.Draft;

            this.RegDate = currentDate;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportTechnicalId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        public ContractReportTechnicalStatus Status { get; set; }

        public string StatusNote { get; set; }

        public ContractReportTechnicalType? Type { get; set; }

        public DateTime? RegDate { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public int? SenderContractRegistrationId { get; set; }

        public virtual ICollection<ContractReportTechnicalMember> ContractReportTechnicalMembers { get; set; }

        ICollection<INotificationEvent> INotificationEventEmitter.NotificationEvents { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }

        public override IList<ContractReportTechnicalFile> XmlFiles
        {
            get
            {
                var technicalReportDoc = this.GetDocument();

                return technicalReportDoc.GetFiles(d => d.AttachedDocuments.AttachedDocumentCollection)
                    .Select(ad =>
                        new ContractReportTechnicalFile(ad)
                        {
                            Type = ContractReportTechnicalFileType.AttachedDoc,
                        }).ToList();
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportTechnicalMap : EntityTypeConfiguration<ContractReportTechnical>
    {
        public ContractReportTechnicalMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportTechnicalId);

            // Properties
            this.Property(t => t.ContractReportTechnicalId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.ContractReportId)
                .IsRequired();

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.VersionNum)
                .IsRequired();

            this.Property(t => t.VersionSubNum)
                .IsRequired();

            this.Property(t => t.Status)
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
            this.ToTable("ContractReportTechnicals");
            this.Property(t => t.ContractReportTechnicalId).HasColumnName("ContractReportTechnicalId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.VersionNum).HasColumnName("VersionNum");
            this.Property(t => t.VersionSubNum).HasColumnName("VersionSubNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusNote).HasColumnName("StatusNote");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.RegDate).HasColumnName("RegDate");
            this.Property(t => t.SubmitDate).HasColumnName("SubmitDate");
            this.Property(t => t.DateFrom).HasColumnName("DateFrom");
            this.Property(t => t.DateTo).HasColumnName("DateTo");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            this.Property(t => t.SenderContractRegistrationId).HasColumnName("SenderContractRegistrationId");

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
