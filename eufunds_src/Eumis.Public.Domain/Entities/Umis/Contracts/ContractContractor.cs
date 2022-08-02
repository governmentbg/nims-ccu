using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Companies;
using Eumis.Public.Domain.Entities.Umis.Core;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractContractor
    {
        private ContractContractor()
        {
        }

        public ContractContractor(
            Guid gid,
            bool isActive,
            string uin,
            UinType uinType,
            string name,
            string nameAlt,
            int? seatCountryId,
            int? seatSettlementId,
            string seatPostCode,
            string seatStreet,
            string seatAddress,
            string representativeNames,
            string representativeIDNumber,
            YesNoNonApplicable vatRegistration,
            CompanyLegalStatus companyLegalStatus) : this()
        {
            this.Gid = gid;
            this.IsActive = isActive;
            this.Uin = uin;
            this.UinType = uinType;
            this.Name = name;
            this.NameAlt = nameAlt;
            this.SeatCountryId = seatCountryId;
            this.SeatSettlementId = seatSettlementId;
            this.SeatPostCode = seatPostCode;
            this.SeatStreet = seatStreet;
            this.SeatAddress = seatAddress;
            this.RepresentativeNames = representativeNames;
            this.RepresentativeIDNumber = representativeIDNumber;
            this.VATRegistration = vatRegistration;
            this.CompanyLegalStatus = companyLegalStatus;
        }

        public int ContractContractorId { get; set; }
        public int ContractId { get; set; }

        public Guid Gid { get; set; }
        public bool IsActive { get; set; }
        public string Uin { get; set; }
        public UinType UinType { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }

        public int? SeatCountryId { get; set; }
        public int? SeatSettlementId { get; set; }
        public string SeatPostCode { get; set; }
        public string SeatStreet { get; set; }
        public string SeatAddress { get; set; }

        public string RepresentativeNames { get; set; }
        public string RepresentativeIDNumber { get; set; }

        public YesNoNonApplicable VATRegistration { get; set; }
        public CompanyLegalStatus CompanyLegalStatus { get; set; }

        public virtual Contract Contract { get; set; }
    }

    public class ContractContractorMap : EntityTypeConfiguration<ContractContractor>
    {
        public ContractContractorMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractContractorId);

            // Properties
            this.Property(t => t.ContractContractorId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();
            this.Property(t => t.IsActive)
                .IsRequired();
            this.Property(t => t.Uin)
                .HasMaxLength(200)
                .IsRequired();
            this.Property(t => t.UinType)
                .IsRequired();
            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();
            this.Property(t => t.NameAlt)
                .HasMaxLength(200)
                .IsOptional();

            this.Property(t => t.SeatCountryId)
                .IsOptional();
            this.Property(t => t.SeatSettlementId)
                .IsOptional();
            this.Property(t => t.SeatPostCode)
                .HasMaxLength(50)
                .IsOptional();
            this.Property(t => t.SeatStreet)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.SeatAddress)
                .IsOptional();

            
            this.Property(t => t.RepresentativeIDNumber)
                .HasMaxLength(200);

            this.Property(t => t.VATRegistration)
                .IsRequired();
            this.Property(t => t.CompanyLegalStatus)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractContractors");
            this.Property(t => t.ContractContractorId).HasColumnName("ContractContractorId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Uin).HasColumnName("Uin");
            this.Property(t => t.UinType).HasColumnName("UinType");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.SeatCountryId).HasColumnName("SeatCountryId");
            this.Property(t => t.SeatSettlementId).HasColumnName("SeatSettlementId");
            this.Property(t => t.SeatPostCode).HasColumnName("SeatPostCode");
            this.Property(t => t.SeatStreet).HasColumnName("SeatStreet");
            this.Property(t => t.SeatAddress).HasColumnName("SeatAddress");
            this.Property(t => t.RepresentativeNames).HasColumnName("RepresentativeNames");
            this.Property(t => t.RepresentativeIDNumber).HasColumnName("RepresentativeIDNumber");
            this.Property(t => t.VATRegistration).HasColumnName("VATRegistration");
            this.Property(t => t.CompanyLegalStatus).HasColumnName("CompanyLegalStatus");

            this.HasRequired(t => t.Contract)
                .WithMany(t => t.ContractContractors)
                .HasForeignKey(t => t.ContractId)
                .WillCascadeOnDelete();
        }
    }
}
