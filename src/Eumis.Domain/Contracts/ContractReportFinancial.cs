using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportFinancial : RioXmlDocumentWithFiles<Rio.FinanceReport, ContractReportFinancialFile>, IAggregateRoot, INotificationEventEmitter, IEventEmitter
    {
        private ContractReportFinancial()
        {
            ((INotificationEventEmitter)this).NotificationEvents = new List<INotificationEvent>();
            ((IEventEmitter)this).Events = new List<IDomainEvent>();
        }

        public ContractReportFinancial(
            int contractId,
            int contractReportId,
            int versionSubNum,
            ContractReportFinancial oldFinancial)
            : this()
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractId = contractId;
            this.ContractReportId = contractReportId;
            this.VersionNum = oldFinancial.VersionNum;
            this.VersionSubNum = versionSubNum;
            this.Status = ContractReportFinancialStatus.Draft;

            // change the document modificationDate so that a unique hash is produced
            var financialDoc = oldFinancial.GetDocument();
            financialDoc.modificationDate = currentDate;
            financialDoc.docSubNumber = versionSubNum.ToString();
            this.SetXml(financialDoc);

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public ContractReportFinancial(
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
            this.Status = ContractReportFinancialStatus.Draft;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        public ContractReportFinancialStatus Status { get; set; }

        public string StatusNote { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? SubmitDate { get; set; }

        public decimal PaymentsFinalRecipientsAmount { get; set; }

        public decimal CommitmentsGuaranteeAmount { get; set; }

        public decimal ExpenseManagementAmount { get; set; }

        public decimal ManagementFeesAmount { get; set; }

        public decimal CorrespondingPublicSpendingAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public int? SenderContractRegistrationId { get; set; }

        ICollection<INotificationEvent> INotificationEventEmitter.NotificationEvents { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }

        public override IList<ContractReportFinancialFile> XmlFiles
        {
            get
            {
                return this.GetDocument()
                    .GetFiles(d => d.CostSupportingDocuments.CostSupportingDocumentCollection, csd => csd.AttachedDocumentCollection)
                    .Select(ad => new ContractReportFinancialFile(ad))
                    .ToList();
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportFinancialMap : EntityTypeConfiguration<ContractReportFinancial>
    {
        public ContractReportFinancialMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportFinancialId);

            // Properties
            this.Property(t => t.ContractReportFinancialId)
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
            this.ToTable("ContractReportFinancials");
            this.Property(t => t.ContractReportFinancialId).HasColumnName("ContractReportFinancialId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.VersionNum).HasColumnName("VersionNum");
            this.Property(t => t.VersionSubNum).HasColumnName("VersionSubNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusNote).HasColumnName("StatusNote");

            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.SubmitDate).HasColumnName("SubmitDate");

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
