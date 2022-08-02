using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10014
{
    public partial class ProgrammeIndicators
    {
        private bool _isValid = true;
        private bool _hasUniqueIds = true;

        [XmlIgnore]
        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        [XmlIgnore]
        public bool HasUniqueIds
        {
            get { return _hasUniqueIds; }
            set { _hasUniqueIds = value; }
        }
    }
}
