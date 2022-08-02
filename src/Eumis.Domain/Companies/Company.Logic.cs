using Eumis.Domain.OperationalMap.ProgrammePriorities;
using System;
using System.Linq;

namespace Eumis.Domain.Companies
{
    public partial class Company : IAggregateRoot
    {
        public void UpdateAttributes(
        int companyTypeId,
        int companyLegalTypeId,
        string name,
        string nameAlt,
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
        string contactEmail,
        ProgrammePriorityType programmePriorityType)
        {
            this.CompanyTypeId = companyTypeId;
            this.CompanyLegalTypeId = companyLegalTypeId;
            this.Name = name;
            this.NameAlt = nameAlt;
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
            this.ProgrammePriorityType = programmePriorityType;

            this.ModifyDate = DateTime.Now;
        }
    }
}
