using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractReportPaymentCheck : IAggregateRoot
    {
        public int ContractReportPaymentCheckId { get; set; }
        public int ContractReportPaymentId { get; set; }
        public int ContractReportId { get; set; }
        public int ContractId { get; set; }
        public Guid Gid { get; set; }

        public int OrderNum { get; set; }
        public ContractReportPaymentCheckStatus Status { get; set; }
        public ContractReportPaymentCheckApproval? Approval { get; set; }
        public ContractReportPaymentType PaymentType { get; set; }
        public Guid? BlobKey { get; set; }
        public int? CheckedByUserId { get; set; }
        public DateTime? CheckedDate { get; set; }
        
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }

        public virtual Blob File { get; set; }

        public virtual ICollection<ContractReportPaymentCheckAmount> ContractReportPaymentCheckAmounts { get; set; }
    }

    public class ContractReportPaymentCheckMap : EntityTypeConfiguration<ContractReportPaymentCheck>
    {
        public ContractReportPaymentCheckMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportPaymentCheckId);

            // Properties
            this.Property(t => t.ContractReportPaymentCheckId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportPaymentId)
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

            this.Property(t => t.PaymentType)
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
            this.ToTable("ContractReportPaymentChecks");
            this.Property(t => t.ContractReportPaymentCheckId).HasColumnName("ContractReportPaymentCheckId");
            this.Property(t => t.ContractReportPaymentId).HasColumnName("ContractReportPaymentId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");

            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");

            this.Property(t => t.Approval).HasColumnName("Approval");
            this.Property(t => t.PaymentType).HasColumnName("PaymentType");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.CheckedByUserId).HasColumnName("CheckedByUserId");
            this.Property(t => t.CheckedDate).HasColumnName("CheckedDate");
            
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            //Relationships
            this.HasOptional(t => t.File)
                .WithMany()
                .HasForeignKey(d => d.BlobKey);
        }
    }
}
