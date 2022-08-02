using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Common.Db;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.HistoricContracts
{
    public class HistoricContract
    {
        private static Sequence historicContractSequence = new Sequence("HistoricContractSequence", "DbContext");

        private HistoricContract()
        {
            this.HistoricContractActivities = new List<HistoricContractActivity>();
            this.HistoricContractLocations = new List<HistoricContractLocation>();
            this.HistoricContractPartners = new List<HistoricContractPartner>();
            this.HistoricContractProcurementPlans = new List<HistoricContractProcurementPlan>();
            this.HistoricContractContractedAmounts = new List<HistoricContractContractedAmount>();
            this.HistoricContractActuallyPaidAmounts = new List<HistoricContractActuallyPaidAmount>();
            this.HistoricContractReimbursedAmounts = new List<HistoricContractReimbursedAmount>();
        }

        public HistoricContract(
            int procedureId,
            DateTime modifyDate,
            string regNumber,
            string name,
            string nameEN,
            string description,
            string descriptionEN,
            string companyName,
            string companyNameEn,
            string companyUin,
            UinType companyUinType,
            int companyTypeId,
            int companyLegalTypeId,
            string seatCountryCode,
            string seatSettlementCode,
            string seatPostCode,
            string seatStreet,
            string seatAddress,
            DateTime? contractDate,
            DateTime? startDate,
            DateTime? completionDate,
            HistoricContractExecutionStatus? executionStatus,
            NutsLevel? nutsLevel)
        {
            this.HistoricContractId = historicContractSequence.NextIntValue();
            this.ProcedureId = procedureId;
            this.ModifyDate = modifyDate;
            this.RegNumber = regNumber;
            this.Name = name;
            this.NameEN = nameEN;
            this.Description = description;
            this.DescriptionEN = descriptionEN;
            this.CompanyName = companyName;
            this.CompanyNameEn = companyNameEn;
            this.CompanyUin = companyUin;
            this.CompanyUinType = companyUinType;
            this.CompanyTypeId = companyTypeId;
            this.CompanyLegalTypeId = companyLegalTypeId;
            this.SeatCountryCode = seatCountryCode;
            this.SeatSettlementCode = seatSettlementCode;
            this.SeatPostCode = seatPostCode;
            this.SeatStreet = seatStreet;
            this.SeatAddress = seatAddress;
            this.ContractDate = contractDate;
            this.StartDate = startDate;
            this.CompletionDate = completionDate;
            this.ExecutionStatus = executionStatus;
            this.NutsLevel = nutsLevel;
        }

        public int HistoricContractId { get; set; }

        public int ProcedureId { get; set; }

        public DateTime ModifyDate { get; set; }

        public string RegNumber { get; set; }

        public string Name { get; set; }

        public string NameEN { get; set; }

        public string Description { get; set; }

        public string DescriptionEN { get; set; }

        public string CompanyName { get; set; }

        public string CompanyNameEn { get; set; }

        public string CompanyUin { get; set; }

        public UinType CompanyUinType { get; set; }

        public int CompanyTypeId { get; set; }

        public int CompanyLegalTypeId { get; set; }

        public string SeatCountryCode { get; set; }

        public string SeatSettlementCode { get; set; }

        public string SeatPostCode { get; set; }

        public string SeatStreet { get; set; }

        public string SeatAddress { get; set; }

        public DateTime? ContractDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public HistoricContractExecutionStatus? ExecutionStatus { get; set; }

        public NutsLevel? NutsLevel { get; set; }

        public virtual ICollection<HistoricContractActivity> HistoricContractActivities { get; set; }

        public virtual ICollection<HistoricContractLocation> HistoricContractLocations { get; set; }

        public virtual ICollection<HistoricContractPartner> HistoricContractPartners { get; set; }

        public virtual ICollection<HistoricContractProcurementPlan> HistoricContractProcurementPlans { get; set; }

        public virtual ICollection<HistoricContractContractedAmount> HistoricContractContractedAmounts { get; set; }

        public virtual ICollection<HistoricContractActuallyPaidAmount> HistoricContractActuallyPaidAmounts { get; set; }

        public virtual ICollection<HistoricContractReimbursedAmount> HistoricContractReimbursedAmounts { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class HistoricContractMap : EntityTypeConfiguration<HistoricContract>
    {
        public HistoricContractMap()
        {
            // Primary Key
            this.HasKey(t => t.HistoricContractId);

            // Properties
            this.Property(t => t.HistoricContractId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            this.Property(t => t.RegNumber)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.CompanyUin)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.CompanyUinType)
                .IsRequired();

            this.Property(t => t.CompanyTypeId)
                .IsOptional();

            this.Property(t => t.CompanyLegalTypeId)
                .IsOptional();

            this.Property(t => t.SeatCountryCode)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.SeatSettlementCode)
                .HasMaxLength(10)
                .IsOptional();

            this.Property(t => t.SeatPostCode)
                .HasMaxLength(50)
                .IsOptional();

            this.Property(t => t.SeatStreet)
                .HasMaxLength(200)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("HistoricContracts");
            this.Property(t => t.HistoricContractId).HasColumnName("HistoricContractId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameEN).HasColumnName("NameEN");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DescriptionEN).HasColumnName("DescriptionEN");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.CompanyNameEn).HasColumnName("CompanyNameEn");
            this.Property(t => t.CompanyUin).HasColumnName("CompanyUin");
            this.Property(t => t.CompanyUinType).HasColumnName("CompanyUinType");
            this.Property(t => t.CompanyTypeId).HasColumnName("CompanyTypeId");
            this.Property(t => t.CompanyLegalTypeId).HasColumnName("CompanyLegalTypeId");
            this.Property(t => t.SeatCountryCode).HasColumnName("SeatCountryCode");
            this.Property(t => t.SeatSettlementCode).HasColumnName("SeatSettlementCode");
            this.Property(t => t.SeatPostCode).HasColumnName("SeatPostCode");
            this.Property(t => t.SeatStreet).HasColumnName("SeatStreet");
            this.Property(t => t.SeatAddress).HasColumnName("SeatAddress");
            this.Property(t => t.ContractDate).HasColumnName("ContractDate");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.CompletionDate).HasColumnName("CompletionDate");
            this.Property(t => t.ExecutionStatus).HasColumnName("ExecutionStatus");
            this.Property(t => t.NutsLevel).HasColumnName("NutsLevel");
        }
    }
}
