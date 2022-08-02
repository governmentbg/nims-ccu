using System;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class MicrodataEsfReportItem
    {
        public string ContractRegNumber { get; set; }

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

        public string AddressDistrictName { get; set; }

        public int? AddressSettlementId { get; set; }

        public string AddressSettlementName { get; set; }

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

        public string Activity { get; set; }

        public int? ActivityPlaceDistrictId { get; set; }

        public string ActivityPlaceDistrictName { get; set; }

        public int? ActivityPlaceSettlementId { get; set; }

        public string ActivityPlaceSettlementName { get; set; }

        public ContractReportMicroType2ItemParticipationState? ParticipationState { get; set; }

        public DateTime? LeavingDate { get; set; }

        public ContractReportMicroType2ItemCancelationReason? CancelationReason { get; set; }

        public ContractReportMicroType2ItemLeavingState? LeavingState { get; set; }
    }
}
