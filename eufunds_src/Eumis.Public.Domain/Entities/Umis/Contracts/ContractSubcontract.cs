using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public class ContractSubcontract
    {
        private ContractSubcontract()
        {
        }

        public ContractSubcontract(
            ContractSubcontractType type,
            DateTime date,
            string number,
            decimal amount)
        {
            this.Type = type;
            this.Date = date;
            this.Number = number;
            this.Amount = amount;
        }

        public int ContractSubcontractId { get; set; }
        public int ContractContractId { get; set; }
        public int ContractContractorId { get; set; }

        public ContractSubcontractType Type { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public decimal Amount { get; set; }

        public virtual ContractContract ContractContract { get; set; }
        public virtual ContractContractor ContractContractor { get; set; }
    }

    public class ContractSubcontractMap : EntityTypeConfiguration<ContractSubcontract>
    {
        public ContractSubcontractMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractSubcontractId);

            // Properties
            this.Property(t => t.ContractSubcontractId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Type)
                .IsRequired();
            this.Property(t => t.Date)
                .IsRequired();
            this.Property(t => t.Number)
                .IsRequired();
            this.Property(t => t.Amount)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractSubcontracts");
            this.Property(t => t.ContractSubcontractId).HasColumnName("ContractSubcontractId");
            this.Property(t => t.ContractContractId).HasColumnName("ContractContractId");
            this.Property(t => t.ContractContractorId).HasColumnName("ContractContractorId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Number).HasColumnName("Number");
            this.Property(t => t.Amount).HasColumnName("Amount");

            this.HasRequired(t => t.ContractContract)
                .WithMany(t => t.ContractSubcontracts)
                .HasForeignKey(t => t.ContractContractId)
                .WillCascadeOnDelete();

            this.HasRequired(t => t.ContractContractor)
                .WithMany()
                .HasForeignKey(t => t.ContractContractorId);
        }
    }
}
