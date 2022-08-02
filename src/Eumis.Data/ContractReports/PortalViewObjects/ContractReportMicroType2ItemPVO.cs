using System;

namespace Eumis.Data.ContractReports.PortalViewObjects
{
    public class ContractReportMicroType2ItemPVO
    {
        public string Number { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Uin { get; set; }

        public string Gender { get; set; }

        public int? Age { get; set; }

        public string Occupation { get; set; }

        public string Education { get; set; }

        public string AddressDistrict { get; set; }

        public string AddressSettlement { get; set; }

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

        public string ActivityPlaceDistrict { get; set; }

        public string ActivityPlaceSettlement { get; set; }

        public string ParticipationState { get; set; }

        public DateTime? LeavingDate { get; set; }

        public string CancelationReason { get; set; }

        public string LeavingState { get; set; }
    }
}
