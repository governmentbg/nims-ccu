using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.Companies
{
    public partial class Company : IAggregateRoot
    {
        public int CompanyId { get; set; }
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

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }

        public virtual CompanyType CompanyType { get; set; }
        public virtual CompanyLegalType CompanyLegalType { get; set; }
        public virtual KidCode KidCode { get; set; }
        public virtual CompanySizeType CompanySizeType { get; set; }
        public virtual Country SeatCountry { get; set; }
        public virtual Settlement SeatSettlement { get; set; }
        public virtual Country CorrCountry { get; set; }
        public virtual Settlement CorrSettlement { get; set; }

        public virtual string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.NameAlt;
                }
                else
                {
                    return this.Name;
                }
            }
        }
    }

    public class CompanyMap : EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.CompanyId);

            // Properties
            this.Property(t => t.CompanyId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

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

            this.Property(t => t.CreateDate)
                .IsRequired();
            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Companies");
            this.Property(t => t.CompanyId).HasColumnName("CompanyId");
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
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            this.HasRequired(t => t.CompanyType)
                .WithMany()
                .HasForeignKey(t => t.CompanyTypeId);
            this.HasRequired(t => t.CompanyLegalType)
                .WithMany()
                .HasForeignKey(t => t.CompanyLegalTypeId);
            this.HasOptional(t => t.KidCode)
                .WithMany()
                .HasForeignKey(t => t.KidCodeId);
            this.HasRequired(t => t.CompanySizeType)
                .WithMany()
                .HasForeignKey(t => t.CompanySizeTypeId);
            this.HasOptional(t => t.SeatCountry)
                .WithMany()
                .HasForeignKey(t => t.SeatCountryId);
            this.HasOptional(t => t.SeatSettlement)
                .WithMany()
                .HasForeignKey(t => t.SeatSettlementId);
            this.HasOptional(t => t.CorrCountry)
                .WithMany()
                .HasForeignKey(t => t.CorrCountryId);
            this.HasOptional(t => t.CorrSettlement)
                .WithMany()
                .HasForeignKey(t => t.CorrSettlementId);
        }
    }
}
