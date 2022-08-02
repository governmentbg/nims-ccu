using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.EuReimbursedAmounts
{
    public partial class EuReimbursedAmount : IAggregateRoot
    {
        public EuReimbursedAmount()
        {
            this.CertExpensesLv = new ExpensesAmount();
            this.CertExpensesEuro = new ExpensesAmount();
            this.CertReports = new List<EuReimbursedAmountCertReport>();
        }

        public EuReimbursedAmount(int programmeId)
            : this()
        {
            this.Status = EuReimbursedAmountStatus.Draft;
            this.ProgrammeId = programmeId;
            this.IsActivated = false;

            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public int EuReimbursedAmountId { get; set; }

        public int ProgrammeId { get; set; }

        public EuReimbursedAmountStatus Status { get; set; }

        public EuReimbursedAmountPaymentType? PaymentType { get; set; }

        public DateTime? Date { get; set; }

        public string PaymentAppNum { get; set; }

        public DateTime? PaymentAppSentDate { get; set; }

        public DateTime? PaymentAppDateFrom { get; set; }

        public DateTime? PaymentAppDateTo { get; set; }

        public ExpensesAmount CertExpensesLv { get; set; }

        public ExpensesAmount CertExpensesEuro { get; set; }

        public decimal? EuTranche { get; set; }

        public string Note { get; set; }

        public bool IsActivated { get; set; }

        public string DeleteNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<EuReimbursedAmountCertReport> CertReports { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class EuReimbursedAmountMap : EntityTypeConfiguration<EuReimbursedAmount>
    {
        public EuReimbursedAmountMap()
        {
            // Primary Key
            this.HasKey(t => t.EuReimbursedAmountId);

            // Properties
            this.Property(t => t.EuReimbursedAmountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProgrammeId)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.PaymentAppNum)
                .HasMaxLength(50)
                .IsOptional();
            this.Property(t => t.IsActivated)
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
            this.ToTable("EuReimbursedAmounts");
            this.Property(t => t.EuReimbursedAmountId).HasColumnName("EuReimbursedAmountId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.PaymentType).HasColumnName("PaymentType");
            this.Property(t => t.Date).HasColumnName("Date");

            this.Property(t => t.PaymentAppNum).HasColumnName("PaymentAppNum");
            this.Property(t => t.PaymentAppSentDate).HasColumnName("PaymentAppSentDate");
            this.Property(t => t.PaymentAppDateFrom).HasColumnName("PaymentAppDateFrom");
            this.Property(t => t.PaymentAppDateTo).HasColumnName("PaymentAppDateTo");

            this.Property(t => t.CertExpensesLv.BfpEuAmount).HasColumnName("CertBfpEuAmountLv");
            this.Property(t => t.CertExpensesLv.BfpBgAmount).HasColumnName("CertBfpBgAmountLv");
            this.Property(t => t.CertExpensesLv.BfpTotalAmount).HasColumnName("CertBfpTotalAmountLv");
            this.Property(t => t.CertExpensesLv.SelfAmount).HasColumnName("CertSelfAmountLv");
            this.Property(t => t.CertExpensesLv.TotalAmount).HasColumnName("CertTotalAmountLv");

            this.Property(t => t.CertExpensesEuro.BfpEuAmount).HasColumnName("CertBfpEuAmountEuro");
            this.Property(t => t.CertExpensesEuro.BfpBgAmount).HasColumnName("CertBfpBgAmountEuro");
            this.Property(t => t.CertExpensesEuro.BfpTotalAmount).HasColumnName("CertBfpTotalAmountEuro");
            this.Property(t => t.CertExpensesEuro.SelfAmount).HasColumnName("CertSelfAmountEuro");
            this.Property(t => t.CertExpensesEuro.TotalAmount).HasColumnName("CertTotalAmountEuro");

            this.Property(t => t.EuTranche).HasColumnName("EuTranche");
            this.Property(t => t.Note).HasColumnName("Note");

            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.DeleteNote).HasColumnName("DeleteNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
