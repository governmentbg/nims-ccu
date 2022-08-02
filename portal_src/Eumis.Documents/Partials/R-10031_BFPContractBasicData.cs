using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace R_10031
{
    public partial class BFPContractBasicData
    {
        [XmlIgnore]
        public R_09991.EnumNomenclature NutsLevel { get; set; }

        [XmlIgnore]
        public List<Tuple<R_09991.EnumNomenclature, string>> Locations { get; set; }

        [XmlIgnore]
        public int MaxDuration { get; set; }

        [XmlIgnore]
        public bool IsPartialReadOnly { get; set; }
    }
}
