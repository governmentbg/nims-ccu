using Eumis.Documents.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;
using Eumis.Common.Helpers;

namespace R_10038
{
    public partial class BFPContractIndicator
    {
        [XmlIgnore]
        public bool IsDeactivated { get; set; }

        [XmlIgnore]
        public IndicatorTrend Trend
        {
            get
            {
                var result = IndicatorTrend.Inapplicable;

                if (this.SelectedIndicator != null && !String.IsNullOrWhiteSpace(this.SelectedIndicator.TrendName))
                {
                    var types = new List<IndicatorTrend> { IndicatorTrend.Inapplicable, IndicatorTrend.Increase, IndicatorTrend.Reduction };

                    foreach(var type in types)
                    {
                        if (this.SelectedIndicator.TrendName.Equals(DataUtils.GetEnumDescription<IndicatorTrend>(type), StringComparison.InvariantCultureIgnoreCase))
                            return type;
                    }
                }

                return result;
            }
        }
    }

    [MetadataType(typeof(PrivateNomenclatureMetadata))]
    public partial class SelectedIndicator
    {
        [XmlIgnore]
        public string text { get { return this.Name ?? string.Empty; } }

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
