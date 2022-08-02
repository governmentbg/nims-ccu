using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10022
{
    public partial class EvalTableGroup
    {
        private bool _isNameValid = true;
        private bool _isLimitValid = true;
        private bool _hasCriterias = true;

        [XmlIgnore]
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; }
        }

        [XmlIgnore]
        public bool IsLimitValid
        {
            get { return _isLimitValid; }
            set { _isLimitValid = value; }
        }

        [XmlIgnore]
        public bool HasCriterias
        {
            get { return _hasCriterias; }
            set { _hasCriterias = value; }
        }
    }
}
