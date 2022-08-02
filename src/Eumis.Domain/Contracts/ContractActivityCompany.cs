using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.Contracts
{
    public class ContractActivityCompany
    {
        private ContractActivityCompany()
        {
        }

        public ContractActivityCompany(
            string companyUin,
            UinType companyUinType,
            string companyName)
        {
            this.CompanyUin = companyUin;
            this.CompanyUinType = companyUinType;
            this.CompanyName = companyName;
        }

        public int ContractActivityCompanyId { get; set; }

        public int ContractActivityId { get; set; }

        public string CompanyUin { get; set; }

        public UinType CompanyUinType { get; set; }

        public string CompanyName { get; set; }

        public virtual ContractActivity ContractActivity { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractActivityCompanyMap : EntityTypeConfiguration<ContractActivityCompany>
    {
        public ContractActivityCompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractActivityCompanyId);

            // Properties
            this.Property(t => t.ContractActivityCompanyId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CompanyUin)
                .HasMaxLength(200)
                .IsRequired();
            this.Property(t => t.CompanyUinType)
                .IsRequired();
            this.Property(t => t.CompanyName)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractActivityCompanies");
            this.Property(t => t.ContractActivityCompanyId).HasColumnName("ContractActivityCompanyId");
            this.Property(t => t.ContractActivityId).HasColumnName("ContractActivityId");
            this.Property(t => t.CompanyUin).HasColumnName("CompanyUin");
            this.Property(t => t.CompanyUinType).HasColumnName("CompanyUinType");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");

            this.HasRequired(t => t.ContractActivity)
                .WithMany(t => t.ContractActivityCompanies)
                .HasForeignKey(t => t.ContractActivityId)
                .WillCascadeOnDelete();
        }
    }
}
