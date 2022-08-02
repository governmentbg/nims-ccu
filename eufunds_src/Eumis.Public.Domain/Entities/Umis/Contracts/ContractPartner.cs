using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Companies;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractPartner
    {
        private ContractPartner()
        {
        }

        public ContractPartner(
            Guid gid,
            bool isActive,
            decimal financialContribution,
            string uin,
            UinType uinType,
            int companyTypeId,
            CompanyLegalStatus companyLegalStatus,
            int companyLegalTypeId,
            string name,
            string nameAlt,
            int? kidCodeId,
            int companySizeTypeId,
            int? seatCountryId,
            int? seatSettlementId,
            string seatPostCode,
            string seatStreet,
            string seatAddress,
            int? corrCountryId,
            int? corrSettlementId,
            string corrPostCode,
            string corrStreet,
            string corrAddress,
            string representative,
            string phone1,
            string phone2,
            string fax,
            string email,
            string contactName,
            string contactPhone,
            string contactEmail)
        {
            this.Gid = gid;
            this.IsActive = isActive;
            this.FinancialContribution = financialContribution;
            this.Uin = uin;
            this.UinType = uinType;
            this.CompanyLegalStatus = companyLegalStatus;
            this.CompanyLegalTypeId = companyLegalTypeId;
            this.Name = name;
            this.NameAlt = nameAlt;
            this.KidCodeId = kidCodeId;
            this.CompanyTypeId = companyTypeId;
            this.CompanySizeTypeId = companySizeTypeId;
            this.SeatCountryId = seatCountryId;
            this.SeatSettlementId = seatSettlementId;
            this.SeatPostCode = seatPostCode;
            this.SeatStreet = seatStreet;
            this.SeatAddress = seatAddress;
            this.CorrCountryId = corrCountryId;
            this.CorrSettlementId = corrSettlementId;
            this.CorrPostCode = corrPostCode;
            this.CorrStreet = corrStreet;
            this.CorrAddress = corrAddress;
            this.Representative = representative;
            this.Phone1 = phone1;
            this.Phone2 = phone2;
            this.Fax = fax;
            this.Email = email;
            this.ContactName = contactName;
            this.ContactPhone = contactPhone;
            this.ContactEmail = contactEmail;
        }

        public int ContractPartnerId { get; set; }
        public int ContractId { get; set; }

        public Guid Gid { get; set; }
        public bool IsActive { get; set; }
        public decimal FinancialContribution { get; set; }
        public string Uin { get; set; }
        public UinType UinType { get; set; }
        public int CompanyTypeId { get; set; }
        public CompanyLegalStatus CompanyLegalStatus { get; set; }
        public int CompanyLegalTypeId { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public int? KidCodeId { get; set; }
        public int CompanySizeTypeId { get; set; }

        public int? SeatCountryId { get; set; }
        public int? SeatSettlementId { get; set; }
        public string SeatPostCode { get; set; }
        public string SeatStreet { get; set; }
        public string SeatAddress { get; set; }

        public int? CorrCountryId { get; set; }
        public int? CorrSettlementId { get; set; }
        public string CorrPostCode { get; set; }
        public string CorrStreet { get; set; }
        public string CorrAddress { get; set; }

        public string Representative { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }

        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        public virtual Contract Contract { get; set; }
    }

    public class ContractPartnerMap : EntityTypeConfiguration<ContractPartner>
    {
        public ContractPartnerMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractPartnerId);

            // Properties
            this.Property(t => t.ContractPartnerId)
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
            this.Property(t => t.CompanyTypeId)
                .IsRequired();
            this.Property(t => t.CompanyLegalTypeId)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();
            this.Property(t => t.NameAlt)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.CompanySizeTypeId)
                .IsRequired();

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

            this.Property(t => t.CorrCountryId)
                .IsOptional();
            this.Property(t => t.CorrSettlementId)
                .IsOptional();
            this.Property(t => t.CorrPostCode)
                .HasMaxLength(50)
                .IsOptional();
            this.Property(t => t.CorrStreet)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.CorrAddress)
                .IsOptional();

            this.Property(t => t.Representative)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.Phone1)
                .HasMaxLength(100)
                .IsRequired();
            this.Property(t => t.Phone2)
                .HasMaxLength(100)
                .IsOptional();
            this.Property(t => t.Fax)
                .HasMaxLength(100)
                .IsOptional();
            this.Property(t => t.Email)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.ContactName)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.ContactPhone)
                .HasMaxLength(100)
                .IsOptional();
            this.Property(t => t.ContactEmail)
                .HasMaxLength(100)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("ContractPartners");
            this.Property(t => t.ContractPartnerId).HasColumnName("ContractPartnerId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.FinancialContribution).HasColumnName("FinancialContribution");
            this.Property(t => t.Uin).HasColumnName("Uin");
            this.Property(t => t.UinType).HasColumnName("UinType");
            this.Property(t => t.CompanyTypeId).HasColumnName("CompanyTypeId");
            this.Property(t => t.CompanyLegalStatus).HasColumnName("CompanyLegalStatus");
            this.Property(t => t.CompanyLegalTypeId).HasColumnName("CompanyLegalTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.KidCodeId).HasColumnName("KidCodeId");
            this.Property(t => t.CompanySizeTypeId).HasColumnName("CompanySizeTypeId");
            this.Property(t => t.SeatCountryId).HasColumnName("SeatCountryId");
            this.Property(t => t.SeatSettlementId).HasColumnName("SeatSettlementId");
            this.Property(t => t.SeatPostCode).HasColumnName("SeatPostCode");
            this.Property(t => t.SeatStreet).HasColumnName("SeatStreet");
            this.Property(t => t.SeatAddress).HasColumnName("SeatAddress");
            this.Property(t => t.CorrCountryId).HasColumnName("CorrCountryId");
            this.Property(t => t.CorrSettlementId).HasColumnName("CorrSettlementId");
            this.Property(t => t.CorrPostCode).HasColumnName("CorrPostCode");
            this.Property(t => t.CorrStreet).HasColumnName("CorrStreet");
            this.Property(t => t.CorrAddress).HasColumnName("CorrAddress");
            this.Property(t => t.Representative).HasColumnName("Representative");
            this.Property(t => t.Phone1).HasColumnName("Phone1");
            this.Property(t => t.Phone2).HasColumnName("Phone2");
            this.Property(t => t.Fax).HasColumnName("Fax");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.ContactName).HasColumnName("ContactName");
            this.Property(t => t.ContactPhone).HasColumnName("ContactPhone");
            this.Property(t => t.ContactEmail).HasColumnName("ContactEmail");

            this.HasRequired(t => t.Contract)
                .WithMany(t => t.ContractPartners)
                .HasForeignKey(t => t.ContractId)
                .WillCascadeOnDelete();
        }
    }
}
