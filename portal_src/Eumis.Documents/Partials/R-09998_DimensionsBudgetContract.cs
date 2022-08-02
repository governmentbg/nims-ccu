using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_09998
{
    public partial class DirectionsBudgetContract
    {
        [XmlIgnore]
        public int Index { get; set; }

        [XmlIgnore]
        public int SectionNumber { get; set; }

        [XmlIgnore]
        public string ProgrammeNameFormatted 
        { 
            get
            {
                return String.IsNullOrWhiteSpace(this.programmeName) ? "" : string.Format(Eumis.Common.Resources.Global.ProgrammeNameTemplate,
                                                this.programmeName);
            }
        }

        [XmlIgnore]
        public Dictionary<Eumis.Documents.Mappers.NomenclatureType, List<Eumis.Documents.Mappers.Nomenclature>> Nomenclatures { get; set; }

        [XmlIgnore]
        public bool IsFinalRecipients { get; set; }

        [XmlIgnore]
        public bool IsFinancialIntermediaries { get; set; }

        [XmlIgnore]
        public bool IsDirectionSelected { get; set; }
    }
}
