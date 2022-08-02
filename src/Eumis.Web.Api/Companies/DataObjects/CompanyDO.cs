using System;
using Eumis.Data.Companies.PortalViewObjects;
using Eumis.Domain.Companies;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.ProgrammePriorities;

namespace Eumis.Web.Api.Companies.DataObjects
{
    public class CompanyDO
    {
        public CompanyDO()
        {
        }

        public CompanyDO(
            UinType uinType,
            string uin,
            string name,
            int? companyTypeId,
            int? companyLegalTypeId,
            int? seatCountryId,
            int? seatSettlementId,
            string seatPostCode,
            string seatStreet,
            int? corrCountryId,
            int? corrSettlementId,
            string corrPostCode,
            string corrStreet,
            string contactPhone,
            string contactEmail)
        {
            this.UinType = uinType;
            this.Uin = uin;
            this.Name = name;
            this.CompanyTypeId = companyTypeId;
            this.CompanyLegalTypeId = companyLegalTypeId;
            this.SeatCountryId = seatCountryId;
            this.SeatSettlementId = seatSettlementId;
            this.SeatPostCode = seatPostCode;
            this.SeatStreet = seatStreet;
            this.CorrCountryId = corrCountryId;
            this.CorrSettlementId = corrSettlementId;
            this.CorrPostCode = corrPostCode;
            this.CorrStreet = corrStreet;
            this.ContactPhone = contactPhone;
            this.ContactEmail = contactEmail;
        }

        public CompanyDO(string uin, UinType uinType, CompanyPVO company)
        {
            this.Uin = uin;
            this.UinType = uinType;
            this.CompanyTypeId = company.CompanyType.NomValueId;
            this.CompanyLegalTypeId = company.CompanyLegalType.NomValueId;
            this.Name = company.Name;
            this.NameAlt = string.IsNullOrEmpty(company.NameAlt) ? company.Name : company.NameAlt;
            this.SeatCountryId = company.SeatCountry.NomValueId;
            this.SeatSettlementId = company.SeatSettlement.NomValueId;
            this.SeatPostCode = company.SeatPostCode;
            this.SeatStreet = company.SeatStreet;
            this.SeatAddress = company.SeatAddress;
            this.CorrCountryId = company.CorrCountry.NomValueId;
            this.CorrSettlementId = company.CorrSettlement.NomValueId;
            this.CorrPostCode = company.CorrPostCode;
            this.CorrStreet = company.CorrStreet;
            this.CorrAddress = company.CorrAddress;
            this.Representative = company.Representative;
            this.Phone1 = company.Phone1;
            this.Phone2 = company.Phone2;
            this.Fax = company.Fax;
            this.Email = company.Email;
            this.ContactName = company.ContactName;
            this.ContactPhone = company.ContactPhone;
            this.ContactEmail = company.ContactEmail;
        }

        public CompanyDO(Company company)
        {
            if (company != null)
            {
                this.CompanyId = company.CompanyId;
                this.Uin = company.Uin;
                this.UinType = company.UinType;
                this.CompanyTypeId = company.CompanyTypeId;
                this.CompanyLegalTypeId = company.CompanyLegalTypeId;
                this.Name = company.Name;
                this.NameAlt = string.IsNullOrEmpty(company.NameAlt) ? company.Name : company.NameAlt;
                this.SeatCountryId = company.SeatCountryId;
                this.SeatSettlementId = company.SeatSettlementId;
                this.SeatPostCode = company.SeatPostCode;
                this.SeatStreet = company.SeatStreet;
                this.SeatAddress = company.SeatAddress;
                this.CorrCountryId = company.CorrCountryId;
                this.CorrSettlementId = company.CorrSettlementId;
                this.CorrPostCode = company.CorrPostCode;
                this.CorrStreet = company.CorrStreet;
                this.CorrAddress = company.CorrAddress;
                this.Representative = company.Representative;
                this.Phone1 = company.Phone1;
                this.Phone2 = company.Phone2;
                this.Fax = company.Fax;
                this.Email = company.Email;
                this.ContactName = company.ContactName;
                this.ContactPhone = company.ContactPhone;
                this.ContactEmail = company.ContactEmail;
                this.ProgrammePriorityType = company.ProgrammePriorityType;
                this.Version = company.Version;
            }
        }

        public int? CompanyId { get; set; }

        public Guid Gid { get; set; }

        public string Uin { get; set; }

        public UinType? UinType { get; set; }

        public int? CompanyTypeId { get; set; }

        public int? CompanyLegalTypeId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public ProgrammePriorityType? ProgrammePriorityType { get; set; }

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

        public byte[] Version { get; set; }
    }
}
