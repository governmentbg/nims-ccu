using System;
using System.Diagnostics.CodeAnalysis;
using DetailTypeCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Tr.DetailType>;
using NonMonetaryDepositTypeCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Tr.NonMonetaryDepositType>;
using ShareTypeCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Tr.ShareType>;

namespace Eumis.Data.Regix.Contracts.Tr
{
    public enum StatusType
    {
        Нова_партида,
        Пререгистрирана_партида,
        Нова_закрита_партида,
        Пререгистрирана_закрита_партида,
    }

    public enum IndentTypeType
    {
        EGN,
        UIC,
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class LegalFormType
    {
        public string LegalFormAbbr { get; set; }

        public string LegalFormName { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class AddressType
    {
        public string CountryCode { get; set; }

        public string Country { get; set; }

        public string IsForeign { get; set; }

        public string DistrictEkatte { get; set; }

        public string District { get; set; }

        public string MunicipalityEkatte { get; set; }

        public string Municipality { get; set; }

        public string SettlementEKATTE { get; set; }

        public string Settlement { get; set; }

        public string Area { get; set; }

        public string AreaEkatte { get; set; }

        public string PostCode { get; set; }

        public string ForeignPlace { get; set; }

        public string HousingEstate { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public string Block { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class ContactsType
    {
        public string Phone { get; set; }

        public string Fax { get; set; }

        public string EMail { get; set; }

        public Uri URL { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class SeatType
    {
        public AddressType Address { get; set; }

        public ContactsType Contacts { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class SubjectOfActivityType
    {
        public string Subject { get; set; }

        public string IsBank { get; set; }

        public string IsInsurer { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class NKIDType
    {
        public string NKIDcode { get; set; }

        public string NKIDname { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class AddemptionOfTraderType
    {
        public AddressType Address { get; set; }

        public ContactsType Contacts { get; set; }

        public string CompetentAuthorityForRegistration { get; set; }

        public string RegistrationNumber { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class CapitalAmountType
    {
        public string Value { get; set; }

        public string Euro { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class ShareType
    {
        public string Type { get; set; }

        public string Count { get; set; }

        public string NominalValue { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class SharesType
    {
        public ShareTypeCollection ShareCollection { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class NonMonetaryDepositType
    {
        public string Description { get; set; }

        public string Value { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class NonMonetaryDepositsType
    {
        public NonMonetaryDepositTypeCollection NonMonetaryDepositCollection { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class MandateType
    {
        public string Type { get; set; }

        public string MandateValue { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class DetailsType
    {
        public DetailTypeCollection DetailCollection { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class SubjectType
    {
        public string Indent { get; set; }

        public string Name { get; set; }

        public IndentTypeType IndentType { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class DetailType
    {
        public string FieldName { get; set; }

        public string FieldCode { get; set; }

        public string FieldOrder { get; set; }

        public SubjectType Subject { get; set; }
    }
}
