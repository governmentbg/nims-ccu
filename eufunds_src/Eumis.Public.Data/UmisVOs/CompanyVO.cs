using System.Text;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Localization;
using UinTypeEnum = Eumis.Public.Domain.Entities.Umis.NonAggregates.UinType;

namespace Eumis.Public.Data.UmisVOs
{
    public class CompanyVO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string TransName
        {
            get
            {
                string result;

                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English && !string.IsNullOrEmpty(this.NameAlt))
                {
                    result = this.NameAlt;
                }
                else
                {
                    result = this.Name;
                }

                if (this.UinType.HasValue && this.UinType.Value == UinTypeEnum.PersonalBulstat)
                {
                    result = Helper.AnonymizeName(result);
                }

                return result;
            }
        }

        public string TransFullName
        {
            get
            {
                if (this.UinType.HasValue && this.UinType.Value == UinTypeEnum.PersonalBulstat)
                {
                    return this.TransName;
                }
                else
                {
                    return this.Uin + " " + this.TransName;
                }
            }
        }

        public int ContractsCount { get; set; }

        public string Uin { get; set; }

        public string UinAnonymized
        {
            get
            {
                return this.UinType == UinTypeEnum.PersonalBulstat ? string.Empty : this.Uin;
            }
        }

        public UinTypeEnum? UinType { get; set; }

        public string SeatCountry { get; set; }

        public string SeatSettlement { get; set; }

        public string SeatPostCode { get; set; }

        public string SeatStreet { get; set; }

        public string SeatAddress { get; set; }

        public bool IsHistoric { get; set; }

        public string Seat
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(this.SeatCountry);

                if (!string.IsNullOrWhiteSpace(this.SeatSettlement))
                {
                    sb.Append(", " + this.SeatSettlement);
                }

                if (!string.IsNullOrWhiteSpace(this.SeatPostCode))
                {
                    sb.Append(", " + this.SeatPostCode);
                }

                if (!string.IsNullOrWhiteSpace(this.SeatStreet))
                {
                    sb.Append(", " + this.SeatStreet);
                }

                if (!string.IsNullOrWhiteSpace(this.SeatAddress))
                {
                    sb.Append(", " + this.SeatAddress);
                }

                return sb.ToString();
            }
        }

        public string SeatFullName
        {
            get
            {
                if (this.UinType.HasValue && this.UinType.Value == UinTypeEnum.PersonalBulstat)
                {
                    return this.SeatCountry;
                }
                else
                {
                    return this.Seat;
                }
            }
        }
    }
}
