using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public partial class Irregularity : IAggregateRoot
    {
        public Irregularity()
        {
            this.FinancialCorrections = new List<IrregularityFinancialCorrection>();
            this.Documents = new List<IrregularityDoc>();
        }

        public Irregularity(FinanceSource financeSource, DateTime currentDate, IrregularitySignal signal)
        {
            this.IrregularitySignalId = signal.IrregularitySignalId;
            this.ProgrammeId = signal.ProgrammeId;
            this.ContractId = signal.ContractId;
            this.FinanceSource = financeSource;
            this.Status = IrregularityStatus.New;

            this.CreateDate = this.ModifyDate = currentDate;
        }

        public int IrregularityId { get; set; }

        public int IrregularitySignalId { get; set; }

        public int ProgrammeId { get; set; }

        public int ContractId { get; set; }

        public IrregularityStatus Status { get; set; }

        public FinanceSource FinanceSource { get; set; }

        public string RegNumber { get; set; }

        public string RegNumberPattern { get; set; }

        public DateTime? IrregularityEndDate { get; set; }

        public IrregularityCaseState? CaseState { get; set; }

        public Year? FirstReportYear { get; set; }

        public Quarter? FirstReportQuarter { get; set; }

        public Year? LastReportYear { get; set; }

        public Quarter? LastReportQuarter { get; set; }

        public string DeleteNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<IrregularityFinancialCorrection> FinancialCorrections { get; set; }

        public ICollection<IrregularityDoc> Documents { get; set; }
    }

    public class IrregularityMap : EntityTypeConfiguration<Irregularity>
    {
        public IrregularityMap()
        {
            // Primary Key
            this.HasKey(t => t.IrregularityId);

            // Properties
            this.Property(t => t.IrregularityId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IrregularitySignalId)
                .IsRequired();
            this.Property(t => t.ProgrammeId)
                .IsRequired();
            this.Property(t => t.ContractId)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.FinanceSource)
                .IsRequired();
            this.Property(t => t.RegNumber)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.RegNumberPattern)
                .HasMaxLength(200)
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
            this.ToTable("Irregularities");
            this.Property(t => t.IrregularityId).HasColumnName("IrregularityId");
            this.Property(t => t.IrregularitySignalId).HasColumnName("IrregularitySignalId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.FinanceSource).HasColumnName("FinanceSource");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");
            this.Property(t => t.RegNumberPattern).HasColumnName("RegNumberPattern");
            this.Property(t => t.IrregularityEndDate).HasColumnName("IrregularityEndDate");
            this.Property(t => t.CaseState).HasColumnName("CaseState");

            this.Property(t => t.FirstReportYear).HasColumnName("FirstReportYear");
            this.Property(t => t.FirstReportQuarter).HasColumnName("FirstReportQuarter");
            this.Property(t => t.LastReportYear).HasColumnName("LastReportYear");
            this.Property(t => t.LastReportQuarter).HasColumnName("LastReportQuarter");

            this.Property(t => t.DeleteNote).HasColumnName("DeleteNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
