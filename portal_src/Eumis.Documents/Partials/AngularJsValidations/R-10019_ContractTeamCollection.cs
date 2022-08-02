using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10019
{
    public partial class ContractTeamCollection
    {
        private bool _isValid = true;

        [XmlIgnore]
        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        [XmlIgnore]
        public int SectionNumber { get; set; }

        [XmlIgnore]
        public bool IsActive { get; set; }
    }
}
