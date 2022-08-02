using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractEumisCompany
    {
        public string uin { get; set; }
        public ContractEnumNomenclature uinType { get; set; }

        public ContractPrivateNomenclature companyType { get; set; }
        public ContractEnumNomenclature companyLegalStatus { get; set; }
        public ContractPrivateNomenclature companyLegalType { get; set; }
        public string name { get; set; }
        public string nameAlt { get; set; }
        public ContractPublicNomenclature kidCode { get; set; }
        public ContractPrivateNomenclature companySizeType { get; set; }

        public ContractPublicNomenclature seatCountry { get; set; }
        public ContractPublicNomenclature seatSettlement { get; set; }
        public string seatPostCode { get; set; }
        public string seatStreet { get; set; }
        public string seatAddress { get; set; }

        public ContractPublicNomenclature corrCountry { get; set; }
        public ContractPublicNomenclature corrSettlement { get; set; }
        public string corrPostCode { get; set; }
        public string corrStreet { get; set; }
        public string corrAddress { get; set; }

        public string representative { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string fax { get; set; }
        public string email { get; set; }

        public string contactName { get; set; }
        public string contactPhone { get; set; }
        public string contactEmail { get; set; }

        public R_10004.Company Map()
        {
            R_10004.Company result = new R_10004.Company();

            result.Uin = this.uin;
            result.UinType = new R_10000.PrivateNomenclature() { Id = this.uinType.value, Name = this.uinType.description };

            if (this.companyType != null)
                result.CompanyType = new R_10000.PrivateNomenclature() { Id = this.companyType.gid, Name = this.companyType.name };

            if (this.companyLegalType != null)
                result.CompanyLegalType = new R_10000.PrivateNomenclature() { Id = this.companyLegalType.gid, Name = this.companyLegalType.name };

            result.Name = this.name;
            result.NameEN = this.nameAlt;

            #region Seat

            result.Seat = new R_10003.Address();
            if (this.seatCountry != null)
                result.Seat.Country = new R_10001.PublicNomenclature() { Code = this.seatCountry.code, Name = this.seatCountry.name };
            if (this.seatSettlement != null)
                result.Seat.Settlement = new R_10001.PublicNomenclature() { Code = this.seatSettlement.code, Name = this.seatSettlement.name };
            result.Seat.PostCode = this.seatPostCode;
            result.Seat.Street = this.seatStreet;
            result.Seat.FullAddress = this.seatAddress;

            #endregion

            #region Correspondence

            result.Correspondence = new R_10003.Address();
            if (this.corrCountry != null)
                result.Correspondence.Country = new R_10001.PublicNomenclature() { Code = this.corrCountry.code, Name = this.corrCountry.name };
            if (this.corrSettlement != null)
                result.Correspondence.Settlement = new R_10001.PublicNomenclature() { Code = this.corrSettlement.code, Name = this.corrSettlement.name };
            result.Correspondence.PostCode = this.corrPostCode;
            result.Correspondence.Street = this.corrStreet;
            result.Correspondence.FullAddress = this.corrAddress;

            #endregion

            #region Contact info

            result.Email = this.email;
            result.Phone1 = this.phone1;
            result.Phone2 = this.phone2;
            result.Fax = this.fax;

            result.CompanyRepresentativePerson = this.representative;
            result.CompanyContactPerson = this.contactName;
            result.CompanyContactPersonPhone = this.contactPhone;
            result.CompanyContactPersonEmail = this.contactEmail;

            #endregion

            return result;
        }
    }
}
