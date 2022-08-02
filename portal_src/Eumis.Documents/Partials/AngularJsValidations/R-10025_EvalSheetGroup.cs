using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10025
{
    public partial class EvalSheetGroup
    {
        private bool _isTotalValid = true;

        [XmlIgnore]
        public bool IsTotalValid
        {
            get { return _isTotalValid; }
            set { _isTotalValid = value; }
        }
    }
}
