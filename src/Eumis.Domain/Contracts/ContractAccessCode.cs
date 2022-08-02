using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractAccessCode : IAggregateRoot
    {
        public ContractAccessCode()
        {
        }

        public ContractAccessCode(
            int contractId,
            string firstName,
            string lastName,
            string position,
            string email,
            string identifier,
            bool isActive,
            bool canReadContracts,
            bool canReadProcurements,
            bool canWriteProcurements,
            bool canReadTechnicalPlan,
            bool canWriteTechnicalPlan,
            bool canReadFinancialPlan,
            bool canWriteFinancialPlan,
            bool canReadMicrodata,
            bool canWriteMicrodata,
            bool canReadPaymentRequest,
            bool canWritePaymentRequest,
            bool canReadCommunication,
            bool canReadSpendingPlan,
            bool canWriteSpendingPlan)
        {
            this.ContractId = contractId;
            this.Gid = Guid.NewGuid();
            this.Code = this.GenerateRandomCode();
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Position = position;
            this.Identifier = identifier;
            this.Email = email;
            this.IsActive = isActive;
            this.CanReadContracts = canReadContracts;
            this.CanReadProcurements = canReadProcurements;
            this.CanWriteProcurements = canWriteProcurements;
            this.CanReadTechnicalPlan = canReadTechnicalPlan;
            this.CanWriteTechnicalPlan = canWriteTechnicalPlan;
            this.CanReadFinancialPlan = canReadFinancialPlan;
            this.CanWriteFinancialPlan = canWriteFinancialPlan;
            this.CanReadMicrodata = canReadMicrodata;
            this.CanWriteMicrodata = canWriteMicrodata;
            this.CanReadPaymentRequest = canReadPaymentRequest;
            this.CanWritePaymentRequest = canWritePaymentRequest;
            this.CanReadCommunication = canReadCommunication;
            this.CanReadSpendingPlan = canReadSpendingPlan;
            this.CanWriteSpendingPlan = canWriteSpendingPlan;

            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public int ContractAccessCodeId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public string Code { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public string Email { get; set; }

        public string Identifier { get; set; }

        public bool IsActive { get; set; }

        public bool CanReadContracts { get; set; }

        public bool CanReadProcurements { get; set; }

        public bool CanWriteProcurements { get; set; }

        public bool CanReadTechnicalPlan { get; set; }

        public bool CanWriteTechnicalPlan { get; set; }

        public bool CanReadFinancialPlan { get; set; }

        public bool CanWriteFinancialPlan { get; set; }

        public bool CanReadMicrodata { get; set; }

        public bool CanWriteMicrodata { get; set; }

        public bool CanReadPaymentRequest { get; set; }

        public bool CanWritePaymentRequest { get; set; }

        public bool CanReadCommunication { get; set; }

        public bool CanReadSpendingPlan { get; set; }

        public bool CanWriteSpendingPlan { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractAccessCodeMap : EntityTypeConfiguration<ContractAccessCode>
    {
        public ContractAccessCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractAccessCodeId);

            // Properties
            this.Property(t => t.ContractAccessCodeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractId)
                .IsRequired();
            this.Property(t => t.Gid)
                .IsRequired();
            this.Property(t => t.Code)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            this.Property(t => t.LastName)
                .HasMaxLength(100)
                .IsRequired();
            this.Property(t => t.Email)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.IsActive)
                .IsRequired();

            this.Property(t => t.CanReadContracts)
                .IsRequired();
            this.Property(t => t.CanReadProcurements)
                .IsRequired();
            this.Property(t => t.CanWriteProcurements)
                .IsRequired();
            this.Property(t => t.CanReadTechnicalPlan)
                .IsRequired();
            this.Property(t => t.CanWriteTechnicalPlan)
                .IsRequired();
            this.Property(t => t.CanReadFinancialPlan)
                .IsRequired();
            this.Property(t => t.CanWriteFinancialPlan)
                .IsRequired();
            this.Property(t => t.CanReadMicrodata)
                .IsRequired();
            this.Property(t => t.CanWriteMicrodata)
                .IsRequired();
            this.Property(t => t.CanReadPaymentRequest)
                .IsRequired();
            this.Property(t => t.CanWritePaymentRequest)
                .IsRequired();
            this.Property(t => t.CanReadCommunication)
                .IsRequired();
            this.Property(t => t.CanReadSpendingPlan)
                .IsRequired();
            this.Property(t => t.CanWriteSpendingPlan)
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
            this.ToTable("ContractAccessCodes");
            this.Property(t => t.ContractAccessCodeId).HasColumnName("ContractAccessCodeId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Identifier).HasColumnName("Identifier");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CanReadContracts).HasColumnName("CanReadContracts");
            this.Property(t => t.CanReadProcurements).HasColumnName("CanReadProcurements");
            this.Property(t => t.CanWriteProcurements).HasColumnName("CanWriteProcurements");
            this.Property(t => t.CanReadTechnicalPlan).HasColumnName("CanReadTechnicalPlan");
            this.Property(t => t.CanWriteTechnicalPlan).HasColumnName("CanWriteTechnicalPlan");
            this.Property(t => t.CanReadFinancialPlan).HasColumnName("CanReadFinancialPlan");
            this.Property(t => t.CanWriteFinancialPlan).HasColumnName("CanWriteFinancialPlan");
            this.Property(t => t.CanReadMicrodata).HasColumnName("CanReadMicrodata");
            this.Property(t => t.CanWriteMicrodata).HasColumnName("CanWriteMicrodata");
            this.Property(t => t.CanReadPaymentRequest).HasColumnName("CanReadPaymentRequest");
            this.Property(t => t.CanWritePaymentRequest).HasColumnName("CanWritePaymentRequest");
            this.Property(t => t.CanReadCommunication).HasColumnName("CanReadCommunication");
            this.Property(t => t.CanReadSpendingPlan).HasColumnName("CanReadSpendingPlan");
            this.Property(t => t.CanWriteSpendingPlan).HasColumnName("CanWriteSpendingPlan");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
