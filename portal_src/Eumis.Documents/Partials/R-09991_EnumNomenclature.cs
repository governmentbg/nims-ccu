using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_09991
{
    public partial class EnumNomenclature
    {
        public EnumNomenclature(Eumis.Documents.Contracts.ContractEnumNomenclature contractEnumNomenclature)
        {
            if (contractEnumNomenclature != null)
            {
                this.Value = contractEnumNomenclature.value;
                this.Description = contractEnumNomenclature.description;
                this.DescriptionEN = contractEnumNomenclature.descriptionAlt;
            }
        }

        [XmlIgnore]
        public string id { get { return this.Value; } }

        [XmlIgnore]
        public string text
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.Description, this.DescriptionEN);
            }
        }
    }
}
