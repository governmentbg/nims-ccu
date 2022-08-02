using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Automatizations
{
    public partial class ContractReportAcceptedAutomatization : IAggregateRoot
    {
        private ContractReportAcceptedAutomatization()
        {
        }

        public ContractReportAcceptedAutomatization(int contractReportId)
        {
            this.ContractReportId = contractReportId;
            this.Status = ContractReportAcceptedAutomatizationStatus.Sent;
            this.HasError = false;

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportId { get; set; }

        public ContractReportAcceptedAutomatizationStatus Status { get; set; }

        public bool HasError { get; set; }

        public string ErrorText { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    public class ContractReportAcceptedAutomatizationMap : EntityTypeConfiguration<ContractReportAcceptedAutomatization>
    {
        public ContractReportAcceptedAutomatizationMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportId);

            // Properties
            this.Property(t => t.ContractReportId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.ContractReportId)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.HasError)
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
            this.ToTable("ContractReportAcceptedAutomatizations");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.HasError).HasColumnName("HasError");
            this.Property(t => t.ErrorText).HasColumnName("ErrorText");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
