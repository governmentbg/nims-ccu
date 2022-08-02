using System;
using System.Xml.Serialization;

namespace R_10047
{
    public partial class ContractContractor
    {
        [XmlIgnore]
        public string NomenclatureName
        {
            get
            {
                string result = "No " + this.Number;

                if (this.SignDate.HasValue)
                    result += " / " + this.SignDate.Value.ToString("dd.MM.yyyy");

                if (this.Contractor != null)
                    result += " - " + this.Contractor.Name;
                return result;
            }
        }
    }
}
