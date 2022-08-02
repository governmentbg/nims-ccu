using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10021
{
    public partial class EvalTableCriteria
    {
        private bool _isNameValid = true;
        private bool _isWeightValid = true;

        [XmlIgnore]
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; }
        }

        [XmlIgnore]
        public bool IsWeightValid
        {
            get { return _isWeightValid; }
            set { _isWeightValid = value; }
        }
    }
}
