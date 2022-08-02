using Eumis.Public.Common.Localization;
using Eumis.Public.Data.UmisVOs;
using System.Collections.Generic;

namespace Eumis.Public.Data.Contracts.ViewObjects
{
    public class ContractActivitiesVO : ContractCommonVO
    {
        public string Description { get; set; }

        public string DescriptionEN { get; set; }

        public string TransDescription
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.DescriptionEN) ? this.Description : this.DescriptionEN;
                }
                else
                {
                    return this.Description;
                }
            }
        }

        public IEnumerable<ContractActivityVO> Activities { get; set; }
    }
}
