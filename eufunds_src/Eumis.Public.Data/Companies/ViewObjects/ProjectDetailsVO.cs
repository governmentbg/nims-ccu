using Eumis.Public.Common.Localization;

namespace Eumis.Public.Data.Companies.ViewObjects
{
    public class ProjectDetailsVO
    {
        public int ContractId { get; set; }

        public string Name { get; set; }

        public string NameEN { get; set; }

        public string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.NameEN) ? this.Name : this.NameEN;
                }
                else
                {
                    return this.Name;
                }
            }
        }

        public string RegNumber { get; set; }

        public bool IsHistoric { get; set; }

        public decimal? ContractAmount { get; set; }

        public int ContractCount { get; set; }
    }
}
