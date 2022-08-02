using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;

namespace Eumis.Web.Api.Irregularities.DataObjects
{
    public class IrregularitySignalInvolvedPersionDO
    {
        public IrregularitySignalInvolvedPersionDO()
        {
        }

        public IrregularitySignalInvolvedPersionDO(int signalId, byte[] version)
        {
            this.SignalId = signalId;
            this.Version = version;
        }

        public IrregularitySignalInvolvedPersionDO(IrregularitySignalInvolvedPerson person, byte[] version)
        {
            this.PersonId = person.IrregularitySignalInvolvedPersonId;
            this.SignalId = person.IrregularitySignalId;
            this.LegalType = person.LegalType;
            this.Uin = person.Uin;
            this.UinType = person.UinType;
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

            this.Version = version;
        }

        public int SignalId { get; set; }

        public int PersonId { get; set; }

        public InvolvedPersonLegalType? LegalType { get; set; }

        public string Uin { get; set; }

        public UinType? UinType { get; set; }

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

        public byte[] Version { get; set; }
    }
}
