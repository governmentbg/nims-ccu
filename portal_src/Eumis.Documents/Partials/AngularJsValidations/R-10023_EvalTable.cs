using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10023
{
    public partial class EvalTable
    {
        private bool _isLimitValid = true;
        private bool _hasGroups = true;

        [XmlIgnore]
        public bool IsLimitValid
        {
            get { return _isLimitValid; }
            set { _isLimitValid = value; }
        }

        [XmlIgnore]
        public bool HasGroups
        {
            get { return _hasGroups; }
            set { _hasGroups = value; }
        }
    }
}
