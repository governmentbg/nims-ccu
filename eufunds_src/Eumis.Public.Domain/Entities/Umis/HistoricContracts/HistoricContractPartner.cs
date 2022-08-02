using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.HistoricContracts
{
    public class HistoricContractPartner
    {
        public int HistoricContractPartnerId { get; set; }

        public int HistoricContractId { get; set; }

        public HistoricContractPartnerType PartnerType { get; set; }

        public string PartnerName { get; set; }

        public string PartnerNameEn { get; set; }

        public string PartnerUin { get; set; }

        public UinType PartnerUinType { get; set; }

        public int PartnerTypeId { get; set; }

        public int PartnerLegalTypeId { get; set; }

        public string SeatCountryCode { get; set; }

        public string SeatSettlementCode { get; set; }

        public string SeatPostCode { get; set; }

        public string SeatStreet { get; set; }

        public string SeatAddress { get; set; }

        public virtual HistoricContract HistoricContract { get; set; }
    }

    public class HistoricContractPartnerMap : EntityTypeConfiguration<HistoricContractPartner>
    {
        public HistoricContractPartnerMap()
        {
            // Primary Key
            this.HasKey(t => t.HistoricContractPartnerId);

            // Properties
            this.Property(t => t.HistoricContractPartnerId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HistoricContractId)
                .IsRequired();

            this.Property(t => t.PartnerType)
                .IsRequired();

            this.Property(t => t.PartnerName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.PartnerNameEn)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.PartnerUin)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.PartnerUinType)
                .IsRequired();

            this.Property(t => t.PartnerTypeId)
                .IsRequired();

            this.Property(t => t.PartnerLegalTypeId)
                .IsRequired();

            this.Property(t => t.SeatCountryCode)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.SeatSettlementCode)
                .HasMaxLength(10)
                .IsRequired();

            this.Property(t => t.SeatPostCode)
                .HasMaxLength(50)
                .IsOptional();

            this.Property(t => t.SeatStreet)
                .HasMaxLength(200)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("HistoricContractPartners");
            this.Property(t => t.HistoricContractPartnerId).HasColumnName("HistoricContractPartnerId");
            this.Property(t => t.HistoricContractId).HasColumnName("HistoricContractId");
            this.Property(t => t.PartnerType).HasColumnName("PartnerType");
            this.Property(t => t.PartnerName).HasColumnName("PartnerName");
            this.Property(t => t.PartnerNameEn).HasColumnName("PartnerNameEn");
            this.Property(t => t.PartnerUin).HasColumnName("PartnerUin");
            this.Property(t => t.PartnerUinType).HasColumnName("PartnerUinType");
            this.Property(t => t.PartnerTypeId).HasColumnName("PartnerTypeId");
            this.Property(t => t.PartnerLegalTypeId).HasColumnName("PartnerLegalTypeId");
            this.Property(t => t.SeatCountryCode).HasColumnName("SeatCountryCode");
            this.Property(t => t.SeatSettlementCode).HasColumnName("SeatSettlementCode");
            this.Property(t => t.SeatPostCode).HasColumnName("SeatPostCode");
            this.Property(t => t.SeatStreet).HasColumnName("SeatStreet");
            this.Property(t => t.SeatAddress).HasColumnName("SeatAddress");

            // Relationships
            this.HasRequired(t => t.HistoricContract)
                .WithMany(t => t.HistoricContractPartners)
                .HasForeignKey(d => d.HistoricContractId)
                .WillCascadeOnDelete();
        }
    }
}
