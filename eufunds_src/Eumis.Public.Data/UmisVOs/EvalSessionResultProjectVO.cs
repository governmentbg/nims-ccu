using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System;

namespace Eumis.Public.Data.UmisVOs
{
    public class EvalSessionResultProjectVO
    {
        private string companyUin;

        public int EvalSessioResultProjectId { get; set; }

        public int ProjectId { get; set; }

        public string RegNumber { get; set; }

        public DateTime RegDate { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.NameAlt) ? this.Name : this.NameAlt;
                }
                else
                {
                    return this.Name;
                }
            }
        }

        public string CompanyName { get; set; }

        public string CompanyNameAlt { get; set; }

        public string TransCompanyName
        {
            get
            {
                string transName;

                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    transName = string.IsNullOrEmpty(this.CompanyNameAlt) ? this.CompanyName : this.CompanyNameAlt;
                }
                else
                {
                    transName = this.CompanyName;
                }

                if (this.CompanyUinType == UinType.PersonalBulstat)
                {
                    transName = Helper.AnonymizeName(transName);
                }

                return transName;
            }
        }

        public string CompanyUin
        {
            get => this.CompanyUinType == UinType.PersonalBulstat ? string.Empty : this.companyUin;
            set => this.companyUin = value;
        }

        public UinType CompanyUinType { get; set; }
    }
}
