using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.ContractReportMicros
{
    public class ContractReportMicrosType2Item
    {
        public int ContractReportMicrosType2ItemId { get; set; }

        public int ContractReportMicroId { get; set; }

        public string Number { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Uin { get; set; }

        public ContractReportMicroType2ItemGender? Gender { get; set; }

        public int? Age { get; set; }

        public ContractReportMicroType2ItemOccupation? Occupation { get; set; }

        public ContractReportMicroType2ItemEducation? Education { get; set; }

        public int? AddressDistrictId { get; set; }

        public int? AddressSettlementId { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool? IsEmigrant { get; set; }

        public bool? IsForeigner { get; set; }

        public bool? IsMinority { get; set; }

        public bool? IsGypsy { get; set; }

        public bool? IsDisabledPerson { get; set; }

        public bool? IsHomeless { get; set; }

        public string DisadvantagedPerson { get; set; }

        public bool? IsLivingInUnemployedHousehold { get; set; }

        public bool? IsLivingInUnemployedHouseholdWithChildren { get; set; }

        public bool? IsLivingInFamilyOfOneWithChildren { get; set; }

        public DateTime? JoiningDate { get; set; }

        public ContractReportMicroType2ItemActivity? Activity { get; set; }

        public int? ActivityPlaceDistrictId { get; set; }

        public int? ActivityPlaceSettlementId { get; set; }

        public ContractReportMicroType2ItemParticipationState? ParticipationState { get; set; }

        public DateTime? LeavingDate { get; set; }

        public ContractReportMicroType2ItemCancelationReason? CancelationReason { get; set; }

        public ContractReportMicroType2ItemLeavingState? LeavingState { get; set; }
    }

    public class ContractReportMicrosType2ItemMap : EntityTypeConfiguration<ContractReportMicrosType2Item>
    {
        public ContractReportMicrosType2ItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportMicrosType2ItemId);

            // Properties
            this.Property(t => t.ContractReportMicrosType2ItemId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.ContractReportMicroId)
                .IsRequired();
            this.Property(t => t.Number)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.FirstName)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.MiddleName)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.LastName)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.Uin)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.Phone)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.Email)
                .HasMaxLength(200)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("ContractReportMicrosType2Items");
            this.Property(t => t.ContractReportMicrosType2ItemId).HasColumnName("ContractReportMicrosType2ItemId");
            this.Property(t => t.ContractReportMicroId).HasColumnName("ContractReportMicroId");

            this.Property(t => t.Number).HasColumnName("Number");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Uin).HasColumnName("Uin");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Age).HasColumnName("Age");
            this.Property(t => t.Occupation).HasColumnName("Occupation");
            this.Property(t => t.Education).HasColumnName("Education");
            this.Property(t => t.AddressDistrictId).HasColumnName("AddressDistrictId");
            this.Property(t => t.AddressSettlementId).HasColumnName("AddressSettlementId");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Email).HasColumnName("Email");

            this.Property(t => t.IsEmigrant).HasColumnName("IsEmigrant");
            this.Property(t => t.IsForeigner).HasColumnName("IsForeigner");
            this.Property(t => t.IsMinority).HasColumnName("IsMinority");
            this.Property(t => t.IsGypsy).HasColumnName("IsGypsy");
            this.Property(t => t.IsDisabledPerson).HasColumnName("IsDisabledPerson");
            this.Property(t => t.IsHomeless).HasColumnName("IsHomeless");
            this.Property(t => t.DisadvantagedPerson).HasColumnName("DisadvantagedPerson");

            this.Property(t => t.IsLivingInUnemployedHousehold).HasColumnName("IsLivingInUnemployedHousehold");
            this.Property(t => t.IsLivingInUnemployedHouseholdWithChildren).HasColumnName("IsLivingInUnemployedHouseholdWithChildren");
            this.Property(t => t.IsLivingInFamilyOfOneWithChildren).HasColumnName("IsLivingInFamilyOfOneWithChildren");

            this.Property(t => t.JoiningDate).HasColumnName("JoiningDate");
            this.Property(t => t.Activity).HasColumnName("Activity");
            this.Property(t => t.ActivityPlaceDistrictId).HasColumnName("ActivityPlaceDistrictId");
            this.Property(t => t.ActivityPlaceSettlementId).HasColumnName("ActivityPlaceSettlementId");

            this.Property(t => t.ParticipationState).HasColumnName("ParticipationState");
            this.Property(t => t.LeavingDate).HasColumnName("LeavingDate");
        }
    }
}
