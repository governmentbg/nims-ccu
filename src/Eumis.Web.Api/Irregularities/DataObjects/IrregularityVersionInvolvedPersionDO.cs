using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;

namespace Eumis.Web.Api.Irregularities.DataObjects
{
    public class IrregularityVersionInvolvedPersionDO
    {
        public IrregularityVersionInvolvedPersionDO()
        {
        }

        public IrregularityVersionInvolvedPersionDO(int versionId, byte[] version)
        {
            this.VersionId = versionId;
            this.Version = version;
        }

        public IrregularityVersionInvolvedPersionDO(
            IrregularityVersionInvolvedPerson person,
            IrregularityVersionStatus vStatus,
            byte[] version)
        {
            this.VersionId = person.IrregularityVersionId;
            this.PersonId = person.IrregularityVersionInvolvedPersonId;
            this.LegalType = person.LegalType;
            this.Uin = person.Uin;
            this.UinType = person.UinType;
            this.UndisclosureMotives = person.UndisclosureMotives;
            this.CompanyName = person.CompanyName;
            this.TradeName = person.TradeName;
            this.HoldingName = person.HoldingName;
            this.FirstName = person.FirstName;
            this.MiddleName = person.MiddleName;
            this.LastName = person.LastName;
            this.CountryId = person.CountryId;
            this.SettlementId = person.SettlementId;
            this.PostCode = person.PostCode;
            this.Street = person.Street;
            this.Address = person.Address;

            this.VersionStatus = vStatus;
            this.Version = version;
        }

        public int VersionId { get; set; }

        public int PersonId { get; set; }

        public InvolvedPersonLegalType? LegalType { get; set; }

        public string Uin { get; set; }

        public UinType? UinType { get; set; }

        public string UndisclosureMotives { get; set; }

        public string CompanyName { get; set; }

        public string TradeName { get; set; }

        public string HoldingName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int? CountryId { get; set; }

        public int? SettlementId { get; set; }

        public string PostCode { get; set; }

        public string Street { get; set; }

        public string Address { get; set; }

        public IrregularityVersionStatus VersionStatus { get; set; }

        public byte[] Version { get; set; }
    }
}
