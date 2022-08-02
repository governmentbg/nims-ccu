using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.Irregularities
{
    public partial class IrregularitySignalInvolvedPerson
    {
        public int IrregularitySignalInvolvedPersonId { get; set; }

        public int IrregularitySignalId { get; set; }

        public InvolvedPersonLegalType LegalType { get; set; }

        public string Uin { get; set; }

        public UinType UinType { get; set; }

        public string CompanyName { get; set; }

        public string TradeName { get; set; }

        public string HoldingName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int? CountryId { get; set; }

        public int? SettlementId { get; set; }

        public string PostCode { get; set; }

        public string Street { get; set; }

        public string Address { get; set; }

        public virtual IrregularitySignal IrregularitySignal { get; set; }

        public void SetAttributes(
            InvolvedPersonLegalType legalType,
            string uin,
            UinType uinType,
            string companyName,
            string tradeName,
            string holdingName,
            string firstName,
            string middleName,
            string lastName,
            int? countryId,
            int? settlementId,
            string postCode,
            string street,
            string address)
        {
            this.LegalType = legalType;
            this.Uin = uin;
            this.UinType = uinType;
            this.CompanyName = companyName;
            this.TradeName = tradeName;
            this.HoldingName = holdingName;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.CountryId = countryId;
            this.SettlementId = settlementId;
            this.PostCode = postCode;
            this.Street = street;
            this.Address = address;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class IrregularitySignalInvolvedPersonMap : EntityTypeConfiguration<IrregularitySignalInvolvedPerson>
    {
        public IrregularitySignalInvolvedPersonMap()
        {
            // Primary Key
            this.HasKey(t => t.IrregularitySignalInvolvedPersonId);

            // Properties
            this.Property(t => t.IrregularitySignalInvolvedPersonId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IrregularitySignalId)
                .IsRequired();
            this.Property(t => t.LegalType)
                .IsRequired();
            this.Property(t => t.Uin)
                .HasMaxLength(200)
                .IsRequired();
            this.Property(t => t.UinType)
                .IsRequired();
            this.Property(t => t.CompanyName)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.TradeName)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.HoldingName)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.FirstName)
                .HasMaxLength(100)
                .IsOptional();
            this.Property(t => t.MiddleName)
                .HasMaxLength(100)
                .IsOptional();
            this.Property(t => t.LastName)
                .HasMaxLength(100)
                .IsOptional();
            this.Property(t => t.PostCode)
                .HasMaxLength(50)
                .IsOptional();
            this.Property(t => t.Street)
                .HasMaxLength(200)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("IrregularitySignalInvolvedPersons");
            this.Property(t => t.IrregularitySignalInvolvedPersonId).HasColumnName("IrregularitySignalInvolvedPersonId");
            this.Property(t => t.IrregularitySignalId).HasColumnName("IrregularitySignalId");
            this.Property(t => t.LegalType).HasColumnName("LegalType");
            this.Property(t => t.Uin).HasColumnName("Uin");
            this.Property(t => t.UinType).HasColumnName("UinType");

            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.TradeName).HasColumnName("TradeName");
            this.Property(t => t.HoldingName).HasColumnName("HoldingName");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.LastName).HasColumnName("LastName");

            this.Property(t => t.CountryId).HasColumnName("CountryId");
            this.Property(t => t.SettlementId).HasColumnName("SettlementId");
            this.Property(t => t.PostCode).HasColumnName("PostCode");
            this.Property(t => t.Street).HasColumnName("Street");
            this.Property(t => t.Address).HasColumnName("Address");

            this.HasRequired(t => t.IrregularitySignal)
                .WithMany(t => t.InvolvedPersons)
                .HasForeignKey(t => t.IrregularitySignalId)
                .WillCascadeOnDelete();
        }
    }
}
