using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractReportPayment : IAggregateRoot
    {
        private ContractReportPayment()
        {
        }

        public string Xml { get; private set; }

        public string Hash { get; private set; }

        public int ContractReportPaymentId { get; set; }
        public int ContractReportId { get; set; }
        public int ContractId { get; set; }
        public Guid Gid { get; set; }
        public int VersionNum { get; set; }
        public int VersionSubNum { get; set; }
        public ContractReportPaymentStatus Status { get; set; }
        public string StatusNote { get; set; }
        public ContractReportPaymentType? PaymentType { get; set; }
        public DateTime? RegDate { get; set; }
        public string OtherRegistration { get; set; }
        public DateTime? SubmitDate { get; set; }
        public DateTime? SubmitDeadline { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal? AdditionalIncome { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? RequestedAmount { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }
    }

    public class ContractReportPaymentMap : EntityTypeConfiguration<ContractReportPayment>
    {
        public ContractReportPaymentMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportPaymentId);

            // Properties
            this.Property(t => t.ContractReportPaymentId)
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
            this.ToTable("ContractReportPayments");
            this.Property(t => t.ContractReportPaymentId).HasColumnName("ContractReportPaymentId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.VersionNum).HasColumnName("VersionNum");
            this.Property(t => t.VersionSubNum).HasColumnName("VersionSubNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusNote).HasColumnName("StatusNote");
            this.Property(t => t.PaymentType).HasColumnName("PaymentType");
            this.Property(t => t.RegDate).HasColumnName("RegDate");
            this.Property(t => t.OtherRegistration).HasColumnName("OtherRegistration");
            this.Property(t => t.SubmitDate).HasColumnName("SubmitDate");
            this.Property(t => t.SubmitDeadline).HasColumnName("SubmitDeadline");
            this.Property(t => t.DateFrom).HasColumnName("DateFrom");
            this.Property(t => t.DateTo).HasColumnName("DateTo");
            this.Property(t => t.AdditionalIncome).HasColumnName("AdditionalIncome");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");
            this.Property(t => t.RequestedAmount).HasColumnName("RequestedAmount");

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
