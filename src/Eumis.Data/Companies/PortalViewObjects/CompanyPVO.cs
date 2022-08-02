using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Companies;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Companies.PortalViewObjects
{
    public class CompanyPVO
    {
        public string Uin { get; set; }

        public EnumPVO<UinType> UinType { get; set; }

        public EntityGidNomVO CompanyType { get; set; }

        public EntityGidNomVO CompanyLegalType { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public EntityCodeNomVO KidCode { get; set; }

        public EntityGidNomVO CompanySizeType { get; set; }

        public EntityCodeNomVO SeatCountry { get; set; }

        public EntityCodeNomVO SeatSettlement { get; set; }

        public string SeatPostCode { get; set; }

        public string SeatStreet { get; set; }

        public string SeatAddress { get; set; }

        public EntityCodeNomVO CorrCountry { get; set; }

        public EntityCodeNomVO CorrSettlement { get; set; }

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
    }
}
