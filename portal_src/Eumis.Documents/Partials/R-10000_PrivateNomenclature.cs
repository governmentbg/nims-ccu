using Eumis.Common;
using Eumis.Common.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10000
{
    [MetadataType(typeof(PrivateNomenclatureMetadata))]
    public partial class PrivateNomenclature
    {
        [XmlIgnore]
        public IEnumerable<LocalizedSelectListItem> Items { get; set; }

        [XmlIgnore]
        public string FunctionName { get; set; }

        [XmlIgnore]
        public bool isSignatureRequired { get; set; }

        [XmlIgnore]
        public string text { get { return this.displayName; } }

        [XmlIgnore]
        public string displayName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.Name, this.NameEN);
            }
        }

        [Serializable]
        internal sealed class PrivateNomenclatureMetadata
        {
//#pragma warning disable 0649

            [JsonProperty(PropertyName = "id")]
            public string Id { get; set; }

//#pragma warning restore 0649
        }
    }
}
