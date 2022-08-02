using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractCompany
    {
        public string id { get; set; }
        public string name { get; set; }
        public string nameAlt { get; set; }

        public ContractCompanyNomenclature companyType { get; set; }
        public ContractCompanyNomenclature companyLegalType { get; set; }

        public ContractCodeNomVO seatCountry { get; set; }
        public ContractCodeNomVO seatSettlement { get; set; }
        public string seatStreet { get; set; }
        public string seatPostCode { get; set; }

        public ContractCodeNomVO corrCountry { get; set; }
        public ContractCodeNomVO corrSettlement { get; set; }
        public string corrStreet { get; set; }
        public string corrPostCode { get; set; }

        public string phone1 { get; set; }
        public string email { get; set; } 
        public string fax { get; set; }

        public object Map()
        {
            ContractCompanyMap result = new ContractCompanyMap();

            result.Name = this.name;
            result.NameAlt = this.nameAlt;

            #region Nomenclatures

            result.CompanyType = new NomenclatureMap();
            if(this.companyType != null)
            {
                result.CompanyType = new NomenclatureMap()
                {
                    id = this.companyType.gid,
                    text = this.companyType.name,
                    Name = this.companyType.name
                };
            }

            result.CompanyLegalType = new NomenclatureMap();
            if (this.companyLegalType != null)
            {
                result.CompanyLegalType = new NomenclatureMap()
                {
                    id = this.companyLegalType.gid,
                    text = this.companyLegalType.name,
                    Name = this.companyLegalType.name
                };
            }

            #endregion

            #region Seat

            if (this.seatCountry != null)
            {
                result.SeatCountry = new NomenclatureMap()
                {
                    id = this.seatCountry.code,
                    text = this.seatCountry.name,
                    Code = this.seatCountry.code,
                    Name = this.seatCountry.name,
                };
            }

            if (this.seatSettlement != null)
            {
                result.SeatSettlement = new NomenclatureMap()
                {
                    id = this.seatSettlement.code,
                    text = this.seatSettlement.name,
                    Code = this.seatSettlement.code,
                    Name = this.seatSettlement.name,
                };
            }

            result.SeatPostCode = this.seatPostCode;
            result.SeatFullAddress = this.seatStreet;
           

            #endregion

            #region Correspondence

            if (this.corrCountry != null)
            {
                result.CorrespondenceCountry = new NomenclatureMap()
                {
                    id = this.corrCountry.code,
                    text = this.corrCountry.name,
                    Code = this.corrCountry.code,
                    Name = this.corrCountry.name,
                };
            }

            if (this.corrSettlement != null)
            {
                result.CorrespondenceSettlement = new NomenclatureMap()
                {
                    id = this.corrSettlement.code,
                    text = this.corrSettlement.name,
                    Code = this.corrSettlement.code,
                    Name = this.corrSettlement.name,
                };
            }

            
            result.CorrespondencePostCode = this.corrPostCode;
            result.CorrespondenceFullAddress = this.corrStreet;
            

            #endregion

            #region Contact info

           
            result.Email = this.email;
            result.Phone1 = this.phone1;
            result.Fax = this.fax;
           

            #endregion

            return result;
        }
    }

    class ContractCompanyMap
    {
        public string Name { get; set; }
        public string NameAlt { get; set; }

        public NomenclatureMap CompanyType { get; set; }
        public NomenclatureMap CompanyLegalType { get; set; }

        public NomenclatureMap SeatCountry { get; set; }
        public NomenclatureMap SeatSettlement { get; set; }
        public string SeatPostCode { get; set; }
        public string SeatFullAddress { get; set; }

        public NomenclatureMap CorrespondenceCountry { get; set; }
        public NomenclatureMap CorrespondenceSettlement { get; set; }
        public string CorrespondencePostCode { get; set; }
        public string CorrespondenceFullAddress { get; set; }

        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Fax { get; set; }
    }

    class NomenclatureMap
    {
        public NomenclatureMap()
        {
            this.id = "";
            this.text = "";
            this.Code = "";
            this.Name = "";
        }

        public string id { get; set; }
        public string text { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class ContractCodeNomVO
    {
        public int nomValueId { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        public string nameAlt { get; set; }
    }

    public class ContractCompanyContact
    {
        public string phone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
    }

    public class ContractCompanyNomenclature
    {
        public string id { get; set; }
        public string gid { get; set; }
        public string name { get; set; }
    }
}
