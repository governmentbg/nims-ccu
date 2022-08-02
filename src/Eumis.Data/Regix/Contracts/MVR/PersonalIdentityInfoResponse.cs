using System;
using System.Collections;
using System.ComponentModel;
using NationalityCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Mvr.Nationality>;

namespace Eumis.Data.Regix.Contracts.Mvr
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class NationalityList
    {
        public NationalityCollection NationalityCollection { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public class Nationality
    {
        public string NationalityCode { get; set; }

        public string NationalityName { get; set; }

        public string NationalityNameLatin { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class BirthPlace
    {
        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public string CountryNameLatin { get; set; }

        public string TerritorialUnitName { get; set; }

        public string DistrictName { get; set; }

        public string MunicipalityName { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class PersonNames
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string FamilyName { get; set; }

        public string FirstNameLatin { get; set; }

        public string SurnameLatin { get; set; }

        public string LastNameLatin { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class PermanentAddress
    {
        public string DistrictName { get; set; }

        public string DistrictNameLatin { get; set; }

        public string MunicipalityName { get; set; }

        public string MunicipalityNameLatin { get; set; }

        public string SettlementCode { get; set; }

        public string SettlementName { get; set; }

        public string SettlementNameLatin { get; set; }

        public string LocationCode { get; set; }

        public string LocationName { get; set; }

        public string LocationNameLatin { get; set; }

        public string BuildingNumber { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class PersonalIdentityInfoResponseType
    {
        public ReturnInformation ReturnInformations { get; set; }

        public string EGN { get; set; }

        public PersonNames PersonNames { get; set; }

        public string DocumentType { get; set; }

        public string DocumentTypeLatin { get; set; }

        public string IdentityDocumentNumber { get; set; }

        public DateTime? IssueDate { get; set; }

        public string IssuerPlace { get; set; }

        public string IssuerPlaceLatin { get; set; }

        public string IssuerName { get; set; }

        public string IssuerNameLatin { get; set; }

        public DateTime? ValidDate { get; set; }

        public DateTime? BirthDate { get; set; }

        public BirthPlace BirthPlace { get; set; }

        public string GenderName { get; set; }

        public string GenderNameLatin { get; set; }

        public NationalityList NationalityList { get; set; }

        public PermanentAddress PermanentAddress { get; set; }

        public double Height { get; set; }

        public string EyesColor { get; set; }

        public byte[] Picture { get; set; }

        public byte[] IdentitySignature { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class ReturnInformation
    {
        public string ReturnCode { get; set; }

        public string Info { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class PersonalIdentityInfoResponse : PersonalIdentityInfoResponseType
    {
    }
}
