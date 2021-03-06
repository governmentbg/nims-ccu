using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Companies;
using Eumis.Public.Domain.Entities.Umis.Core;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class Contract : IAggregateRoot, IEventEmitter
    {
        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public int ProgrammeId { get; set; }

        public int ProcedureId { get; set; }

        public int ProjectId { get; set; }

        public ContractType ContractType { get; set; }

        public ContractStatus ContractStatus { get; set; }

        public int? AttachedContractId { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyNameAlt { get; set; }

        public string CompanyUin { get; set; }

        public UinType CompanyUinType { get; set; }

        public int? CompanyKidCodeId { get; set; }

        public int CompanySizeTypeId { get; set; }

        public int? CompanyTypeId { get; set; }

        public CompanyLegalStatus? CompanyLegalStatus { get; set; }

        public int? CompanyLegalTypeId { get; set; }

        public string CompanyEmail { get; set; }

        public string Name { get; set; }

        public string NameEN { get; set; }

        public string Description { get; set; }

        public string DescriptionEN { get; set; }

        public DateTime? ContractDate { get; set; }

        public string RegNumber { get; set; }

        public ContractExecutionStatus? ExecutionStatus { get; set; }

        public DateTime? CompletionDate { get; set; }

        public DateTime? StartDate { get; set; }

        public string StartConditions { get; set; }

        public NutsLevel? NutsLevel { get; set; }

        public int? Duration { get; set; }

        public int? ProjectKidCodeId { get; set; }

        public int? BeneficiarySeatCountryId { get; set; }

        public int? BeneficiarySeatSettlementId { get; set; }

        public string BeneficiarySeatPostCode { get; set; }

        public string BeneficiarySeatStreet { get; set; }

        public string BeneficiarySeatAddress { get; set; }

        public decimal? TotalEuAmount { get; set; }

        public decimal? TotalBgAmount { get; set; }

        public decimal? TotalBfpAmount { get; set; }

        public decimal? TotalSelfAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<ContractsContractRegistration> ContractRegistrations { get; set; }
        public virtual ICollection<ContractPartner> ContractPartners { get; set; }
        public virtual ICollection<ContractLocation> ContractLocations { get; set; }
        public virtual ICollection<ContractActivity> ContractActivities { get; set; }
        public virtual ICollection<ContractBudgetLevel3Amount> ContractBudgetLevel3Amounts { get; set; }
        public virtual ICollection<ContractContractor> ContractContractors { get; set; }
        public virtual ICollection<ContractIndicator> ContractIndicators { get; set; }
        public virtual ICollection<ContractContract> ContractContracts { get; set; }
        public virtual ICollection<ContractProcurementPlan> ContractProcurementPlans { get; set; }
        public virtual ICollection<ContractExtensionVesselIdentifier> ContractExtensionVesselIdentifiers { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }

        // navigation property should be user from EF only
        // it should not be used for accessing company properties
        public virtual Company __Company__ { get; set; }
    }

    public class ContractMap : EntityTypeConfiguration<Contract>
    {
        public ContractMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractId);

            // Properties
            this.Property(t => t.ContractId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProgrammeId)
                .IsRequired();

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.ProjectId)
                .IsRequired();

            this.Property(t => t.ContractType)
                .IsRequired();

            this.Property(t => t.ContractStatus)
                .IsRequired();

            this.Property(t => t.AttachedContractId).HasColumnName("AttachedContractId");

            this.Property(t => t.CompanyId)
                .IsRequired();

            this.Property(t => t.CompanyName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.CompanyNameAlt)
                .HasMaxLength(200)
                .IsOptional();

            this.Property(t => t.CompanyUin)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.CompanyUinType)
                .IsRequired();

            this.Property(t => t.CompanySizeTypeId)
                .IsRequired();

            this.Property(t => t.CompanyTypeId)
                .IsOptional();

            this.Property(t => t.CompanyLegalStatus)
                .IsOptional();

            this.Property(t => t.CompanyLegalTypeId)
                .IsOptional();

            this.Property(t => t.CompanyEmail)
                .HasMaxLength(200)
                .IsOptional();

            this.Property(t => t.RegNumber)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.BeneficiarySeatPostCode)
                .HasMaxLength(50)
                .IsOptional();

            this.Property(t => t.BeneficiarySeatStreet)
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
            this.ToTable("Contracts");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.ContractType).HasColumnName("ContractType");
            this.Property(t => t.ContractStatus).HasColumnName("ContractStatus");

            this.Property(t => t.CompanyId).HasColumnName("CompanyId");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.CompanyNameAlt).HasColumnName("CompanyNameAlt");
            this.Property(t => t.CompanyUin).HasColumnName("CompanyUin");
            this.Property(t => t.CompanyUinType).HasColumnName("CompanyUinType");
            this.Property(t => t.CompanyKidCodeId).HasColumnName("CompanyKidCodeId");
            this.Property(t => t.CompanySizeTypeId).HasColumnName("CompanySizeTypeId");
            this.Property(t => t.CompanyTypeId).HasColumnName("CompanyTypeId");
            this.Property(t => t.CompanyLegalStatus).HasColumnName("CompanyLegalStatus");
            this.Property(t => t.CompanyLegalTypeId).HasColumnName("CompanyLegalTypeId");
            this.Property(t => t.CompanyEmail).HasColumnName("CompanyEmail");

            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameEN).HasColumnName("NameEN");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DescriptionEN).HasColumnName("DescriptionEN");
            this.Property(t => t.ContractDate).HasColumnName("ContractDate");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");
            this.Property(t => t.ExecutionStatus).HasColumnName("ExecutionStatus");
            this.Property(t => t.CompletionDate).HasColumnName("CompletionDate");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.StartConditions).HasColumnName("StartConditions");
            this.Property(t => t.NutsLevel).HasColumnName("NutsLevel");
            this.Property(t => t.Duration).HasColumnName("Duration");
            this.Property(t => t.ProjectKidCodeId).HasColumnName("ProjectKidCodeId");

            this.Property(t => t.BeneficiarySeatCountryId).HasColumnName("BeneficiarySeatCountryId");
            this.Property(t => t.BeneficiarySeatSettlementId).HasColumnName("BeneficiarySeatSettlementId");
            this.Property(t => t.BeneficiarySeatPostCode).HasColumnName("BeneficiarySeatPostCode");
            this.Property(t => t.BeneficiarySeatStreet).HasColumnName("BeneficiarySeatStreet");
            this.Property(t => t.BeneficiarySeatAddress).HasColumnName("BeneficiarySeatAddress");

            this.Property(t => t.TotalEuAmount).HasColumnName("TotalEuAmount");
            this.Property(t => t.TotalBgAmount).HasColumnName("TotalBgAmount");
            this.Property(t => t.TotalBfpAmount).HasColumnName("TotalBfpAmount");
            this.Property(t => t.TotalSelfAmount).HasColumnName("TotalSelfAmount");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            this.HasRequired(t => t.__Company__)
                .WithMany()
                .HasForeignKey(t => t.CompanyId);
        }
    }
}
