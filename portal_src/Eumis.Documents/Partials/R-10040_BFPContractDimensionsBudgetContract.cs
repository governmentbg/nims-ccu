using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10040
{
    public partial class BFPContractDirectionsBudgetContract
    {
        [XmlIgnore]
        public int SectionNumber { get; set; }

        [XmlIgnore]
        public Dictionary<Eumis.Documents.Mappers.NomenclatureType, List<Eumis.Documents.Mappers.Nomenclature>> Nomenclatures { get; set; }

        [XmlIgnore]
        public bool IsDirectionSectionSelected { get; set; }
    }
}
