using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.MonitoringFinancialControl
{
    public partial class Prognosis : IAggregateRoot
    {
        private Prognosis()
        {
            this.Contracted = new BfpAmount();
            this.Payment = new BfpAmount();
            this.AdvancePayment = new BfpAmount();
            this.AdvanceVerPayment = new BfpAmount();
            this.IntermediatePayment = new BfpAmount();
            this.FinalPayment = new BfpAmount();
            this.Approved = new BfpAmount();
            this.Certified = new BfpAmount();
        }

        public Prognosis(
            PrognosisLevel level,
            int scopeId,
            Year year,
            Month month)
            : this()
        {
            this.Level = level;
            this.Year = year;
            this.Month = month;

            switch (level)
            {
                case PrognosisLevel.Programme:
                    this.ProgrammeId = scopeId;
                    break;
                case PrognosisLevel.ProgrammePriority:
                    this.ProgrammePriorityId = scopeId;
                    break;
                case PrognosisLevel.Procedure:
                    this.ProcedureId = scopeId;
                    break;
            }

            this.Status = PrognosisStatus.Draft;
            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public int PrognosisId { get; set; }

        public PrognosisLevel Level { get; set; }

        public int? ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public PrognosisStatus Status { get; set; }

        public Year Year { get; set; }

        public Month Month { get; set; }

        public BfpAmount Contracted { get; set; }

        public BfpAmount Payment { get; set; }

        public BfpAmount AdvancePayment { get; set; }

        public BfpAmount AdvanceVerPayment { get; set; }

        public BfpAmount IntermediatePayment { get; set; }

        public BfpAmount FinalPayment { get; set; }

        public BfpAmount Approved { get; set; }

        public BfpAmount Certified { get; set; }

        public bool IsActivated { get; set; }

        public string DeleteNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class PrognosisMap : EntityTypeConfiguration<Prognosis>
    {
        public PrognosisMap()
        {
            // Primary Key
            this.HasKey(t => t.PrognosisId);

            // Properties
            this.Property(t => t.PrognosisId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Level)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.Year)
                .IsRequired();
            this.Property(t => t.Month)
                .IsRequired();
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
            this.ToTable("Prognoses");
            this.Property(t => t.PrognosisId).HasColumnName("PrognosisId");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Status).HasColumnName("Status");

            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Month).HasColumnName("Month");

            this.Property(t => t.Contracted.EuAmount).HasColumnName("ContractedEuAmount");
            this.Property(t => t.Contracted.BgAmount).HasColumnName("ContractedBgAmount");
            this.Property(t => t.Contracted.TotalAmount).HasColumnName("ContractedBfpAmount");

            this.Property(t => t.Payment.EuAmount).HasColumnName("PaymentEuAmount");
            this.Property(t => t.Payment.BgAmount).HasColumnName("PaymentBgAmount");
            this.Property(t => t.Payment.TotalAmount).HasColumnName("PaymentBfpAmount");

            this.Property(t => t.AdvancePayment.EuAmount).HasColumnName("AdvancePaymentEuAmount");
            this.Property(t => t.AdvancePayment.BgAmount).HasColumnName("AdvancePaymentBgAmount");
            this.Property(t => t.AdvancePayment.TotalAmount).HasColumnName("AdvancePaymentBfpAmount");

            this.Property(t => t.AdvanceVerPayment.EuAmount).HasColumnName("AdvanceVerPaymentEuAmount");
            this.Property(t => t.AdvanceVerPayment.BgAmount).HasColumnName("AdvanceVerPaymentBgAmount");
            this.Property(t => t.AdvanceVerPayment.TotalAmount).HasColumnName("AdvanceVerPaymentBfpAmount");

            this.Property(t => t.IntermediatePayment.EuAmount).HasColumnName("IntermediatePaymentEuAmount");
            this.Property(t => t.IntermediatePayment.BgAmount).HasColumnName("IntermediatePaymentBgAmount");
            this.Property(t => t.IntermediatePayment.TotalAmount).HasColumnName("IntermediatePaymentBfpAmount");

            this.Property(t => t.FinalPayment.EuAmount).HasColumnName("FinalPaymentEuAmount");
            this.Property(t => t.FinalPayment.BgAmount).HasColumnName("FinalPaymentBgAmount");
            this.Property(t => t.FinalPayment.TotalAmount).HasColumnName("FinalPaymentBfpAmount");

            this.Property(t => t.Approved.EuAmount).HasColumnName("ApprovedEuAmount");
            this.Property(t => t.Approved.BgAmount).HasColumnName("ApprovedBgAmount");
            this.Property(t => t.Approved.TotalAmount).HasColumnName("ApprovedBfpAmount");

            this.Property(t => t.Certified.EuAmount).HasColumnName("CertifiedEuAmount");
            this.Property(t => t.Certified.BgAmount).HasColumnName("CertifiedBgAmount");
            this.Property(t => t.Certified.TotalAmount).HasColumnName("CertifiedBfpAmount");

            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.DeleteNote).HasColumnName("DeleteNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
