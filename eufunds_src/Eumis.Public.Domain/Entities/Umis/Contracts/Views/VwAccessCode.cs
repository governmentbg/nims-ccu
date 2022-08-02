using System;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.Views
{
    public class VwAccessCode
    {
        public string Code { get; set; }

        public string Email { get; set; }

        public int ContractAccessCodeId { get; set; }

        public bool IsActive { get; set; }

        public int ContractId { get; set; }

        public Guid ContractGid { get; set; }
    }

    public class VwAccessCodeMap : EntityTypeConfiguration<VwAccessCode>
    {
        public VwAccessCodeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Email, t.ContractId });

            // Properties
            this.Property(t => t.Code)
                .IsRequired();

            this.Property(t => t.ContractAccessCodeId)
                .IsRequired();

            this.Property(t => t.ContractGid)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("vwUniqueContractEmailAccessCodeIndexed");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.ContractAccessCodeId).HasColumnName("ContractAccessCodeId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ContractGid).HasColumnName("ContractGid");
        }
    }
}
