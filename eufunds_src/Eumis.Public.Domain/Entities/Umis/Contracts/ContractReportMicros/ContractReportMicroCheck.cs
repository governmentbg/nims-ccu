using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.ContractReportMicros
{
    public partial class ContractReportMicroCheck : IAggregateRoot
    {

        private ContractReportMicroCheck()
        {
        }

        public ContractReportMicroCheck(
            int contractReportMicroId,
            int contractReportId,
            int contractId,
            int orderNum,
            int checkedByUserId)
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractReportMicroId = contractReportMicroId;
            this.ContractReportId = contractReportId;
            this.ContractId = contractId;
            this.OrderNum = orderNum;
            this.Status = ContractReportMicroCheckStatus.Draft;

            this.CheckedByUserId = checkedByUserId;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportMicroCheckId { get; set; }
        public int ContractReportMicroId { get; set; }
        public int ContractReportId { get; set; }
        public int ContractId { get; set; }
        public Guid Gid { get; set; }

        public int OrderNum { get; set; }
        public ContractReportMicroCheckStatus Status { get; set; }
        public ContractReportMicroCheckApproval? Approval { get; set; }
        public Guid? BlobKey { get; set; }
        public int? CheckedByUserId { get; set; }
        public DateTime? CheckedDate { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }

        public virtual Blob File { get; set; }
    }

    public class ContractReportMicroCheckMap : EntityTypeConfiguration<ContractReportMicroCheck>
    {
        public ContractReportMicroCheckMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportMicroCheckId);

            // Properties
            this.Property(t => t.ContractReportMicroCheckId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportMicroId)
                .IsRequired();

            this.Property(t => t.ContractReportId)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.OrderNum)
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
            this.ToTable("ContractReportMicroChecks");
            this.Property(t => t.ContractReportMicroCheckId).HasColumnName("ContractReportMicroCheckId");
            this.Property(t => t.ContractReportMicroId).HasColumnName("ContractReportMicroId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Approval).HasColumnName("Approval");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.CheckedByUserId).HasColumnName("CheckedByUserId");
            this.Property(t => t.CheckedDate).HasColumnName("CheckedDate");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            this.HasOptional(t => t.File)
                .WithMany()
                .HasForeignKey(d => d.BlobKey);
        }
    }
}
